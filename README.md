# IHVolunteeringAPI
RESTful API to serve data to and from the IH Volunteering applications

## Running locally
From the `IHVolunteerAPI` directory of the project where the `.csproj` folder exists, run the following:

```bash
dotnet run
```

This should spin up the server locally, accessible on `https://localhost:5008/api/<Route>`.

### Environment Variables (User Secrets)
You will need to set some environment variables (user secrets) in order for the application to work. Locally, you can run:

```bash
dotnet user-secrets set "API:Key" "someapikey"
dotnet user-secrets set "API:IV" "someinitvector"
```

## Swagger
A Swagger Open API spec for the API should load automatically when the application is run, but is also accessible on `https://localhost:5008`.

## Postgres
You need to have an instance of postgres running on port 5432 for the API to communicate with. To get an instance spun up, run:

```bash
docker-compose up --build postgres
```

You may need to modify the configuration to have the proper `Username` value for your DB context. This value is typically set to `postgres`.
