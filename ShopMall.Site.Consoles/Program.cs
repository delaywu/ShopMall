using Microsoft.Extensions.Configuration;
using ShopMall.Site.Infrastructure.DapperExtensions;
using ShopMall.Site.Infrastructure.Helper;
using System;

namespace ShopMall.Site.Consoles
{
    class Program
    {
        static void Main(string[] args)
        {

            //WhereClip whereClip = new WhereClip();

            //whereClip.And(Oper.Eq("name", "delaywu", "n")).And(Oper.Eq("age", "23", "age"));

            //Console.WriteLine(whereClip.WhereSql);

            string read = ConfigurationManager.Configuration.GetConnectionString("Read");
            Console.WriteLine(read);

            string write = ConfigurationManager.Configuration.GetConnectionString("Write");
            Console.WriteLine(write);

            Console.WriteLine(Guid.NewGuid().ToString());
        }
    }
}
