//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Data.SqlClient;

//namespace TechApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class InventoryController : ControllerBase
//    {
//        public ActionResult SaveInventoryData()
//        {
//            SqlConnection connecion = new SqlConnection
//            {
//                ConnectionString = "Server=DESKTOP-2Q7HSLN\\SQLEXPRESS;Password=771143849;User Id=sa;Database=techDb;TrustServerCertificate=True;Persist Security Info=True;"
//            };

//            SqlCommand command = new SqlCommand
//            {
//                CommandText = "",
//                CommandType = System.Data.CommandType.StoredProcedure,
//                Connection = connecion
//            };
//            connecion.Open();
//            command.ExecuteNonQuery();
//            connecion.Close();

//                return Ok("Inventory Data Saved!");
//        }
//    }
//}
