using System.Threading;
using Microsoft.Extensions.Logging;
using NUnit.Framework;


namespace Nunit3netcore
{
    [Parallelizable(ParallelScope.Children)]
    public class UnitTestWIthThreadLocal
    {
        public ThreadLocal<ILogger> Logger;

        [SetUp]
        public void Setup()
        {
            Logger = new ThreadLocal<ILogger>(() => NUnitLogger.GetLogger());
        }

        [Test]
        public void Test1()
        {
            Thread.Sleep(3000);
            Logger.Value.LogInformation($"{TestContext.CurrentContext.Test.Name}");
        }

        [Test]
        public void Test2()
        {
            Thread.Sleep(3000);
            Logger.Value.LogInformation($"{TestContext.CurrentContext.Test.Name}");

        }

        [Test]
        public void Test5()
        {

            Thread.Sleep(3000);
            Logger.Value.LogInformation($"{TestContext.CurrentContext.Test.Name}");
        }


        [TearDown]
        public void TearDown()
        {
            //TestContext.Out.WriteLine(TestContext.CurrentContext.Test.Name + " Finished");

        }
    }
}
