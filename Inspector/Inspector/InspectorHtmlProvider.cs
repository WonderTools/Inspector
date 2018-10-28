namespace WonderTools.Inspector
{
    public static class InspectorHtmlProvider
    {
        public static string GetHtml(string url, string header)
        {
            return $@"<html>
    <head>
        <meta charset=""UTF-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
        <title>WonderTools-Inspector</title>
        <style>
            .heading {{
                background-color: blue;
                height : 50px;
                vertical-align: middle;
                font-size: 250%;
                text-align: center;
                padding: 20px;
                color: aliceblue;
            }}

            .hinttext {{
                display: inline-block;
                width: 20%;
            }}
            .userinput{{
                display: inline-block;
                width: 78%;
                margin: 2px;
            }}

            .userinput input {{
                width: 98%;
            }}

            .input {{
                background-color: skyblue;
                padding-top: 20px;
                padding-bottom: 10px;
            }}
                        
            .input button {{
                align-content: center;
            }}
            .result {{
                background-color: aliceblue
            }}
                        
            .result textarea {{
                width: 90%;
                margin-left: 20px;
                margin-right: 20px;
                margin-bottom: 6px;
                margin-top: 6px;
                height: 200px;
            }}
        </style>
        <script>
            function httpGet(url,key){{
                var xmlHttp = new XMLHttpRequest();
                xmlHttp.onreadystatechange = function() {{
                    if (this.readyState == 4 && this.status == 200) {{
                        document.getElementById(""result"").value = this.responseText;
                    }}else if(this.readyState == 4 && this.status == 403) {{
                        document.getElementById(""result"").value = ""Password may be invalid"";
                    }}
                }};
                xmlHttp.open( ""GET"", url);
                xmlHttp.setRequestHeader(""{header}"",key);
                xmlHttp.send(null);
                return xmlHttp.response;
            }}
            function getData(){{
                document.getElementById('result').value = """";
                var key = document.getElementById(""password"").value; 
                var url = document.getElementById(""url"").value;
                var x = httpGet(url,key); 
            }}
        </script>
    </head>
    <body>
        <div class=""heading"">WonderTools Inspector</div>
        <div class=""input"">
            <div class=""inputline"">
                <div class=""hinttext"">url</div><div class=""userinput""><input id=""url"" type=""text"" value=""{url}""></div>
            </div>
            <div>
                <div class=""inputline"">
                    <div class=""hinttext"">password</div><div class=""userinput""><input id=""password"" type=""password""></div>
                </div >
            </div>
            <div class=""inputline"">
                <button id=""demo"">Get Version</button>
            </div>
        </div>
        <div class=""result"">
            <textarea id=""result""></textarea>
        </div>
        <script>
            document.getElementById(""demo"").onclick = function() {{getData()}};
        </script>
    </body>
</html>";
        }
    }
}