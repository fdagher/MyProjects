using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CarsBot.Repository
{
    public static class CarRepository
    {
        static IList<Car> cars;

        static CarRepository()
        {
            cars = CarBuilder.Build();
        }

        public static IList<Car> Search(CarType type, BodyType body, int? budget, int? kilometers)
        {
            return cars.Where(c => (c.Body == body || body == BodyType.Any) && 
                                   (c.Make == type || type == CarType.Any) && 
                                   (kilometers == null || c.KilometersDriven <= kilometers) && 
                                   (budget == null || c.Price <= budget)).ToList<Car>();
        }

        public static string CarInfo(string registrationNumber)
        {
            string url = string.Format("https://www.regcheck.org.uk/api/PrivateApi_1.aspx?RegistrationNumber={0}&State=NSW&username=pickles_auctions", registrationNumber);

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }
    }
}