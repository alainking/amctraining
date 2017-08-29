using Microsoft.Owin.Hosting;
using System;

namespace AMC.WebApi
{
  class Program
  {
    static void Main(string[] args)
    {
      StartUp();
    }

    private static void StartUp()
    {
      var options = WebAppOptions();
      // Start OWIN host
      using (WebApp.Start<Startup>(options))
      {
        Console.WriteLine("Press [Enter] to exit ...");
        Console.ReadLine();
      }
    }

    private static StartOptions WebAppOptions()
    {
      var options = new StartOptions();
      options.Urls.Add("http://*:11000");
      return options;
    }
  }
}
