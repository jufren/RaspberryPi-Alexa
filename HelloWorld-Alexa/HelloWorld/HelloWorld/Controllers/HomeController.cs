using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public object Index([FromBody] SkillRequest input)
        {
            var requestType = input.GetRequestType();
            SkillResponse response = new SkillResponse();
            Console.WriteLine("requestType:" + typeof(LaunchRequest).Name);
            // return a welcome message
            if (requestType == typeof(LaunchRequest))
            {
                SkillResponse rp = ResponseBuilder.Ask("Welcome to to the alexa skill. You can say your name, or how are you?", null);
               

                return rp;
                
            }

            // return information from an intent
            else if (requestType == typeof(IntentRequest))
            {
                // do some intent-based stuff
                var intentRequest = input.Request as IntentRequest;

                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("AMAZON.FallbackIntent"))
                {

                    return ResponseBuilder.Ask(new PlainTextOutputSpeech("What is your name?"), new Reprompt("Can you tell me your name?"));
                }
                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("HelloWorldIntent"))
                {
                    // get the pull requests
                   // var pullrequests = CountPullRequests();

                   // if (pullrequests == 0)
                   //     return ResponseBuilder.Tell("You have no pull requests at this time.");

                    return ResponseBuilder.Tell("Hello " + intentRequest.Intent.Slots.GetValueOrDefault("firstname").Value + ". Great job on running your first alexa skill.");
                }
            }
            // return Json("hello");
            //return "test";
            //string o = JsonConvert.SerializeObject(ResponseBuilder.Ask("I don't understand. Can you please try again?", null));
            return ResponseBuilder.Ask("I don't understand. Can you please try again?", null);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

        [HttpGet]
        public int CountPullRequests()
        {
            /*var creds = new InMemoryCredentialStore(new Credentials(token));
            var client = new GitHubClient(new ProductHeaderValue(product_name), creds);
            var pullrequests = client.PullRequest.GetAllForRepository(owner, repo).Result;*/
            return 0;
        }
    }
}
