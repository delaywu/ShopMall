using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ShopMall.Site.Infrastructure.Helper
{
    public static class ConfigurationManager
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder()
                                                 .SetBasePath(Directory.GetCurrentDirectory())
                                                 .AddJsonFile("appsettings.json").Build();
        }

        //public static T GetAppSettings<T>(string fileName, string key) where T : class
        //{
        //    if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(key)) return null;

        //    var baseDir = AppContext.BaseDirectory + "json/";
        //    var currentClassDir = baseDir;

        //    IConfiguration config = new ConfigurationBuilder()
        //        .SetBasePath(currentClassDir)
        //        .Add(new JsonConfigurationSource
        //        {
        //            Path = fileName,
        //            Optional = false,
        //            ReloadOnChange = true
        //        }).Build();


        //}
    }
}
