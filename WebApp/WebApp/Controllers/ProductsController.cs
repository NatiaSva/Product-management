using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //Receive an http request and execute a query string that retrieves all the information of the products from databse and sends to the customer.
        [HttpGet("pull")]

        public List<Product> pull(int categoryId)
        {
            return DB.PullData<Product>("SELECT ProductID, ProductName, Products.SupplierID AS 'SupplierID', Suppliers.CompanyName AS 'SupplierName',QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, Products.CategoryID AS 'CategoryID',Categories.CategoryName AS 'CategoryName' FROM Products INNER JOIN Suppliers ON (Products.SupplierID=Suppliers.SupplierID) INNER JOIN Categories ON (Products.CategoryID=Categories.CategoryID) WHERE Products.CategoryID=@CategoryID",(dr)=>
            new Product 
            {
                ProductID=dr.GetInt32(0),
                ProductName=dr.GetString(1),
                SupplierID=dr.GetInt32(2),
                SupplierName=dr.GetString(3),
                QuantityPerUnit=dr.GetStringOrNull(4),
                UnitPrice=dr.GetDecimal(5),
                UnitsInStock=dr.GetInt16(6),
                UnitsOnOrder=dr.GetInt16(7),
                ReorderLevel=dr.GetInt16(8),
                Discontinued=dr.GetBoolean(9),
                CategoryID=dr.GetInt32(10),
                CategoryName=dr.GetString(11)
            },(cmd)=>cmd.Parameters.AddWithValue("@CategoryID",categoryId));

        }


        //Receive a http request and execute a query string that deletes the product from the database.

        [HttpGet("delete")]

        public bool Modify(int id)
        {
            return DB.Modify("DELETE FROM Products WHERE ProductID=@ProductID", (cmd) =>
                cmd.AddWithValue("@ProductID",id))==1;
        }


        //Receive a http request and execute a query string that updates the specific product in the database.

        [HttpPost("update")]

        public bool Update(Product product)
        {
            return DB.Modify("UPDATE Products SET ProductName = @ProductName, SupplierID=@SupplierID, QuantityPerUnit=@QuantityPerUnit, UnitPrice = @UnitPrice, UnitsInStock=@UnitsInStock,UnitsOnOrder = @UnitsOnOrder, ReorderLevel=@ReorderLevel, Discontinued = @Discontinued WHERE ProductID=@ProductID",
                (cmd) => {
                    cmd
                    .AddWithValue("@ProductName", product.ProductName)
                    .AddWithValue("@SupplierID", product.SupplierID)
                    .AddWithValue("@QuantityPerUnit", product.QuantityPerUnit)
                    .AddWithValue("@UnitPrice", product.UnitPrice)
                    .AddWithValue("@UnitsInStock", product.UnitsInStock)
                    .AddWithValue("@UnitsOnOrder", product.UnitsOnOrder)
                    .AddWithValue("@ReorderLevel", product.ReorderLevel)
                    .AddWithValue("@Discontinued", product.Discontinued)
                    .AddWithValue("@ProductID", product.ProductID);
                }) == 1;
        }


        //Receive an http request and execute a query string that puts all the product details into a database
        [HttpPost("insert")]

        public bool Insert(Product product)
        {
            return DB.Modify("INSERT INTO Products (ProductName,SupplierID, QuantityPerUnit, UnitPrice, UnitsInStock,UnitsOnOrder, ReorderLevel, Discontinued,CategoryID) VALUES (@ProductName,@SupplierID, @QuantityPerUnit, @UnitPrice, @UnitsInStock,@UnitsOnOrder, @ReorderLevel, @Discontinued, @CategoryID)",
                (cmd) => {
                    cmd
                    .AddWithValue("@ProductName", product.ProductName)
                    .AddWithValue("@SupplierID", product.SupplierID)
                    .AddWithValue("@QuantityPerUnit", product.QuantityPerUnit)
                    .AddWithValue("@UnitPrice", product.UnitPrice)
                    .AddWithValue("@UnitsInStock", product.UnitsInStock)
                    .AddWithValue("@UnitsOnOrder", product.UnitsOnOrder)
                    .AddWithValue("@ReorderLevel", product.ReorderLevel)
                    .AddWithValue("@Discontinued", product.Discontinued)
                    .AddWithValue("@CategoryID",product.CategoryID);
                }) == 1;
        }


    }

    //Extention method 
    public static class MyExtention
    {
        public static SqlCommand AddWithValue(this SqlCommand cmd, string parameterName, object value)
        {
            if (value == null)
                cmd.Parameters.AddWithValue(parameterName, DBNull.Value);
            else
                cmd.Parameters.AddWithValue(parameterName, value);
            return cmd;
        }

        public static string GetStringOrNull(this SqlDataReader dr,int i)
        {
            return dr.IsDBNull(i) ? null : dr.GetString(i);
        }
    }


}
