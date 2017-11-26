using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //Display the CustomerId and CompanyName of customers who have placed orders after the 1st of January, 1998
            using (var context = new NorthwindEntities())
            {
                var custWithOrders = context.Orders.Where(ord => ord.OrderDate > new DateTime(1998, 1, 1))
                    .Select(ord => new { ord.Customer.CustomerID, ord.Customer.CompanyName, ord.OrderDate });
                foreach (var cust in custWithOrders)
                {
                    Console.WriteLine("{0} from {1} ordered items on {2}"
                        , cust.CustomerID, cust.CompanyName, cust.OrderDate);
                }
                //var spResult = context.Database.SqlQuery<CustOrdersOrders_Result>("exec CustOrdersOrders @CustomerID", new SqlParameter("@CustomerID", "ALFKI"));
                //var order = spResult.FirstOrDefault();

                var result2 = context.CustOrdersOrders("ALFKI");
                var order = result2.FirstOrDefault();

                //context.Regions.Add(new Region { RegionID = 1, RegionDescription = "Pune" });
                //context.Territories.Add(new Territory { TerritoryID = "0001", TerritoryDescription = "Vadagaon", RegionID = 1, State = "MS", Country = "India" });
                //context.SaveChanges();
            }
            Console.ReadLine();
        }
    }
}
