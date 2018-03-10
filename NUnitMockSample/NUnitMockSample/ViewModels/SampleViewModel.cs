using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnitMockSample.Models;

namespace NUnitMockSample.ViewModels
{
    public class SampleViewModel
    {
        private ISample Sample { get; set; }

        public SampleViewModel(ISample sample)
        {
            Sample = sample;
        }

        public async Task ViewModelMethod()
        {
            try
            {
                await Sample.MethodAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException Message={ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message={ex.Message}");
            }
        }
    }
}
