# CI/CD Pipeline Using GitHub Actions: Automate Software Delivery (for free)

This project includes the code from my YouTube video on ["CI/CD Pipeline Using GitHub Actions: Automate Software Delivery (for free)"](https://www.youtube.com/watch?v=p3W2XCD3smk)

The purpose of this project is to show how you can use GitHub Actions as CI/CD pipeline instead of using tools such as TeamCity and Octopus.

This application is made up of the following projects:

| Project Name                            | Description                                  |
| --------------------------------------- | -------------------------------------------- |
| [GitHubActionsDemo.Api](src/GitHubActionsDemo.Api/)                   | Controllers and validation for the API       |
| [GitHubActionsDemo.Api.Sdk](src/GitHubActionsDemo.Api.Sdk/)               | Contracts and Refit SDK                      |
| [GitHubActionsDemo.Service](src/GitHubActionsDemo.Service)               | Business Logic Layer                         |
| [GitHubActionsDemo.Persistance](src/GitHubActionsDemo.Persistance)           | Code for setup and writing to the database   |
| [GitHubActionsDemo.Api.Unit.Tests](test/GitHubActionsDemo.Api.Unit.Tests)        | API unit tests with xunit                    |
| [GitHubActionsDemo.Service.Unit.Tests](test/GitHubActionsDemo.Service.Unit.Tests)    | Service unit tests with xunit                |
| [GitHubActionsDemo.Api.Integration.Tests](test/GitHubActionsDemo.Api.Integration.Tests) | Integration tests using Refit SDK with xunit |

## Running the project

In order to run the API you need to have the database running. You can run the following to spin up the database and the API.

**Mac & Linux**
```bash
export VERSION=0.1.0; docker-compose up
```

**Windows**
```
SET VERSION=0.1.0
docker-compose up
```

The API running in Docker uses port 5200. You can also run the API using `dotnet run` from the `GitHubActionsDemo.Api` folder.

You can find a postman collection for all of the API calls in the [docs](docs) folder.

## Running the tests
The tests can be run from the root folder using `dotnet test`. This will run both the unit tests and the integration tests.

If you just want to run the Unit or Integration tests by themselves you can use the following commands:

**Unit Tests**
```bash
dotnet test --filter Category=Unit
```

**Integration Tests**
```bash
dotnet test --filter Category=Integration
```

## GitHub Actions Workflow
The GitHub Actions workflow file [build-and-test.yml](.github/workflows/build-and-test.yml) demonstrates the following:

1. Setting up gitversion
2. Building dotnet core project
3. Running unit tests
4. Spinning up docker containers
5. Running integration tests against docker API
6. Displaying Test Results
7. Pushing tagged docker image to ECR
8. 