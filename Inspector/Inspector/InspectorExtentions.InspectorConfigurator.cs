using System;
using System.Text.RegularExpressions;

namespace WonderTools.Inspector
{
    public static partial class InspectorExtentions
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

                if(baseEndpoint.EndsWith("/"))
                    throw new Exception("The base end point should not end with /");


                _options.BaseEndPoint = baseEndpoint;
            }

            public void EnableCors()
            {
                _options.IsCorsEnabled = true;
            }
        }
    }
}