using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace WonderTools.Inspector
{
    public class InspectorConfigurator
    {
        private readonly InspectorRepository _repository;
        private readonly InspectorOptions _options;

        public InspectorConfigurator(InspectorRepository repository, InspectorOptions options)
        {
            _repository = repository;
            _options = options;
        }

        public void AddVersion(string version)
        {
            _repository.AddKeyValue("Version", version);
        }

        public void AddName(string name)
        {
            _repository.AddKeyValue("Name", name);
        }

        public void AddConfigurationSection(IConfiguration configuration, string sectionName)
        {
            var inspectorSection = configuration.GetSection(sectionName);
            var children = inspectorSection.GetChildren();

            foreach (var child in children)
            {
                var key = child.Key;
                var value = child.Value;
                if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(key))
                {
                    _repository.AddKeyValue(key, value);
                }
            }
        }

        public void AddEnvironment(string environment)
        {
            _repository.AddKeyValue("Environment", environment);
        }

        public void AddKeyValue(string key, string value)
        {
            _repository.AddKeyValue(key, value);
        }

        public void SetBaseEndpoint(string baseEndpoint)
        {
            if (string.IsNullOrWhiteSpace(baseEndpoint))
                throw new Exception("The base end point should not be empty");

            var match = Regex.Match(baseEndpoint, @"^/[-0-9a-z/]+$", RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new Exception("The base end point has to start with /. It could contains alphabets, numbers, hypen and /");

            var match1 = Regex.Match(baseEndpoint, @"//", RegexOptions.IgnoreCase);
            if (match1.Success)
                throw new Exception("The base end point should not contain //");

            if (baseEndpoint.EndsWith("/"))
                throw new Exception("The base end point should not end with /");


            _options.BaseEndPoint = baseEndpoint;
        }

        public void EnableCors()
        {
            _options.IsCorsEnabled = true;
        }

        public void UseAuthenticationHeader(string authenticationHeader)
        {
            _options.AuthenticationHeader = authenticationHeader;
        }

        public void AuthenticateWith(params string[] validTokens)
        {
            SetAuthenticator(validTokens.Contains);
        }

        public void AuthenticateWith(Func<string, bool> authenticator)
        {
            SetAuthenticator(authenticator);
        }

        private void SetAuthenticator(Func<string, bool> authenticator)
        {
            _options.Authenticator = authenticator;
            _options.IsLoginPageEnabled = true;
        }
    }
}
