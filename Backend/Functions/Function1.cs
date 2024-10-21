using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Backend;
using Backend.Interfaces;

namespace Backend.Functions
{
    public class Function1
    {
        private readonly IUserService _userService;

        public Function1(IUserService userService)
        {
            _userService = userService;
        }

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

		[FunctionName("GetMessages")]
		public static async Task<IActionResult> GetMessages([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "messages")] HttpRequest req, ILogger log)
		{
			log.LogInformation(">>> Called GetMessages with GET request");

			//Simulate async process
			return await Task.Run(() =>
			{
				var msg = new Message { Data = "Seed is Working!" };
				return new OkObjectResult(msg);
			});
		}

        [FunctionName("GetUsers")]
		public async Task<IActionResult> GetUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "users")] HttpRequest req, ILogger log)
		{
            Console.WriteLine("┌───────────────────────┐");
            Console.WriteLine("│ ██ Function1.GetUsers │");
            Console.WriteLine("└───────────────────────┘");

			log.LogInformation(">>> Called GetUsers with GET request");

			return new OkObjectResult(await _userService.GetAllUsers());
		}
	}
}
