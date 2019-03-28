using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
// MS: https://docs.microsoft.com/en-us/aspnet/web-api/overview/older-versions/self-host-a-web-api

namespace SelfHostedWebServiceDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = new HttpSelfHostConfiguration("http://27.0.0.1:8080");

			config.Routes.MapHttpRoute(
				"API Default", "api/{controller}/{id}",
				new { id = RouteParameter.Optional });

			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

			using (HttpSelfHostServer server = new HttpSelfHostServer(config))
			{
				server.OpenAsync().Wait();
				Console.WriteLine("Press Enter to quit.");
				Console.ReadLine();
			}
		}
	}
}
