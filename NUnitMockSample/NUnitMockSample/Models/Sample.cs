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
            Console.WriteLine("Hello");
        }

        public async Task MethodAsync()
        {
            await Task.Delay(1);
            Console.WriteLine("Hello　Async");
        }
    }
}