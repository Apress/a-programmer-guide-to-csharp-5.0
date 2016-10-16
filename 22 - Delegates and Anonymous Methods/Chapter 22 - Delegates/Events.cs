using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chapter_22___Delegates
{
    class Events
    {
        public static void Boo(object o, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Boo");
            args.Cancel = true;
        }

        public static void HookToEvent()
        {
            Console.CancelKeyPress += Boo;
        }

        public static void UnhookFromEvent()
        {
            Console.CancelKeyPress -= Boo;
        }

        public static void HookToLambda()
        {
            Console.CancelKeyPress += (o, args) =>
                {
                    Console.WriteLine("Boo");
                    args.Cancel = true;
                };
        }

        public static void UnhookFromLambda()
        {
            Console.CancelKeyPress -= (o, args) =>
            {
                Console.WriteLine("Boo");
                args.Cancel = true;
            };
        }




        public static void Test()
        {
#if method
            Console.WriteLine("Method");
            Events.HookToEvent();

            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Unhook");
            Events.UnhookFromEvent();

            System.Threading.Thread.Sleep(5000);
#else

            Console.WriteLine("Lambda");
            Events.HookToLambda();

            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Unhook");
            Events.UnhookFromLambda();

            System.Threading.Thread.Sleep(5000);
#endif
        }
    }
}
