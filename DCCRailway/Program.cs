using DCCRailway.Railway;

var railway = RailwayManager.Load() ?? RailwayManager.New();

//var services = new ServiceCollection();
//services.AddTransient<ILogger, DCCRailway.Common.Helpers.Logger>();
//var serviceProvider = services.BuildServiceProvider(); //ioc container
//var railway = serviceProvider.GetService<IRailwayManager>();
Console.WriteLine(railway.Name);