## gRPC CRUD Service Description

---

### General Information

This gRPC service provides basic CRUD (Create, Read, Update, Delete) operations for task management. It is developed using ASP.NET Core 8 and integrates with PostgreSQL via Dapper for database interactions. The service is tested using xUnit and Moq, and it is containerized with Docker for convenient deployment and scaling.

### Components

1. **ASP.NET Core 8**: Provides the foundation for implementing the gRPC service and handling HTTP requests.
2. **Dapper**: A lightweight ORM used for executing database queries with PostgreSQL.
3. **PostgreSQL**: A relational database for storing task data.
4. **xUnit**: Utilized for writing unit tests.
5. **Moq**: A library for creating mock objects used in testing.
6. **Docker**: A platform for packaging the application into containers, simplifying deployment and dependency management.

### Functional Capabilities

- **CreateTask**: Creates a new task in the database.
- **ReadTasks**: Retrieves a list of all tasks from the database.
- **UpdateTask**: Updates an existing task.
- **DeleteTask**: Deletes a task by its identifier.

