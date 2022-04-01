using System;
using System.Device.Gpio;
using System.Threading;

namespace GPIOConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // GPIO 17 which is physical pin 11
            int ledPin1 = 17;
            GpioController controller = new GpioController();
            // Sets the pin to output mode so we can switch something on
            controller.OpenPin(ledPin1, PinMode.Output);

            int lightTimeInMilliseconds = 5000;
            int dimTimeInMilliseconds = 200;
            controller.Write(ledPin1, PinValue.Low);
            //Thread.Sleep(dimTimeInMilliseconds);
            //while (true)
            //{
            Console.WriteLine($"LED1 on for {lightTimeInMilliseconds}ms");
                // turn on the LED
                controller.Write(ledPin1, PinValue.High);
                Thread.Sleep(lightTimeInMilliseconds);
                //Console.WriteLine($"LED1 off for {dimTimeInMilliseconds}ms");
                // turn off the LED
                //controller.Write(ledPin1, PinValue.Low);
                //Thread.Sleep(dimTimeInMilliseconds);
            //}
        }
    }
}
