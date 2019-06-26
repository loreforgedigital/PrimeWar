using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeWarEngine.ConsoleHelpers
{
    public class ConsoleOptions<T>
        {
            public static T GetResponseToOptions(List<T> options)
            {
                Console.WriteLine("Please Select an option");
                for(int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine((i + 1) + ") " + options[i]);
                }
                string response = Console.ReadLine();
            var result = options.FirstOrDefault(o => o.ToString() == response);
                if (result != null)
                {
                    return result;
                }
                else if (int.TryParse(response, out int x) && x >=1 && x <= options.Count)
                {
                    return options[x-1];
                }
                else
                {
                    Console.WriteLine("Invalid Response. Please select an option by its name or number.");
                    return GetResponseToOptions(options);
                }
            }
        public static int GetResponseIndexOrDoneToOptions(List<T> options)
        {
            Console.WriteLine("Please Select an option");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine((i + 1) + ") " + options[i]);
            }
            Console.WriteLine((options.Count + 1) + ") Done"); 
            string response = Console.ReadLine();
            var result = options.FirstOrDefault(o => o.ToString() == response);
            if (result != null)
            {
                return options.IndexOf(result);
            }
            else if (int.TryParse(response, out int x) && x >= 1 && x <= options.Count+1)
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
