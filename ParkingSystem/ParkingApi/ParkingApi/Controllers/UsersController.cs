using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ParkingApi;
using System.Configuration;
using ParkingApi.ObjectModel;
using System.Web.Http.Cors;

namespace ParkingApi.Controllers
{
    [RoutePrefix("api/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private ParkingDBEntities db = new ParkingDBEntities();
        IMessageTemplate messageTemplate;

        #region User Management Methods

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        #endregion

        #region Parking Booking Methods

        // GET: api/Users/5/confirm
        [HttpGet]
        [Route("broadcast")]
        public IHttpActionResult BroadcastConfirmation()
        {
            IQueryable<User>  users = db.Users.Where(x => x.Date == DateTime.Today && x.ParkingToday);

            if (users == null || users.Count() == 0)
            {
                return NotFound();
            }

            IQueryable<User> unallocatedUsers = db.Users.Where(x => x.ParkingToday == false &&
                                                                        x.Date == DateTime.Today);

            foreach (User user in users)
            {
                // send the confirmation email
                this.SendConfirmationNotification(user, unallocatedUsers);
            }

            return Ok("Success");
        }

        // GET: api/Users/5/confirm
        [HttpGet]
        [Route("{id}/confirm/{slot?}")]
        public async Task<IHttpActionResult> ConfirmUserParkingSlot(int id, string slot = null)
        {
            User user = await db.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // if user already assigned a parking slot, send a success notification and return
            if (user.ParkingToday)
            {
                this.SendSuccessNotification(user);

                return Ok("Success");
            }
            else
            {
                // if parking slots already full, reject the confirmation and send a rejection notification to the user
                if (db.Users.Where(x=>x.ParkingToday).Count() == 
                            int.Parse(ConfigurationManager.AppSettings["ParkingApi.NumberOfParkingSlots"]))
                {
                    this.SendRejectionNotification(user);

                    return StatusCode(HttpStatusCode.Forbidden);
                }
                else
                {
                    // else, set the parking slot and send a confirmation email to the user. This use case happens after the user
                    // receives an interest email and selects confirm to take the empty slot. In this case, he has to confirm
                    // his place same as if he was assigned the slot from the beginning
                    db.Entry(user).State = EntityState.Modified;

                    try
                    {
                        // set the parking slot
                        user.ParkingToday = true;
                        user.ParkingSlot = slot;

                        await db.SaveChangesAsync();

                        // send the success email
                        this.SendSuccessNotification(user);

                        return Ok("Success");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        // send rejection email as the operation failed, probably due to the fact that somebody else
                        // took the slot
                        this.SendRejectionNotification(user);

                        return StatusCode(HttpStatusCode.Forbidden);
                    }
                }
            }
        }

        // GET: api/Users/5/cancel
        [HttpGet]
        [Route("{id}/cancel")]
        public async Task<IHttpActionResult> CancelUserParkingSlot(int id)
        {
            User user = await db.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                IQueryable<User> interestedUsers = db.Users.Where(x => x.ParkingToday == false &&
                                                                         x.Date == DateTime.Today &&
                                                                         x.Id != user.Id);
                Parallel.ForEach<User>(interestedUsers, (targetUsr) => 
                {
                    this.SendInterestNotification(user, targetUsr);
                });

                user.ParkingToday = false;
                user.ParkingSlot = null;

                await db.SaveChangesAsync();

                return Ok("Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                this.SendRejectionNotification(user);

                return StatusCode(HttpStatusCode.Forbidden);
            }
        }

        // GET: api/Users/5/forward
        [HttpGet]
        [Route("{id}/forward/{targetId}")]
        public async Task<IHttpActionResult> ForwardUserParkingSlot(int id, int targetId)
        {
            User user = await db.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (!user.ParkingToday)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            User userto = await db.Users.FindAsync(targetId);

            if (userto == null)
            {
                return NotFound();
            }
            
            db.Entry(user).State = EntityState.Modified;
            db.Entry(userto).State = EntityState.Modified;

            try
            {
                user.ParkingToday = false;
                userto.ParkingSlot = user.ParkingSlot;
                userto.ParkingToday = true;
                user.ParkingSlot = null;

                await db.SaveChangesAsync();

                IQueryable<User> unallocatedUsers = db.Users.Where(x => x.ParkingToday == false &&
                                                                        x.Date == DateTime.Today &&
                                                                        x.Id != user.Id && 
                                                                        x.Id != userto.Id);

                // send confirmation email to the forwarded to user
                this.SendConfirmationNotification(userto, unallocatedUsers);

                return Ok("Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                this.SendRejectionNotification(user);

                return StatusCode(HttpStatusCode.Forbidden);
            }
        }

        #endregion

        #region Private Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

        private void SendSuccessNotification(User user)
        {
            messageTemplate = new SuccessEmailMesageTemplate(user);

            NotificationManager.SendNotification("Your parking slot has been confirmed!",
                                                  messageTemplate.Template,
                                                  user.Email,
                                                  NotificationMedia.Email);
        }

        private void SendRejectionNotification(User user)
        {
            messageTemplate = new RejectionEmailMesageTemplate(user);

            NotificationManager.SendNotification("Your parking slot request has been rejected!",
                                                  messageTemplate.Template,
                                                  user.Email,
                                                  NotificationMedia.Email);
        }

        private void SendInterestNotification(User user, User targetUsr)
        {
            string baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/api/users";
            messageTemplate = new InterestEmailMesageTemplate(user, targetUsr, baseUrl);

            NotificationManager.SendNotification("There is an empty parking slot!",
                                                  messageTemplate.Template,
                                                  targetUsr.Email,
                                                  NotificationMedia.Email);
        }

        private void SendConfirmationNotification(User user, IQueryable<User> targetUsers)
        {
            string baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/api/users";
            messageTemplate = new ConfirmationEmailMesageTemplate(user, targetUsers, baseUrl);

            NotificationManager.SendNotification("Parking slot confirmation!",
                                                  messageTemplate.Template,
                                                  user.Email,
                                                  NotificationMedia.Email);
        }
        #endregion
    }
}