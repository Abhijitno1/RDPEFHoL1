using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            LINQPractice();
            Console.ReadLine();
        }

        static void CoreTest()
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
        }

        static void LINQPractice()
        {
            using (var context = new NorthwindEntities())
            {
                var catWiseProdSalesO = from p in context.Product_Sales_for_1997
                                        group p by p.CategoryName into grp
                                        select new { CategoryName = grp.Key, ProductSales = grp };

                var catWiseProdSales = context.Product_Sales_for_1997.GroupBy(p => p.CategoryName, (key, grp) => new { CategoryName = key, ProductSales = grp });
                
                foreach (var cat in catWiseProdSales)
                {
                    Console.WriteLine("CategoryName: {0}", cat.CategoryName);
                    foreach (var ps in cat.ProductSales)
                    {
                        Console.WriteLine("Product Name: {0}, Sales: {1}", ps.ProductName, ps.ProductSales);
                    }
                }
                Console.WriteLine(string.Join("", Enumerable.Repeat("=", 70)));
                
                var expensiveProdByCat = from p in context.Products
                          group p by p.Category.CategoryName into grp
                          let maxPrice = grp.Max(gg => gg.UnitPrice)
                          select new { CategoryName = grp.Key, MostExpensiveProds = grp.Where(pp => pp.UnitPrice == maxPrice) };

                foreach (var cat in expensiveProdByCat)
                {
                    Console.WriteLine("CategoryName: {0}", cat.CategoryName);
                    foreach (var ep in cat.MostExpensiveProds)
                    {
                        Console.WriteLine("Product Name: {0}, Price: {1}", ep.ProductName, ep.UnitPrice);
                    }
                }
                Console.WriteLine(string.Join("", Enumerable.Repeat("=", 70)));

                Console.Write("Which number's Table do you want to create?");
                var padha = int.Parse(Console.ReadLine());
                Enumerable.Range(1, 10).Select(n => n * padha).ToList().ForEach(cn => Console.WriteLine(cn));

                var entitySQL = "SELECT VALUE prod FROM NorthwindEntities.Products AS prod WHERE prod.Category.CategoryName = 'Seafood'";
                var objectContext = (context as IObjectContextAdapter).ObjectContext;
                var products = objectContext.CreateQuery<Product>(entitySQL);
                if (products.Any())
                    Console.WriteLine("Found Product using EntitySQL: {0}", products.First().ProductName);
            }
        }
    }
}
