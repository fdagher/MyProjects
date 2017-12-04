using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECTSRepository
{
    public class Repository
    {
        public static IEnumerable<Entity> GetCodeRecords(IEnumerable<Entity> codes)
        {
            List<Entity> output = new List<Entity>();

            using (SqlConnection connection = new SqlConnection())
            {
                int _counter = 0;
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["basConnStr"].ConnectionString;

                SqlCommand command;

                string sqlcommand = "";

                foreach (Entity ent in codes)
                {
                    sqlcommand += "SELECT * FROM VCT_IHubUnified_" + ent.BusinessTerm + " WHERE Code = '" + ent.CodeValue + "';";
                    _counter++;
                }

                command = new SqlCommand(sqlcommand,connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                for (int i=0; i<_counter; i++)
                {
                    if (reader.Read())
                    {
                        output.Add(new Entity
                        {
                            BusinessTerm = codes.ToList<Entity>()[i].BusinessTerm,
                            CodeValue = reader.GetString(0),
                            CodeDescription = reader.GetString(1),
                            PropertyName = codes.ToList<Entity>()[i].PropertyName
                        });
                    }
                    else
                    {
                        output.Add(new Entity
                        {
                            BusinessTerm = codes.ToList<Entity>()[i].BusinessTerm,
                            CodeValue = codes.ToList<Entity>()[i].CodeValue,
                            CodeDescription = null,
                            PropertyName = codes.ToList<Entity>()[i].PropertyName
                        });
                    }

                    reader.NextResult();
                }

                reader.Close();
            }

            return output;
        }
    }
}
