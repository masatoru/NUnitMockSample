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
            var sample = new Mock<ISample>() {CallBase = true};
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
//            Assert.Throws<HttpRequestException>(async () => await vm.ViewModelMethod(sample.Object));
            Assert.That(async () => await vm.ViewModelAsyncMethod(), Throws.TypeOf<HttpRequestException>());
        }

        /// <summary>
        /// ReactivePropertyのComandを実行
        /// ただし、HttpRequestExceptionは取得できない
        /// </summary>
        [Test]
        public void CommandMockAsyncExceptionTest()
        {
            var sample = new Mock<ISample>() {CallBase = true};
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
            Assert.IsTrue(vm.SampleMethodAsyncCommand.CanExecute());
            Assert.That(() => vm.SampleMethodAsyncCommand.Execute(), Throws.TypeOf<HttpRequestException>());
        }

        /// <summary>
        /// ReactivePropertyのComandを実行 SubscribeはWaitで待つ
        /// AggregateExceptionが戻る
        /// </summary>
        [Test]
        public void CommandMockExceptionTest()
        {
            var sample = new Mock<ISample>() {CallBase = true};
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
            Assert.IsTrue(vm.SampleMethodCommand.CanExecute());
            Assert.That(() => vm.SampleMethodCommand.Execute(), Throws.TypeOf<AggregateException>());

            try
            {
                vm.SampleMethodCommand.Execute();
            }
            catch (AggregateException ex)
            {
                Assert.AreEqual(1, ex.InnerExceptions.Count);
                Assert.AreEqual(typeof(HttpRequestException), ex.InnerExceptions[0].GetType());
            }
        }
    }
}