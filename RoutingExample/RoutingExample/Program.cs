using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// enabled routing
app.UseRouting();

/*
app.Use(async (context, next) =>
{
    // ? means it's nullable or not of a type
    // in real world we can use GetEndpoint to investigate information about it like if it's post,get,etc. and/or it's mapping
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();

    if(endPoint != null)
    {
        await context.Response.WriteAsync($"End point: {endPoint}\n");
    }

    await next(context);
});
*/

// creating end points
app.UseEndpoints(endpoints =>
{
    // add your end points here

    // end point with url "/map1"
    endpoints.Map("/map1", async (context) =>
    {
        await context.Response.WriteAsync("In Map 1");
    });

    // end point wiht url "/map2"
    endpoints.Map("/map2", async (context) =>
    {
        await context.Response.WriteAsync("In map 2");

    });


    // by default endpoints work for any type of http method
    // if you want to restrict we can use the following.
    // end point with url "/map4"
    endpoints.MapPost("/mapPost", async (context) =>
    {
        await context.Response.WriteAsync("In Map Post");
    });

    // end point wiht url "/mapGet"
    endpoints.MapGet("/mapGet", async (context) =>
    {
        await context.Response.WriteAsync("In Map Get");

    });

    // on post man we only get the response from the end point if it matches the Request type, otherwise we get a status of: "405 method not allowed."
    // this way we can configure endpoints to the correct type of methods.

    // example of having route parameters, the parameters in this case is the filename and extension type, the literal is files because that won't change
    // it will access this endpoint using this as an example: http://localhost:5053/files/sample.txt
    endpoints.Map("files/{filename}.{extension}", async context =>
    {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);

        await context.Response.WriteAsync($"In files - fileName: {fileName} - extension: {extension}");

    });


    // Route parameter for profile using ex. http://localhost:5053/employee/profile/chris output is: In Employee profile: chris
    // We can set default value for this by equaling the parameter and like this if we don't give the parameter it will default to what it is equal to and if not overwritten
    // ex. http://localhost:5053/employee/profile will have the folling output: In Employee profile: Default
    // we can also set a length restriction, at least 3 long and at most 7 long in this case using {parameter:length(min, max)}
    // given length with just one parameter you can accept just a specfic amount of characters, {parameter:length(9)}
    // contraint alpha will constraint it to be only alphabet characters
    endpoints.Map("employee/profile/{EmployeeName:length(3,7):alpha}", async context =>
    {
        // the parameter is not case sensative so something like this would work
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);

        await context.Response.WriteAsync($"In Employee profile: {employeeName}");
    });


    // ex: url/products/details/20 output: Product ID is: 20
    // we can set a range for ints , -1 would not match and 1001
    endpoints.Map("products/details/{id:int:range(1,1000)?}", async context =>
    {
        int? productID = Convert.ToInt32(context.Request.RouteValues["id"]);

        await context.Response.WriteAsync($"Product ID is: {productID}");

    });

    // Optional parameters, added the question mark next to id
    // user/details will give you a null which defaults to 0 as an int
    endpoints.Map("user/details/{id?}", async context =>
    {
        if(context.Request.RouteValues.ContainsKey("id"))
        {
            int? userID = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product ID is: {userID}");
        }
        else
        {
            await context.Response.WriteAsync("Product details id not supplied");
        }

    });

    // constraints on value type route parameters, we do this by using the following {parameter:datatypeConstraint}
    // this will route automtiacally to the home page because it does not map to anything since it does not satisfy the contraint
    endpoints.Map("contraint/test/{id:int?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int? testID = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Test ID is: {testID}");
        }
        else
        {
            await context.Response.WriteAsync("Test id not supplied");
        }
    });

    // Another ex using date constraint
    // daily-digest-report/2030-06-01 output: The report date is: 6/1/2030
    // Using a invalid month 20, daily-digest-report/2030-20-01 output:   Request recieved at: /daily-digest-report/2030-20-01
    endpoints.Map("daily-digest-report/{reportDate:datetime}", async context =>
    {
        DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportDate"]);
        await context.Response.WriteAsync($"The report date is: {reportDate.ToShortDateString()}");
    });

    // we can also do a regex(expression) this is to do a regular expression for the incoming URL
    // sales-report/2030/apr, for simplicity setting a min of 1900 for year and only accepting months apr, jul, oct, and jan
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);

        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        await context.Response.WriteAsync($"Sales report = {year} - {month}");
    });
});


// terminating or short cirtuiting middlerware
app.Run(async context =>
{
    await context.Response.WriteAsync($"Request recieved at: {context.Request.Path}");
});

app.Run();
