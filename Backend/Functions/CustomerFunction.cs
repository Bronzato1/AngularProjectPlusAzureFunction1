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
    public class CustomerFunction
    {
        private readonly ICustomerRepository _CustomerRepository;
        private ILogger _Logger;

        public CustomerFunction(ICustomerRepository customerRepository, ILoggerFactory loggerFactory)
        {
            _CustomerRepository = customerRepository;
            _Logger = loggerFactory.CreateLogger(nameof(CustomerFunction));
        }

        // GET api/customers
        [FunctionName("GetCustomers")]
		public async Task<IActionResult> GetCustomers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers")] HttpRequest req, ILogger log)
		{
            try
            {
                Console.WriteLine("┌──────────────────────────────────┐");
                Console.WriteLine("│ ██ CustomerFunction.GetCustomers │");
                Console.WriteLine("└──────────────────────────────────┘");

			    log.LogInformation(">>> Called GetCustomers with GET request");
			    return new OkObjectResult(await _CustomerRepository.GetCustomersAsync());
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in GetCustomers: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
		}

        // GET api/customers/page/10/10
        [FunctionName("GetCustomersPage")]
		public async Task<IActionResult> GetCustomersPage([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/page/{skip}/{take}")] HttpRequest req, ILogger log, int skip, int take)
		{
            try
            {
                Console.WriteLine("┌──────────────────────────────────────┐");
                Console.WriteLine("│ ██ CustomerFunction.GetCustomersPage │");
                Console.WriteLine("└──────────────────────────────────────┘");

			    log.LogInformation(">>> Called GetCustomersPage with GET request");
                var pagingResult = await _CustomerRepository.GetCustomersPageAsync(skip, take);
                req.HttpContext.Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());

                return new OkObjectResult(pagingResult.Records);
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in GetCustomersPage: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
		}

        // GET api/customers/5
        [FunctionName("GetCustomer")]
        public async Task<IActionResult> GetCustomer([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{id}")] HttpRequest req, ILogger log, int id)
        {
            try
            {
                Console.WriteLine("┌─────────────────────────────────┐");
                Console.WriteLine("│ ██ CustomerFunction.GetCustomer │");
                Console.WriteLine("└─────────────────────────────────┘");

                log.LogInformation(">>> Called GetCustomer with GET request");
                var customer = await _CustomerRepository.GetCustomerAsync(id);
                return new OkObjectResult(customer);
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in GetCustomer: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // POST api/customers
        [FunctionName("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, ILogger log, [FromBody]Customer customer)
        {
            try
            {
                Console.WriteLine("┌────────────────────────────────────┐");
                Console.WriteLine("│ ██ CustomerFunction.CreateCustomer │");
                Console.WriteLine("└────────────────────────────────────┘");

                log.LogInformation(">>> Called CreateCustomer with POST request");

                // https://stackoverflow.com/questions/56381450/how-to-validate-parameters-sent-to-an-azure-function
                
                var newCustomer = await _CustomerRepository.InsertCustomerAsync(customer);
                if (newCustomer == null)
                {
                    return new BadRequestResult();
                }
                return new OkObjectResult(newCustomer);
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in CreateCustomer: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // PUT api/customers/5
        // [ValidateAntiForgeryToken]
        [FunctionName("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "customers/{id}")] HttpRequest req, ILogger log, [FromBody]Customer customer)
        {
            try
            {
                Console.WriteLine("┌────────────────────────────────────┐");
                Console.WriteLine("│ ██ CustomerFunction.UpdateCustomer │");
                Console.WriteLine("└────────────────────────────────────┘");

                log.LogInformation(">>> Called UpdateCustomer with POST request");

                // https://stackoverflow.com/questions/56381450/how-to-validate-parameters-sent-to-an-azure-function
                
                var status = await _CustomerRepository.UpdateCustomerAsync(customer);
                if (!status)
                {
                    return new BadRequestResult();
                }
                return new OkObjectResult(customer);
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in UpdateCustomer: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE api/customers/5
        //[ValidateAntiForgeryToken]
        [FunctionName("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "customers/{id}")] HttpRequest req, ILogger log, int id)
        {
            try
            {
                Console.WriteLine("┌────────────────────────────────────┐");
                Console.WriteLine("│ ██ CustomerFunction.DeleteCustomer │");
                Console.WriteLine("└────────────────────────────────────┘");

                log.LogInformation(">>> Called DeleteCustomer with POST request");

                // https://stackoverflow.com/questions/56381450/how-to-validate-parameters-sent-to-an-azure-function
                
                var status = await _CustomerRepository.DeleteCustomerAsync(id);
                if (!status)
                {
                    return new BadRequestResult();
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                _Logger.LogError(">>> Exception catch in DeleteCustomer: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
	}
}
