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
using Backend.Repository;
using System.Collections.Generic;
using Backend.Models;

namespace Backend.Functions
{
    public class StateFunction
    {
        private readonly IStateRepository _StateRepository;
        private ILogger _Logger;

        public StateFunction(IStateRepository stateRepository, ILoggerFactory loggerFactory)
        {
            _StateRepository = stateRepository;
            _Logger = loggerFactory.CreateLogger(nameof(StateFunction));
        }

        [ProducesResponseType(typeof(List<State>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        [FunctionName("GetStates")]
		public async Task<IActionResult> GetStates([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetStates")] HttpRequest req, ILogger log)
		{
            try
            {
                Console.WriteLine("┌────────────────────────────┐");
                Console.WriteLine("│ ██ StateFunction.GetStates │");
                Console.WriteLine("└────────────────────────────┘");

			    log.LogInformation(">>> Called GetStates with GET request");
			    return new OkObjectResult(await _StateRepository.GetStatesAsync());
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in GetStates: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            
		}
	}
}
