using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET")
    {
        int firstNumber = 0, secondNumber = 0;
        string? operation = null;
        long? result = null;

        // Check if the queuery has a first number
        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            // Get the string for first number then check to see if it was empty or not
            string firstNumberString = context.Request.Query["firstNumber"][0];

            if (!string.IsNullOrEmpty(firstNumberString))
            {
                firstNumber = int.Parse(firstNumberString); 
            }
            // if it was empty then set status to 400 and advise that there was no valid first number
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for first number\n");
            }
        }
        // Setting status to 400 if it was okay if it gets to this else because that means no first number was given
        else
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;

            await context.Response.WriteAsync("Invalid input for first number\n");
        }

        // Checking Query for second number now
        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            // Get the string for the second number then check to see if it was empty or not
            string secondNumberString = context.Request.Query["secondNumber"][0];

            if (!string.IsNullOrEmpty(secondNumberString))
            {
                secondNumber = int.Parse(secondNumberString);
            }
            else 
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for second number\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;

            await context.Response.WriteAsync("Invalid number for second input\n");
        }


        // Reading the opertion query if submitted in the request body
        if (context.Request.Query.ContainsKey("operation"))
        {
            operation = Convert.ToString(context.Request.Query["operation"][0]);
            operation = operation.ToLower();

            switch(operation)
            {
                case "add":
                case "+":
                    result = firstNumber + secondNumber;
                    break;

                case "subtract":
                case "-":
                    result = firstNumber - secondNumber;
                    break;

                case "multiply":
                case "*":
                    result = firstNumber * secondNumber;
                    break;

                case "divide":
                case "/":
                    result = firstNumber / secondNumber;
                    break;

                case "mod":
                case "%":
                    result = firstNumber % secondNumber;
                    break;
            }

            // We know we had a good result if it isn't the default null, if it is null bad operation request happened
            if(result.HasValue)
            {
                await context.Response.WriteAsync(result.Value.ToString());
            }
            else
            {
                if (context.Response.StatusCode == 200)
                    context.Response.StatusCode = 400;
                
                await context.Response.WriteAsync("Invaid input for operation\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;

            await context.Response.WriteAsync("Invalid Input for Operation\n");
        }
        
        
    }
});

app.Run();
