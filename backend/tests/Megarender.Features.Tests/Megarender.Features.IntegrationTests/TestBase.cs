using System.Threading.Tasks;
using NUnit.Framework;

namespace Megarender.Features.IntegrationTests
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