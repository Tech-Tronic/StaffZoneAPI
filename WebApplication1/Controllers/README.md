# Controllers

### What is it?
Controllers act as the entry point for all HTTP requests made to the API.
They are part of the presentation layer in an API architecture.
It acts as the traffic cop that receives HTTP requests (GET, POST, PUT, DELETE) from the client (like a web browser or mobile app).

### What's in it?
- API endpoint methods (e.g., `HttpGet`, `HttpPost`, `HttpPut`, `HttpDelete`).
- Route definitions (e.g., `[Route("api/[controller]")]`).
- Dependency injection via constructors (usually injecting Managers).
- Minimal logic mostly dealing with HTTP status codes and delegating work.

### What's the purpose of it?
To receive incoming network requests from clients (like a web frontend or mobile app),
	validate the basic request format, delegate the actual processing to the business logic layer (Managers/Services layer), 
	and then return an appropriate HTTP response (such as `200 OK`, `400 Bad Request`, or `404 Not Found`).

### How it works?
When a client makes an HTTP request to an endpoint (e.g., `/api/booking`),
	the .NET routing engine directs the request to the matching method in the `BookingController`. 
The controller receives the data (often as a DTO), calls the corresponding method in `IBookingManager`,
	and wraps the result in an `IActionResult` to send back to the client.