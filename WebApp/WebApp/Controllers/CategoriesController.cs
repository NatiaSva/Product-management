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
    public class CategoriesController : ControllerBase
    {
        //Receive a http request and execute a query string that retrieves all the categories from the databse and sends to the client.

        [HttpGet("pull")]

        public List<Category> Pull()
        {
            return DB.PullData<Category>("SELECT CategoryID, CategoryName, Description FROM Categories", 
                (dr) => new Category 
                {
                    CategoryID=dr.GetInt32(0),
                    CategoryName=dr.GetString(1),
                    Description=dr.GetString(2)
                });    
        }




    }
}
