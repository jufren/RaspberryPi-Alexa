using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Device.Gpio;
using System.Threading;

namespace HelloWorld.Controllers
{
    public class LEDController : Controller
    {
        public void GPIO(string command)
        {
            // GPIO 17 which is physical pin 11
            int ledPin1 = 17;
            GpioController controller = new GpioController();
            // Sets the pin to output mode so we can switch something on
            controller.OpenPin(ledPin1, PinMode.Output);

          
            controller.Write(ledPin1, PinValue.Low);
            //while (true)
            //{

            // turn on the LED
            if (command == "on")
                {
                Console.WriteLine($"LED1 on ");
                controller.Write(ledPin1, PinValue.High);
                //    Thread.Sleep(lightTimeInMilliseconds);
                }
                if (command == "off")
                {
                    Console.WriteLine($"LED1 off ");

                    // turn off the LED
                    controller.Write(ledPin1, PinValue.Low);
                    //Thread.Sleep(dimTimeInMilliseconds);
                }
            //}
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
                SkillResponse rp = ResponseBuilder.Ask("Welcome to the LED control skill. You can say, tell led switch to turn on, or, tell led project to turn off", null);


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

                    return ResponseBuilder.Ask(new PlainTextOutputSpeech("Do you want to turn your LED on, or, off?"), new Reprompt("Do you want to turn your LED on, or, off"));
                }
                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("OnOffIntent"))
                {
                    // get the pull requests
                    // var pullrequests = CountPullRequests();

                    // if (pullrequests == 0)
                    //     return ResponseBuilder.Tell("You have no pull requests at this time.");
                    var onOff = intentRequest.Intent.Slots.GetValueOrDefault("OnOff").Value;
                    GPIO(onOff);
                    return ResponseBuilder.Tell("Turning your LED " + onOff + ".");
                }
            }
            // return Json("hello");
            //return "test";
            //string o = JsonConvert.SerializeObject(ResponseBuilder.Ask("I don't understand. Can you please try again?", null));
            return ResponseBuilder.Ask("I don't understand. Can you please try again?", null);

        }
    }
}
