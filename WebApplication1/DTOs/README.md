# DTOs (Data Transfer Objects) 

### What is it?
DTOs are simple un-opinionated C# classes that act as data containers. They are used exclusively to pass data across system boundaries (from the Client to the Controller, or from the Controller to the Manager).

### What's in it?
- Properties with getters and setters.
- Data validation annotations (e.g., `[Required]`, `[MaxLength]`).
- Generally **no** business logic or behaviors (methods).

### What's the purpose of it?
- **Security & Encapsulation**
	To avoid exposing internal domain Entities and the actual database structure to the client. It prevents "over-posting" (mass assignment) attacks.
	You might have a User entity with a PasswordHash and IsAdmin flag.
	You don't want a user to update those via a profile update form.
	A UserUpdateDTO ensures only safe fields (like Email or FirstName) can be passed.
- **Efficiency**
	To restrict the payload size to only what is necessary for a specific operation.
- **Decoupling**
	To keep the database schema independent of the API contract.

### How it works?
When a JSON payload hits a Controller endpoint
	.NET automatically deserializes it into a request DTO.
	The Controller passes this DTO to a Manager.
	The Manager extracts the data from the DTO,
		applies business logic,
		maps that data to a domain Entity, and sends it to the Repo.
	Conversely, when data is fetched from the database,
		the Entity is transformed into a response DTO before the Controller sends it out as JSON.

