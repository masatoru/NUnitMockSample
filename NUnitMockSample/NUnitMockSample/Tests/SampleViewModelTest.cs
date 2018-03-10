using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnitMockSample.Models;
using NUnitMockSample.ViewModels;

namespace NUnitMockSample.Tests
{
    [TestFixture]
    public class SampleViewModelTest
    {
        /// <summary>
        /// モデルのメソッドをMockで例外を返す→OK
        /// </summary>
        [Test]
        public void ModelMethodMockExceptionTest()
        {
            var sample = new Mock<ISample>() { CallBase = true };
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
//            Assert.Throws<HttpRequestException>(async () => await vm.ViewModelMethod(sample.Object));
            Assert.That(async () => await vm.ViewModelMethod(), Throws.TypeOf<HttpRequestException>());
        }
        /// <summary>
        /// ReactivePropertyのComandを実行
        /// ただし、HttpRequestExceptionは取得できない
        /// </summary>
        [Test]
        public void CommandMockExceptionTest()
        {
            var sample = new Mock<ISample>() { CallBase = true };
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
            Assert.IsTrue(vm.SampleMethodCommand.CanExecute());
            Assert.That(() => vm.SampleMethodCommand.Execute(), Throws.TypeOf<HttpRequestException>());
        }
    }
}
