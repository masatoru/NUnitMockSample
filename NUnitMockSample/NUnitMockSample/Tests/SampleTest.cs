using System;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using NUnitMockSample.Models;

namespace NUnitMockSample.Tests
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        public void MethodMockException()
        {
            var sample = new Mock<ISample>();
            sample.Setup(m => m.Method())
                .Throws(new Exception());

            Assert.Throws<Exception>(() => sample.Object.Method());
        }

        [Test]
        public void MethodMockHttpRequestException()
        {
            var sample = new Mock<ISample>();
            sample.Setup(m => m.Method())
                .Throws(new HttpRequestException());

            Assert.Throws<HttpRequestException>(() => sample.Object.Method());
        }

        [Test]
        public void MethodAsyncMockException()
        {
            var sample = new Mock<ISample>();
            sample.Setup(m => m.MethodAsync())
                .Throws(new Exception());

            Assert.Throws<Exception>(() => sample.Object.MethodAsync());
        }

        [Test]
        public void MethodAsyncMockHttpRequestException()
        {
            var sample = new Mock<ISample>() { CallBase = true };
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            // これでもOK
            //Assert.That(async () => await sample.Object.MethodAsync(), Throws.TypeOf<HttpRequestException>());
            Assert.Throws<HttpRequestException>(() => sample.Object.MethodAsync());
        }

    }
}