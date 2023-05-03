using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace MiddlewareLogin.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/" && httpContext.Request.Method == "POST")
            {
                // read resposne body as a stream
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                // storing what is read
                string responseBody = await reader.ReadToEndAsync();


                // parse request body from string into Dictionary
                Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(responseBody);

                string? email = null, password = null;

                //read 'firstNumber' if submited in the request body
                if(queryDict.ContainsKey("email"))
                {
                    email = Convert.ToString(queryDict["email"][0]);
                }

                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for email!\n");
                }

                // read 'secondNumber if submitted in request body
                if (queryDict.ContainsKey("password"))
                {
                    password = Convert.ToString(queryDict["password"][0]);
                }

                else
                {
                    if(httpContext.Response.StatusCode == 200)
                        httpContext.Response.StatusCode = 400;
                    
                    await httpContext.Response.WriteAsync("Invalid input for password!\n");
                }

                // now to write the logic if given password and user name
                if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    //valid email and password as per the requirement specification
                    string validEmail = "admin@example.com", validPassword = "admin1234";
                    bool isValidLogin;

                    if(email == validEmail && password  == validPassword)
                    {
                        isValidLogin = true;
                    }
                    
                    else
                    {
                        isValidLogin = false;
                    }

                    //give user response

                    if(isValidLogin)
                    {
                        await httpContext.Response.WriteAsync("Successful login\n");
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid Login\n");
                    }
                }
            }
            // if not post call next part middleware to be used
            else
            {
                await _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
