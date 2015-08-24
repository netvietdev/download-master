using DownloadMaster.Common;
using DownloadMaster.CustomLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Rabbit.SerializationMaster;
using Rabbit.SerializationMaster.JsonNet;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DownloadMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplyApplicationConfig();

            var services = GetConfiguredServices();

            foreach (var serviceOption in services)
            {
                var serviceType = Type.GetType(serviceOption.ServiceType);
                if (serviceType == null)
                {
                    continue;
                }

                try
                {
                    var downloadService = (IDownloadService)Activator.CreateInstance(serviceType);
                    downloadService.Download(serviceOption);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                Console.WriteLine("--------------");
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void ApplyApplicationConfig()
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                // 201508031500
                DateFormatString = "yyyyMMddHHmm",

            };
            serializerSettings.Converters.Add(new StringEnumConverter());

            SerializationContext.Current.Initialize(new JsonSerializationStrategy(serializerSettings));
        }

        private static IList<DownloadServiceOption> GetConfiguredServices()
        {
            var cfgFile = ConfigurationManager.AppSettings["ConfigFile"];
            return SerializationHelper.DeserializeFrom<List<DownloadServiceOption>>(cfgFile);
        }
    }
}
