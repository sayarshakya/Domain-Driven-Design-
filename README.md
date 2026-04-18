# Domain-Driven Design Example with .NET

This repository provides a practical implementation of Domain-Driven Design (DDD) principles using .NET. The project is modeled around a veterinary practice system, which is divided into two distinct Bounded Contexts: **Management** and **Clinic**.

The primary goal is to demonstrate core DDD patterns for building robust, scalable, and maintainable software by separating concerns and modeling complex business domains effectively.

## Key DDD Concepts Demonstrated

*   **Bounded Contexts**: The application is split into `Wpm.Management` and `Wpm.Clinic`, each with its own models, logic, and persistence, representing different subdomains of the veterinary practice.
*   **Shared Kernel**: The `Wpm.SharedKernel` project contains common code like base classes (`Entity`, `AggregateRoot`), interfaces (`IDomainEvent`), and shared Value Objects (`Weight`) used across multiple Bounded Contexts.
*   **Aggregates**: `Pet` in the Management context and `Consultation` in the Clinic context serve as aggregate roots, ensuring the consistency of the objects within their boundaries.
*   **Entities & Value Objects**: The domain models make a clear distinction between Entities (objects with a distinct identity, e.g., `Pet`) and Value Objects (immutable objects defined by their attributes, e.g., `WeightRange`, `Dose`).
*   **Domain Events**: The system uses domain events to communicate significant occurrences within the domain (e.g., `PetWeightUpdated`, `ConsultationStarted`). A `DomainEventDispatcher` is used for handling these events.
*   **Application Services**: `ManagementApplicationService` and `ClinicApplicationService` orchestrate domain logic by handling commands and coordinating with domain objects.
*   **Event Sourcing**: The `Clinic` Bounded Context uses an event sourcing approach to persist the state of the `Consultation` aggregate. Instead of storing the current state, it stores the sequence of domain events that have occurred. The aggregate's state is rebuilt by replaying these events.

## Project Structure

The solution is organized into several projects, each corresponding to a layer or a Bounded Context.

```
├── Wpm.Clinic.Api/         # API and application layer for the Clinic context
├── Wpm.Clinic.Domain/      # Domain model for the Clinic context (uses Event Sourcing)
├── Wpm.Management.Api/     # API and application layer for the Management context
├── Wpm.Management.Domain/  # Domain model for the Management context (traditional state persistence)
├── Wpm.SharedKernel/       # Code shared across all contexts
├── *.Domain.Tests/         # Unit tests for the domain layers
```

### Bounded Contexts

#### Management Context (`Wpm.Management.*`)

This context is responsible for managing pet information, including registration and tracking physical attributes like weight.

*   **Aggregates**: `Pet`
*   **Entities**: `Breed`
*   **Persistence**: Uses Entity Framework Core with a traditional state-based approach, storing the current state of the `Pet` entity in a SQLite database (`WpmManagement.db`).

#### Clinic Context (`Wpm.Clinic.*`)

This context is responsible for managing clinical consultations for pets.

*   **Aggregates**: `Consultation`
*   **Persistence**: Uses an **Event Sourcing** pattern. All changes to a `Consultation` (starting it, setting a diagnosis, administering a drug) are stored as a sequence of events in a SQLite database (`WpmClinic.db`). The `Consultation` aggregate's state is restored by replaying these events.

## Getting Started

### Prerequisites

*   .NET 8.0 SDK

### Running the Application

This solution contains two separate web APIs that can be run concurrently.

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/sayarshakya/Domain-Driven-Design-.git
    cd Domain-Driven-Design-
    ```

2.  **Run the Management API:**
    ```bash
    cd Wpm.Management.Api
    dotnet run
    ```
    The API will be available at `http://localhost:5042`. You can access the Swagger UI at `http://localhost:5042/swagger`.

3.  **Run the Clinic API:**
    In a new terminal window:
    ```bash
    cd Wpm.Clinic.Api
    dotnet run
    ```
    The API will be available at `http://localhost:5148`. You can access the Swagger UI at `http://localhost:5148/swagger`.

Both APIs use SQLite for persistence, and the database files (`WpmManagement.db` and `WpmClinic.db`) will be created automatically in their respective API project directories upon first launch.

## API Usage Examples

You can use the Swagger UI or a tool like `curl` or Postman to interact with the APIs.

### 1. Create a Pet (Management API)

Send a `POST` request to `http://localhost:5042/controller` to create a new pet. The `breedId` must be one of the pre-configured breeds.

*   `1c10f44e-83b1-4094-b6b1-4298991d9d71` (Labrador Retriever)
*   `63386cae-79c2-4180-8630-60c6cdf2f5f1` (German Shepherd)

**Request:**

```json
{
  "id": "a3b1f13e-6f8d-4f1a-8c5e-8a7e7a5b6c4d",
  "name": "Buddy",
  "age": 5,
  "color": "Golden",
  "sexOfPet": 0, // 0 for Male, 1 for Female
  "breedId": "1c10f44e-83b1-4094-b6b1-4298991d9d71"
}
```

### 2. Start a Consultation (Clinic API)

Use the Pet ID from the previous step to start a new consultation. Send a `POST` request to `http://localhost:5148/Clinic`.

**Request:**

```json
{
  "patientId": "a3b1f13e-6f8d-4f1a-8c5e-8a7e7a5b6c4d"
}
```

This will return the new `consultationId`.

### 3. Set Diagnosis for the Consultation (Clinic API)

Send a `PUT` request to `http://localhost:5148/Clinic/diagnosis` to add a diagnosis.

**Request:**

```json
{
  "consultationId": "{Your-Consultation-ID}",
  "diagnosis": "Minor skin irritation."
}
