using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnitMockSample.Models;

namespace NUnitMockSample
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
            var sample = new Mock<ISample>();
            sample.Setup(m => m.MethodAsync())
                .Throws(new HttpRequestException());

            Assert.Throws<HttpRequestException>(() => sample.Object.MethodAsync());
        }

    }
}