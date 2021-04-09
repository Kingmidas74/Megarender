using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Megarender.WebAPI.AcceptanceTests.StepDefenitions
{
    [Binding]
    public class AuthorizationCheckSteps
    {
        private readonly HttpClient httpClient;
        private readonly ScenarioContext scenarioContext;

        public AuthorizationCheckSteps(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            this.httpClient = httpClient;
            this.scenarioContext = scenarioContext;
        }

        [When(@"User doesn't have authorization")]
        public void WhenUserDoesntHaveAuthorization()
        {
            this.scenarioContext.Add("Bearer",String.Empty);
        }

        [Then(@"Free status is available")]
        public async Task ThenFreeStatusIsAvailable()
        {
            var result = await this.httpClient.GetAsync("");
            result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
        }

        [Then(@"Protect status is not available")]
        public async Task ThenProtectStatusIsNotAvailable()
        {
            var bearer = this.scenarioContext.Get<string>("Bearer");
            this.httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", bearer);

            var result = await this.httpClient.GetAsync("");
            result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Forbidden);
        }

        [When(@"User have authorization")]
        public void WhenUserHaveAuthorization()
        {
            this.scenarioContext.Add("Bearer","11");
        }

        [Then(@"Protect status is available")]
        public async Task ThenProtectStatusIsAvailable()
        {
            var bearer = this.scenarioContext.Get<string>("Bearer");
            this.httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", bearer);
            var result = await this.httpClient.GetAsync("");
            result.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
        }
    }
}