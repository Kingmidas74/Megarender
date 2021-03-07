using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Megarender.Business.IntegrationTests
{    
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}