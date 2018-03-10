﻿using NUnit.Framework;
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
        public void Modelのメソッドの例外を受け取る()
        {
            var sample = new Mock<ISample>() {CallBase = true};
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
            Assert.That(async () => await vm.ViewModelAsyncMethod(), Throws.TypeOf<HttpRequestException>());
        }

        /// <summary>
        /// ReactivePropertyのComandを実行
        /// ただし、HttpRequestExceptionは取得できない
        /// </summary>
        [Test]
        public void Asyncのコマンドを実行するけどExceptionを受け取れず()
        {
            var sample = new Mock<ISample>() {CallBase = true};
            sample.Setup(m => m.MethodAsync())
                .ThrowsAsync(new HttpRequestException());
//                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
            Assert.IsTrue(vm.SampleMethodAsyncCommand.CanExecute());

            // 例外を受け取れない !!! no exception thrown !!!
            Assert.That(() => vm.SampleMethodAsyncCommand.Execute(), Throws.TypeOf<HttpRequestException>());
        }

        /// <summary>
        /// ReactivePropertyのComandを実行 SubscribeはWaitで待つ
        /// AggregateExceptionが戻る
        /// </summary>
        [Test]
        public void Waitでコマンドを実行するとAggregateExceptionを受け取る()
        {
            var sample = new Mock<ISample>() {CallBase = true};
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            var vm = new SampleViewModel(sample.Object);
            Assert.IsTrue(vm.SampleMethodWaitCommand.CanExecute());
            Assert.That(() => vm.SampleMethodWaitCommand.Execute(), Throws.TypeOf<AggregateException>());

            try
            {
                vm.SampleMethodWaitCommand.Execute();
            }
            catch (AggregateException ex)
            {
                Assert.AreEqual(1, ex.InnerExceptions.Count);
                Assert.AreEqual(typeof(HttpRequestException), ex.InnerExceptions[0].GetType());
            }
        }
    }
}