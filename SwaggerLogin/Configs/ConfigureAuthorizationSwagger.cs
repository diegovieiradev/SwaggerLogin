namespace SwaggerLogin.Configs
{
    public class ConfigureAuthorizationSwagger
    {
        private readonly RequestDelegate _requestDelegate;

        public ConfigureAuthorizationSwagger(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger-login"))
            {
                if (context.Request.Method == "POST")
                {
                    var form = await context.Request.ReadFormAsync();
                    var username = form["username"];
                    var password = form["password"];

                    if (username == "admin" && password == "senha")
                    {
                        context.Session.SetString("Authenticated", "true");
                        context.Response.Redirect("/swagger");
                        return;
                    }

                    var errorHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "SwaggerAutenticacaoErrorView.html");
                    var errorHtmlContent = await File.ReadAllTextAsync(errorHtmlPath);
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(errorHtmlContent);
                    return;
                }

                var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "SwaggerAutenticacaoView.html");
                var htmlContent = await File.ReadAllTextAsync(htmlPath);

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(htmlContent);
                return;
            }

            if (context.Request.Path.StartsWithSegments("/swagger") && !context.Session.Keys.Contains("Authenticated"))
            {
                context.Response.Redirect("/swagger-login");
                return;
            }

            await _requestDelegate(context);
        }
    }
}
