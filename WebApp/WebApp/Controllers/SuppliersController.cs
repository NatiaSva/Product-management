using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        //Receiving an http request and executing a query string that pulling the supplier of the product
        [HttpGet("pull")]
        public List<Supplier> Pull()
        {
            return DB.PullData<Supplier>("SELECT SupplierID, CompanyName,ContactName, ContactTitle, Address, City FROM Suppliers", 
                (dr) =>new Supplier {
                    SupplierID=dr.GetInt32(0),
                    CompanyName=dr.GetString(1),
                    ContactName=dr.GetString(2),
                    ContactTitle=dr.GetString(3),
                    Address=dr.GetString(4),
                    City=dr.GetString(5)
                });
        }



    }
}
