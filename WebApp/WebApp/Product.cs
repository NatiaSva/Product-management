using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    // Class representing the product details

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public int SupplierID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice{ get; set; }
        public  short UnitsInStock{ get; set; }
        public short UnitsOnOrder  { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
