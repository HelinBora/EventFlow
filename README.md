# EventFlow

### Distributed Event Ticketing & Reservation Engine

EventFlow is a backend-focused project built with **C# and ASP.NET Core Web API**, designed to explore the core engineering challenges of an event ticketing and reservation platform.

The project focuses on RESTful API development, relational database design, automated testing, containerization, and reliable reservation workflows.

Rather than being just a CRUD application, EventFlow aims to address real-world backend challenges such as data consistency, concurrent requests, and preventing duplicate seat reservations.

## Tech Stack

| Technology | Purpose |
|---|---|
| C# | Backend development |
| ASP.NET Core Web API | RESTful API |
| Entity Framework Core | ORM and data access |
| Microsoft SQL Server | Relational database |
| xUnit | Automated testing |
| Git & GitHub | Version control |
| Postman | API testing |
| Docker & Docker Compose | Planned containerization |
| GitHub Actions | Planned CI pipeline |

## Implemented Features

- [x] ASP.NET Core Web API project setup
- [x] SQL Server database integration
- [x] Entity Framework Core configuration
- [x] Database migrations
- [x] Event entity and data operations
- [x] Retrieve all events
- [x] Retrieve an event by ID
- [x] HTTP response handling (`200 OK`, `404 Not Found`)
- [x] Test project structure
- [x] Custom web application factory for integration testing

## Development Roadmap

### Testing & Code Quality
- [x] Create xUnit test project
- [x] Set up custom web application factory
- [ ] Implement tests for existing API endpoints
- [ ] Test successful and unsuccessful request scenarios
- [ ] Expand test coverage

### Continuous Integration
- [ ] Configure GitHub Actions
- [ ] Automate restore, build, and test workflows

### Containerization
- [ ] Create a Dockerfile for the API
- [ ] Configure Docker Compose
- [ ] Run API and SQL Server together using containers

### Event & Venue Management
- [ ] Complete event CRUD operations
- [ ] Expand venue management
- [ ] Define event capacity and seat models

### Reservation Engine
- [ ] Implement seat availability queries
- [ ] Develop reservation workflows
- [ ] Prevent duplicate seat reservations
- [ ] Handle concurrent reservation requests
- [ ] Ensure data consistency through transactions

### Ticketing & Notifications
- [ ] Generate digital tickets
- [ ] Explore PDF ticket generation
- [ ] Implement asynchronous confirmation workflows
- [ ] Add email notifications

## Engineering Goals

EventFlow is designed to provide hands-on experience with:

- RESTful API design and backend architecture
- Relational database modeling
- Entity Framework Core and database migrations
- Integration testing with xUnit
- Containerized application development
- CI/CD automation
- Transaction management and concurrency control
- Reliable reservation system design

## Project Status

**Status:** In Progress

EventFlow is actively being developed. The implementation and roadmap sections are updated as new features are completed.

The long-term goal is to build a reliable backend reservation engine that demonstrates practical software engineering skills beyond basic API development.

---

**Developer:** [Helin Bora](https://github.com/HelinBora)

**Repository:** [EventFlow](https://github.com/HelinBora/EventFlow)