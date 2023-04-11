using Microsoft.Extensions.Primitives;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    StreamReader reader = new StreamReader(context.Request.Body);

    // make the program wait for a response
    string body = await reader.ReadToEndAsync();


    // Sending post request through PostMan as raw: firstName=scott&age=20&age=30
    // With Dictionaty and String Values class we are able to store both firstName(s)
    // and age(s) with in the same place even though they have diffrent name
    Dictionary<string, StringValues> queryDict = 
        Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if(queryDict.ContainsKey("firstName"))
    {
        string firstName = queryDict["firstName"][0];
        await context.Response.WriteAsync(firstName);
    }

    // lets try to parse over mutiple names now
    // Testing using postman with this data(POST): firstName=Jane&firstName=John&firstName=David

    if(queryDict.ContainsKey("firstName"))
    {
        foreach(string name in queryDict["firstName"])
        {
            await context.Response.WriteAsync(name);
        }
    }

    // response: JaneJohnDavid

});

app.Run();
