using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WonderTools.Inspector
{
    public class InspectorMiddleWare
    {
        private InspectorRepository _repository;
        private readonly InspectorOptions _options;

        public InspectorMiddleWare(InspectorRepository repository, InspectorOptions options)
        {
            _repository = repository;
            _options = options;
        }


        public async Task Process(HttpContext context, Func<Task> next)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;
            if (IsRequestForInspection(path, method)) await HandleInspection(context);
            else await next.Invoke();
        }

        private bool IsRequestForInspection(string path, string method)
        {
            return IsRequestValid(path, method, _options.BaseEndPoint+"/version", "get");
        }

        private bool IsRequestValid(string requestPath, string requestMethod, string expectedAdditionalPath, string expectedMethod)
        {
            var expected = expectedAdditionalPath;
            if (string.IsNullOrWhiteSpace(requestPath)) return false;
            if (string.IsNullOrEmpty(requestMethod)) return false;
            if (!requestMethod.Equals(expectedMethod, StringComparison.InvariantCultureIgnoreCase)) return false;
            if (requestPath.Equals(expected, StringComparison.InvariantCultureIgnoreCase)) return true;
            if (requestPath.Equals(expected + "/", StringComparison.InvariantCultureIgnoreCase)) return true;
            return false;
        }


        private async Task HandleInspection(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            var dictionary = _repository.GetDictionary();
            
            var jsonString = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }

        //private async Task HandleJitUiRequest(HttpContext context)
        //{
        //    context.Response.StatusCode = 200;
        //    context.Response.ContentType = "text/html";
        //    var html = HtmlGenerator.GetIndex(css, js);
        //    await context.Response.WriteAsync(html, Encoding.UTF8);
        //}

        //private async Task HandlePreflight(HttpContext context)
        //{
        //    context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
        //    context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization", "cache-control" });
        //    context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "null" });
        //    context.Response.Headers.Add("Vary", new[] { "Origin" });
        //    context.Response.StatusCode = 204;
        //    await context.Response.WriteAsync(string.Empty, Encoding.UTF8);
        //}

        //private async Task HandleJsRequest(HttpContext context)
        //{
        //    context.Response.ContentType = "application/javascript";
        //    context.Response.StatusCode = 200;
        //    var data = ReadEmbeddedResource("WonderTools.JitLogger.Resource." + js);
        //    await context.Response.Body.WriteAsync(data, 0, data.Length);
        //}

        //private async Task HandleCssRequest(HttpContext context)
        //{
        //    context.Response.ContentType = "text/css";
        //    context.Response.StatusCode = 200;
        //    var data = ReadEmbeddedResource("WonderTools.JitLogger.Resource." + css);
        //    await context.Response.Body.WriteAsync(data, 0, data.Length);
        //}




        //private static List<Log> FilterLogsBasedOnQueryString(HttpContext context, List<Log> logs)
        //{
        //    var QueryParameter = "exclusion-log-id-limit";
        //    if (!context.Request.Query.ContainsKey(QueryParameter)) return logs;
        //    var idAsString = context.Request.Query[QueryParameter].ToString();
        //    if (!Int32.TryParse(idAsString, out var id)) return logs;
        //    if (!logs.Any(x => x.LogId == id)) return logs;
        //    return logs.Where(x => x.LogId > id).ToList();
        //}

        //private bool IsRequestForJitUi(string path, string method)
        //{
        //    return IsRequestValid(path, method, "/ui", "get");
        //}



        //private bool IsRequestForPreflightJitUi(string path, string method)
        //{
        //    return IsRequestValid(path, method, "/ui", "options");
        //}

        //private bool IsRequestForPreflightJitLogs(string path, string method)
        //{
        //    return IsRequestValid(path, method, "/logs", "options");
        //}



        //private byte[] ReadEmbeddedResource(String filename)
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    using (Stream filestream = assembly.GetManifestResourceStream(filename))
        //    {
        //        if (filestream == null) return null;
        //        byte[] bytes = new byte[filestream.Length];
        //        filestream.Read(bytes, 0, bytes.Length);
        //        return bytes;
        //    }
        //}
    }
}