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
            else if (IsRequestForPreflightInspection(path, method)) await HandlePreflight(context);
            else await next.Invoke();
        }

        private bool IsRequestForPreflightInspection(string path, string method)
        {
            return IsRequestValid(path, method, "/version", "options");
        }

        private bool IsRequestForInspection(string path, string method)
        {
            return IsRequestValid(path, method, "/version", "get");
        }

        private bool IsRequestValid(string requestPath, string requestMethod, string expectedAdditionalPath, string expectedMethod)
        {
            var expectePath = _options.BaseEndPoint + expectedAdditionalPath;
            if (string.IsNullOrWhiteSpace(requestPath)) return false;
            if (string.IsNullOrEmpty(requestMethod)) return false;
            if (!requestMethod.Equals(expectedMethod, StringComparison.InvariantCultureIgnoreCase)) return false;
            if (requestPath.Equals(expectePath, StringComparison.InvariantCultureIgnoreCase)) return true;
            if (requestPath.Equals(expectePath + "/", StringComparison.InvariantCultureIgnoreCase)) return true;
            return false;
        }

        private async Task HandlePreflight(HttpContext context)
        {
            if (_options.IsCorsEnabled)
            {
                AddCorsResponseHeaders(context);
                context.Response.StatusCode = 204;
                await context.Response.WriteAsync(string.Empty, Encoding.UTF8);
            }
            else
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(string.Empty);
            }            
        }

        private static void AddCorsResponseHeaders(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "wondertools-authorization");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET");
            context.Response.Headers.Add("Vary", new[] {"Origin"});
        }


        private async Task HandleInspection(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            var dictionary = _repository.GetDictionary();
            AddCorsResponseHeaders(context);
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