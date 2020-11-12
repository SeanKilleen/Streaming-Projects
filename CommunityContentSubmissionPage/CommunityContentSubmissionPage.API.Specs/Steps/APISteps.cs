﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityContentSubmissionPage.API.Specs.Drivers;
using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CommunityContentSubmissionPage.API.Specs.Steps
{
    [Binding]
    public class APISteps
    {
        private readonly WebServerDriver _webServerDriver;

        public APISteps(WebServerDriver webServerDriver)
        {
            _webServerDriver = webServerDriver;
        }

        [Then(@"you can choose from the following Types:")]
        public void ThenYouCanChooseFromTheFollowingTypes(Table table)
        {
            var typenameEntries = table.CreateSet<TypenameEntry>();

            var restClient = new RestClient(_webServerDriver.Hostname);
            var restRequest = new RestRequest("api/AvailableTypes", DataFormat.Json);
            var restResponse = restClient.Get<AvailableTypesResponse>(restRequest);

            restResponse.IsSuccessful.Should().BeTrue();

            var actualTypes = restResponse.Data.Types;
            var expectedTypes = typenameEntries.Select(i => i.Typename);

            actualTypes.Should().BeEquivalentTo(expectedTypes);
        }


        public class AvailableTypesResponse
        {
            public List<string> Types { get; set; } = new List<string>();
            public string SelectedType { get; set; } = String.Empty;
        }
    }
}
