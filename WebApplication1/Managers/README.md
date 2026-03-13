# Managers (Business/Service Layer)

### What is it?
Managers (often referred to as Services in other conventions) represent the domain or business logic layer of the application.
The core brain of the application. The Business Logic Layer (BLL). In the .NET ecosystem.

### What's in it?
- **Contracts**
	Interfaces defining the operations (e.g., `IBookingManager`).
- **Implementations**
	Classes that implement the interfaces containing the actual business rules and logic (e.g., `BookingManager`).
- References to Repositories to fetch or persist data.
- Coordination of helper classes, like calculators or external validation logics.
- Contain methods with conditional statements, calculations, complex rules, and data mapping.

### What's the purpose of it?
To separate the core business rules of the application from both the web/HTTP context (Controllers) and the raw data access logic (Repos).
This makes the application highly testable, cohesive, and easy to maintain.

### How it works?
A Manager receives a request from a Controller (often taking in DTOs as parameters).
It then performs required business and validation rules (e.g., checking if a room is available, calculating the price using `PriceCalculator`).
Next, it fetches or updates needed models using the Repositories, constructs the final result, and returns it to the Controller.
