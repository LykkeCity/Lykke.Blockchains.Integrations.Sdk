﻿using System;
using System.Threading.Tasks;
using Lykke.Bil2.Client.BlocksReader;
using Lykke.Bil2.Client.BlocksReader.Services;
using Lykke.Bil2.Contract.BlocksReader.Commands;
using Lykke.Common;
using Lykke.Logs;
using Lykke.Logs.Loggers.LykkeConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlocksReaderExampleClient
{
    internal sealed class Program
    {
        // ReSharper disable once UnusedParameter.Global
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var services = new ServiceCollection();

            services.AddSingleton(s => LogFactory.Create().AddUnbufferedConsole());

            Console.WriteLine("Registering blocks reader client...");

            services.AddBlocksReaderClient(options =>
            {
                options.BlockEventsHandlerFactory = s => new BlockEventsHandler();
                options.RabbitMqConnString = config["RabbitMqConnString"];
                options.AddIntegration("Steem");
                options.AddIntegration("EthereumClassic");
                options.RabbitVhost = AppEnvironment.EnvInfo;
            });

            using (var serviceProvider = services.BuildServiceProvider())
            {
                Console.WriteLine("Press any key to start.");

                Console.ReadKey(true);

                var client = serviceProvider.GetService<IBlocksReaderClient>();

                Console.WriteLine("Starting client...");

                client.Initialize();
                client.StartListening();

                var apiFactory = serviceProvider.GetService<IBlocksReaderApiFactory>();

                var steemApi = apiFactory.Create("Steem");
                var ethereumClassicFactory = apiFactory.Create("EthereumClassic");

                await steemApi.SendAsync(new ReadBlockCommand(1000), Guid.NewGuid().ToString());
                await ethereumClassicFactory.SendAsync(new ReadBlockCommand(2000), Guid.NewGuid().ToString());

                Console.WriteLine("Press any key to exit.");

                Console.ReadKey(true);
            }
        }
    }
}
