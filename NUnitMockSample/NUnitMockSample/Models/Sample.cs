using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NUnitMockSample.Models
{
    public interface ISample
    {
        void Method();
        Task MethodAsync();
    }

    public class Sample : ISample
    {
        public void Method()
        {
            try
            {
                Console.WriteLine("Hello");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException Message={ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message={ex.Message}");
            }
        }

        public async Task MethodAsync()
        {
            try
            {
                await Task.Delay(1);
                Console.WriteLine("Hello");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException Message={ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message={ex.Message}");
            }
        }
    }
}
