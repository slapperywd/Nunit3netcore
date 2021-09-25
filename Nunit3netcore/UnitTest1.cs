using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Logging;
using NUnit.Framework;


namespace Nunit3netcore
{
    [Parallelizable(ParallelScope.Children)]
    public class Tests
    {
        public ILogger Logger { get; set; }

        [SetUp]
        public void Setup()
        {
            Logger = NUnitLogger.GetLogger();
        }

        [Test]
        public void Test1()
        {
            Thread.Sleep(3000);
            Logger.LogInformation($"{TestContext.CurrentContext.Test.Name}");
        }

        [Test]
        public void Test2()
        {
            Thread.Sleep(3000);
            Logger.LogInformation($"{TestContext.CurrentContext.Test.Name}");

        }

        [Test]
        public void Test5()
        {

            Thread.Sleep(3000);
            Logger.LogInformation($"{TestContext.CurrentContext.Test.Name}");
        }

        
        [TearDown]
        public void TearDown()
        {
            //TestContext.Out.WriteLine(TestContext.CurrentContext.Test.Name + " Finished");

        }
    }
}