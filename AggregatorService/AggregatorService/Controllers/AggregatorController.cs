using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AggregatorService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polly.CircuitBreaker;
using Steeltoe.Common.Discovery;

namespace AggregatorService.Controllers
{
    public class AggregatorController : Controller
    {
        DiscoveryHttpClientHandler handler;
        private readonly IHttpClientFactory _factory;

        public AggregatorController(IDiscoveryClient client, IHttpClientFactory factory)
        {
            handler = new DiscoveryHttpClientHandler(client);
            _factory = factory;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Aggregator/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ActionResult retVal = null;
            var client = new HttpClient(handler, false);
            try
            {
                if(id>4)
                {
                    id = 1;
                }
                UserOrderModel userOrder = new UserOrderModel();
                
                var response = await client.GetAsync("http://10.44.7.243/user/" + id);

                UserModel data = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

                var orderResponse = await client.GetAsync("http://10.44.15.88/order/" + id);
                var test = await orderResponse.Content.ReadAsStringAsync();
                OrderModel[] orders = JsonConvert.DeserializeObject<OrderModel[]>(await orderResponse.Content.ReadAsStringAsync());

                userOrder.User = data;
                userOrder.Order = orders;
                retVal = View("~/Views/Aggregator/Aggregator.cshtml", userOrder);
            }
            catch (BrokenCircuitException e)
            {
                retVal = View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = "CE001", Error = " Broken Circuit Exception: The circuit is now open. Please try again after a few minutes" });
            }
            return retVal;
        }

        public async Task<string> Error()
        {
            var data = await GetSomeThingFromOrderApi();

            return "Order API has thrown error.";
        }

        private async Task<string> GetSomeThingFromOrderApi()
        {
            var client = _factory.CreateClient("orderApi");

            var requestMsg = new HttpRequestMessage(HttpMethod.Get, "http://10.44.15.88/order");

            var responseMsg = await client.SendAsync(requestMsg);

            var data = await responseMsg.Content.ReadAsStringAsync();

            return data;
        }
    }
}