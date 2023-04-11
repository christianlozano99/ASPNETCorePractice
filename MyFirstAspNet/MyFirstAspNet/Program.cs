var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Any async needs to have await before the asnyc function
// the =>{} is lambda 
app.Run(async (HttpContext context) =>
{
    // Can only assign head key to string.
    context.Response.Headers["MyKey"] = "my Value";
    // We can set server name in the header but it does not change the fact of where it came from.
    context.Response.Headers["Server"] = "My Value";

    // We can set the type of content the Server is giving as a response
    // here we can see that it is type text/html
    context.Response.Headers["Content-Type"] = "text/html";

    // there is a content-Length type put it is usuaully self assigned so we need to not worry about it
    // context.Response.Headers["Content-Length] = Number of Bytes;

    /* Other Response Headers 
     * Cache-Control: Indicates how many seconds that response can be cacached on browser, Ex: max-age=60
     * Set-Cookie: Contains cookies to send to browser, Ex: x = 10
     * Access-Control-Allow-Origin: Used to enable CORS (Cross-Origin-Resource-Sharing)
     * Location: Contains url to redirect, EX: http://example-redirect.com
     */
    
    // Response in a sync manner, we wait for the response from the server.
    await context.Response.WriteAsync("<h1>The context.Response.WriteAsync says: Hello World!</h1>");

    //We can get the string of the html request path the following way, also the request method
    string path = context.Request.Path;
    string requestMethohd = context.Request.Method;

    // Little neat thing to put variables into a write async is to put a $ before the string of html.
    await context.Response.WriteAsync($"<h2>Current path is: {path}</h2>");
    await context.Response.WriteAsync($"<h2>Current HTTP method is: {requestMethohd}</h2>");

    // lets do some logic, lets check to see in case of a get requests and that the query contains id
    if(context.Request.Method == "GET")
    {
        if(context.Request.Query.ContainsKey("id"))
        {
            string id = context.Request.Query["id"];
            // we give ID by URL ex: localhost:5278?id=4
            // or if multiple seperate with & localhost:5278?id=4&id=6&id=9
            await context.Response.WriteAsync($"<b>The given ID(s) is: {id}</b></br>\n");
        }
    }


    // HTTPRequest Headers
    // Accept: Represent MIME type of response content to be accepted by the client. Ex: text/html

    // Accept-Language: Represents natural langugage of response content to be accepted by the client. Ex: en-US

    // Content-Type: MIME type of request body. Ex: text/x-ww-form-urlencoded, application/json, applicaiton/xml, etc

    // Content-Length: Length(bytes) of request body. Ex: 100

    // Date: Date and time of request. Ex: Tue, 11 Apr 2023 16:01:42 GMT

    // Host: Server domain name. Ex: www.example.com

    // User-Agent: Browser(client) details. Ex: Chrome/112.0.0.0, Safari/537.36

    // Cookies: Contains cookies to send to server. Ex: x = 100

    // lets see our content type
    if(context.Request.Headers.ContainsKey("User-Agent"))
    {

        string userAgent = context.Request.Headers["User-Agent"];
        await context.Response.WriteAsync($"\n<b>The User-Agent is: {userAgent}</b></br>");
    }

    // lets send something with post man then read it on our site:
    if (context.Request.Headers.ContainsKey("AuthorizationKey"))
    {
        string AuthorizationKey = context.Request.Headers["AuthorizationKey"];
        await context.Response.WriteAsync($"</br><b>The Authorization Key is: {AuthorizationKey}</b>");
    }
});

app.Run();


// Most common request methods:
/*
 * Get: Requests to retrieve informaiton (page, entity object, or a static file)
 * 
 * Post: Sends an entity object to a server; generally it will be inserted into a DB
 * 
 * Put: Sends an entity object to server; generally updates all properties of the object in DB
 * 
 * Patch: Sends an entity object to server; generally updates few properties in the DB (Partial-Update)
 * 
 * Delete: Requests to delete an entity in the Database
 * 
 */