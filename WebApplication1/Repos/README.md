# Repos (Repositories)

### What is it?
The Data Access Layer of the application.
Repositories act as a middleman between the application's business logic (Managers) and the underlying database.

### What's in it?
- Methods specifically designed to retrieve, insert, update, and delete entities from the database.
- Typically an Interface (e.g., `IBookingRepository`) and an Implementation (`BookingRepository`).

### What's the purpose of it?
To encapsulate all data access mechanisms.
The Managers do not need to know *how* data is stored or queried (whether it's an SQL database, a file, or a NoSQL store);
	they just call high-level methods like `GetBookingById()` or `Save()`.
	This abstracts database complexity and prevents data queries from leaking into business logic.

### How it works?
Managers inject Repositories via dependency injection.
When the Manager needs data, it calls a repo method.
	The repository translates this call into a database query (often using LINQ to SQL via Entity Framework),
	executes it against the database, maps the relational data into C# domain Entities, and returns them to the Manager.