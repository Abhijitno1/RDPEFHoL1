using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstDBLayer;

namespace CodeFirstConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ListTest();
        }

        static void DataAnnotationTest()
        {
            var dest = new HolidayDestination
            {
                Name = "Indo",
                Description = "Ecotourism Place",
                Country = "Indonesia"
            };
            using (var context= new ProductContext())
            {
                context.Destinations.Add(dest);
                context.SaveChanges();                
            }
        }

        static void TransactionTest()
        {
            using (var context = new ProductContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        UpdateTest(context);
                        DeleteTest(context);
                        context.Database.ExecuteSqlCommand("Update Products set Name = 'XYZ update' where Name='XYZ'");
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Console.WriteLine(ex.Message ?? ex.InnerException.Message);
                    }
                }
            }
            ListTest();
        }

        static void AddTest(ProductContext context)
        {
            //First add few Products
            context.Products.AddRange(new[] {
                        new Product { Name = "ABC" },
                        new Product { Name = "PQR" },
                        new Product { Name = "XYZ" }
                    });
            context.SaveChanges();
        }

        static void UpdateTest(ProductContext context)
        {
            var prod = context.Products.FirstOrDefault(p => p.Name.Equals("ABC"));
            prod.Name = "ABC update";
            context.SaveChanges();
        }
        static void DeleteTest(ProductContext context)
        {
            var prod = context.Products.FirstOrDefault(p => p.Name.Equals("PQR"));
            context.Products.Remove(prod);
            context.SaveChanges();
        }

        static void ListTest()
        {
            using (var context = new ProductContext())
            {
                //Then display them one by one
                var prodList = from p in context.Products
                               select p;
                foreach (var item in prodList)
                {
                    Console.WriteLine(item.ProductId + "\t" + item.Name);
                }
            }

        }
    }
}
