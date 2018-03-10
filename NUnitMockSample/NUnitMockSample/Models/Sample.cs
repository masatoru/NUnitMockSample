using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitMockSample.Models
{
    public class Sample
    {
        public void Method()
        {
            try
            {
                Console.WriteLine("Hello");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message={ex.Message}");
            }
        }
    }
}
