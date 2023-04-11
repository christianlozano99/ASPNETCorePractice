# Concepts About HTTP

## What is HTTP?

HTTP stands for Hypertext Transfer Protocol. It is a set of rule which is used for transferring the information like text, audio, video, graphic image and other multimedia files on the WWW (World Wide Web).

HTTP is a protocol that is used to transfer the hypertext from the client end to the server.

## What is the format of a Request Message?

HTTP Requests are messages which are sent by the client to initiate an action on the server.

It consists of various things:

1. Request Line: The Request-Line includes with HTTP method, Url, HTTP version.

2. Request Headers: Contains request-header fields that allow the client to pass additional information to the server, logically equivalent to the parameters while method invocation in a programming language.

3. Request body: Contains actual content to send to server; such as query string, JSON, XML etc.

## What are the important HTTP methods (or HTTP verbs) – (GET, POST, PUT, PATCH, HEAD, DELETE)?

HTTP defines a set of request methods to indicate the desired action to be performed for a given resource.

The “GET” and “POST” are most used in HTML forms. The default request type in browsers while opening a page, is “GET”.

The “GET”, “POST”, “PUT”, “PATCH”, “HEAD” and “DELETE” are used in RESTful HTTP services, such as “Web API controllers”.

GET: This method retrieves information from the given server using a given URI. GET request can retrieve the data. It cannot apply other side effects (changes) on the data.

POST: The POST request sends the data to the server. For example sending user details in a registration form, or in a login form.

PUT: The PUT method is used to replace (update) an existing record with the provided new record. The client sends the entire record that needs to be updated.

PATCH: The PATCH method is to update a part of an existing record. The client sends part of the record that needs to be updated.

HEAD: The HEAD method is the same as the GET method. It is used to transfer the response start line and headers section only (without response body). It can be used when there is no need of sending response body to server; but the server wants to communicate to the client that the necessary operation (such as database updation) has been completed.

DELETE: The DELETE method is used to remove an existing record based on the parameters supplied in the request.

## What are the important HTTP status codes?

An HTTP status code is a server response to a browser’s request. It indicates status of completed action as a response to the request.

HTTP status code classes:

1xx – Informational

2xx – Success

3xx – Redirection

4xx – Client errors

5xx – Server errors



Important HTTP status codes:

200 – OK: The ideal & commonly-used status code that represents normal functioning of a server resource (Eg: page)

201 – Created: Indicates that the server has created (inserted) a record in the data store. It is generally used as response in POST request in RESTful services using as Web API.

301 – Moved Permanently: Represents one URL needs to be redirected to another permanently. A 301 redirect means that visitors and bots that land on that page will be passed to the new URL. That means the first URL will no longer work in future.

302 – Found: Represents a temporary redirection from one URL to another. That means, the first URL might work in the near future.

400 – Bad Request: The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).

401 – Unauthorized: Although the HTTP standard specifies "unauthorized", semantically this response means "unauthenticated". That is, the client must authenticate itself to get the requested response.

404 – Not Found: This means the file or page that the browser is requesting wasn’t found by the server. 404s don’t indicate whether the missing page or resource is missing permanently or only temporarily.

405 – Method Not Allowed: This is mostly used for RESTful services such as Web API. It indicates that the request method is known by the server but the HTTP method is not supported by the server resource. For example, an API may allow GET and POST only; may not allow calling PUT request.

500 – Internal Server Error: Indicates there is some runtime error (exception) while executing the code at server side.

## What is Content Negotiation in HTTP?

In HTTP, content negotiation is the mechanism that is used for serving different representations of a resource to the same URI to help the user agent specify which representation is best suited for the user (for example, which document language, which image format, or which content encoding).

For example, the client may ask for XML data instead of receiving content in JSON format.

It is generally done using “Accept” request header.

Eg:

Accept: application/xml

## How does the HTTP protocol work?

Hypertext Transfer Protocol (HTTP) is an application-layer protocol for transmitting hypermedia documents, such as HTML. It handles communication between web browsers and web servers. HTTP follows a classical client-server model. A client, such as a web browser, opens a connection to make a request, then waits until it receives a response from the server.



HTTP is a protocol that allows the fetching of resources, such as HTML documents. It is the foundation of any data exchange on the Web, and it is a client-server protocol, which means requests are initiated by the recipient, usually the Web browser.

## What is a web server?

The term web server can refer to both hardware and software, working separately or together.


On the hardware side, a web server is a computer with more processing power and memory that stores the application’s back-end code and static assets such as images and JavaScript, CSS, HTML files. This computer is connected to the internet and allows data flow between connected devices.



On the software side, a web server is a program that accepts HTTP requests from the clients, such as a web browser, processes the request, and returns a response. The response can be static, i.e. image/text, or dynamic, i.e. calculated total of the shopping cart.



Popular examples of web servers include Apache, Nginx, and IIS.
