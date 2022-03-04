using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Interfaces;
using Revendor.Domain.Interfaces.Services;

namespace Application.UnitTests.Services
{
    [TestFixture]
    public abstract class ServiceTestBase<T> where T:class
    {
        protected abstract T Service { get; set; }
        protected Mock<IRepository> Repository;
        protected Mock<ILogger<T>> Logger;

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<IRepository>();
            Logger = new Mock<ILogger<T>>();
            AdditionalSetup();
        }

        protected virtual void AdditionalSetup()
        {
            
        }

    }
}