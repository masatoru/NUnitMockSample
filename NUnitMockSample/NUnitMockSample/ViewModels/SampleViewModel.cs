using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnitMockSample.Models;
using Reactive.Bindings;

namespace NUnitMockSample.ViewModels
{
    public class SampleViewModel
    {
        private ISample Sample { get; set; }
        public ReactiveCommand SampleMethodAsyncCommand { get; }
        public ReactiveCommand SampleMethodCommand { get; }

        public SampleViewModel(ISample sample)
        {
            Sample = sample;
            SampleMethodAsyncCommand = new ReactiveCommand();
            SampleMethodAsyncCommand.Subscribe(async () => await ViewModelAsyncMethod());

            SampleMethodCommand = new ReactiveCommand();
            SampleMethodCommand.Subscribe(() => ViewModelAsyncMethod().Wait());
        }

        public async Task ViewModelAsyncMethod()
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