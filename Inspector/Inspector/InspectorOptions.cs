using System;

namespace WonderTools.Inspector
{
    public class InspectorOptions
    {
        public InspectorOptions()
        {
            BaseEndPoint = "";
            IsCorsEnabled = false;
            AuthenticationHeader = "wondertools-authorization";
            IsLoginPageEnabled = false;
        }
        public string BaseEndPoint { get; set; }
        public bool IsCorsEnabled { get; set; }
        public string AuthenticationHeader { get; set; }
        public Func<string,bool> Authenticator { get; set; }
        public bool IsLoginPageEnabled { get; set; }

    }

    public static class InspectorOptionsExtentions
    {
        public static string GetInspectorUri(this InspectorOptions options)
        {
            return options.BaseEndPoint + "/version";
        }

        public static string GetInspectorLoginUri(this InspectorOptions options)
        {
            return options.BaseEndPoint + "/version-login";
        }
    }
}