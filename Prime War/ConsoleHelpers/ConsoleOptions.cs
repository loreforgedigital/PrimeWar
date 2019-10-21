using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeWarEngine.ConsoleHelpers
{
    public class ConsoleOptions<T>
        {
            public static T GetResponseToOptions(IEnumerable<T> options, string[] optionNames = null)
            {
                Console.WriteLine("Please Select an option");
                for(int i = 0; i < options.Count(); i++)
                {
                if (optionNames != null && optionNames.Length >= i)
                    Console.WriteLine((i + 1) + ") " + optionNames[i]);
                else
                    Console.WriteLine((i + 1) + ") " + options.ElementAt(i));
            }
                string response = Console.ReadLine();
            var result = options.FirstOrDefault(o => o.ToString() == response);
            if (int.TryParse(response, out int x) && x >= 1 && x <= options.Count())
            {
                return options.ElementAt(x - 1);
            }
            else if (result != null)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid Response. Please select an option by its name or number.");
                    return GetResponseToOptions(options);
                }
            }
        public static int GetResponseIndexOrDoneToOptions(IEnumerable<T> options, string[] optionNames = null)
        {
            Console.WriteLine("Please Select an option");
            for (int i = 0; i < options.Count(); i++)
            {
                if(optionNames != null && optionNames.Length >= i)
                Console.WriteLine((i + 1) + ") " + optionNames[i]);
                else
                    Console.WriteLine((i + 1) + ") " + options.ElementAt(i));
            }
            Console.WriteLine((options.Count() + 1) + ") Done"); 
            string response = Console.ReadLine();
            var result = options.FirstOrDefault(o => o.ToString() == response);
            if (result != null)
            {
                return options.ToList().IndexOf(result);
            }
            else if (int.TryParse(response, out int x) && x >= 1 && x <= options.Count()+1)
            {
                return x - 1;
            }
            else
            {
                Console.WriteLine("Invalid Response. Please select an option by its name or number.");
                return GetResponseIndexOrDoneToOptions(options);
            }
        }
    }

    
}
