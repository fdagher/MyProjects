using CarDetails.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDetails.Controllers
{
    public class VehiclesController : ApiController
    {
        [HttpGet]
        public Vehicle Get(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = @"Server=tcp:aae-sqldatawarehousesrv-dvt01.database.windows.net,1433;Database=aae-SQLDataWareHouseDW-DVT01;User ID=bizdataDWH;Password=iuBxj8E1GaVsGfP2YHbO;";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = @"SELECT [valuer_book_code_id]
                                          ,[valuer_book_code]
                                          ,[valuer_book_conv]
                                          ,[no_of_doors]
                                          ,[no_of_gears]
                                          ,[engine_capacity]
                                          ,[cylinders]
                                          ,[make_year_full]
                                          ,[month_group]
                                          ,[year_group]
                                          ,[release_month]
                                          ,[release_year]
                                          ,[engine_size]
                                          ,[gross_weight]
                                          ,[payload]
                                          ,[gross_combination_mass]
                                          ,[glasses_nvic]
                                          ,[glasses_mrrp]
                                          ,[glasses_trade_low]
                                          ,[glasses_average_kms]
                                          ,[glasses_km_category]
                                          ,[glasses_trade_medium]
                                          ,[VIN_Mask]
                                          ,[Engine_Mask]
                                          ,[RBC_Avg_wholesale]
                                          ,make.[code] as make_code
                                          ,make.[description] as make_description
                                          ,model.[code] as model_code
                                          ,model.[description] as model_description
                                          ,auc_type.[code] as auc_type_code
                                          ,auc_type.[description] as auc_type_description
                                FROM
                                    [PAS_auction].[valuer_book_code] as vbook join [PAS_auction].[make] as make on vbook.make_id = make.[make_id]
                                    join [PAS_auction].[model] as model on vbook.model_id = model.model_id
                                    join [PAS_auction].[type] as auc_type on auc_type.type_id = vbook.type_id
                                WHERE [glasses_nvic] = '" + id + "'";

            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Vehicle car = null;

            while (reader.Read())
            {
                car = new Vehicle();
                car.valuer_book_code_id = reader.GetValue(0).ToString();
                car.valuer_book_code = reader.GetValue(1).ToString();
                car.valuer_book_conv = reader.GetValue(2).ToString();
                car.no_of_doors = reader.GetValue(3).ToString();
                car.no_of_gears = reader.GetValue(4).ToString();
                car.engine_capacity = reader.GetValue(5).ToString();
                car.cylinders = reader.GetValue(6).ToString();
                car.make_year_full = reader.GetValue(7).ToString();
                car.month_group = reader.GetValue(8).ToString();
                car.year_group = reader.GetValue(9).ToString();
                car.release_month = reader.GetValue(10).ToString();
                car.release_year = reader.GetValue(11).ToString();
                car.engine_size = reader.GetValue(12).ToString();
                car.gross_weight = reader.GetValue(13).ToString();
                car.payload = reader.GetValue(14).ToString();
                car.gross_combination_mass = reader.GetValue(15).ToString();
                car.glasses_nvic = reader.GetValue(16).ToString();
                car.glasses_mrrp = reader.GetValue(17).ToString();
                car.glasses_trade_low = reader.GetValue(18).ToString();
                car.glasses_average_kms = reader.GetValue(19).ToString();
                car.glasses_km_category = reader.GetValue(20).ToString();
                car.glasses_trade_medium = reader.GetValue(21).ToString();
                car.VIN_Mask = reader.GetValue(22).ToString();
                car.Engine_Mask = reader.GetValue(23).ToString();
                car.RBC_Avg_wholesale = reader.GetValue(24).ToString();
                car.make_code = reader.GetValue(25).ToString();
                car.make_description = reader.GetValue(26).ToString();
                car.model_code = reader.GetValue(27).ToString();
                car.model_description = reader.GetValue(28).ToString();
                car.auc_type_code = reader.GetValue(29).ToString();
                car.auc_type_description = reader.GetValue(30).ToString();
            }

            myConnection.Close();

            return car;
        }
    }
}
