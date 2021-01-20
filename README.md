# Interview Game API

[![Build Status](https://travis-ci.com/benthepoet/interview-game-api.svg?branch=master)](https://travis-ci.com/benthepoet/interview-game-api)

This project provides an API for users to keep lists of their favorite games.

## Getting Started
This project requires the following prerequisites.

* .NET Core 3.1 SDK ([link](https://dotnet.microsoft.com/download))
* Visual Studio 2019 ([link](https://visualstudio.microsoft.com/))

## Configuration 
In order to use the API you must first obtain an API key from RAWG ([link](https://rawg.io/)) as their service is 
used to retrieve game details.

For testing the RAWG API key can be set in the user secrets file ([link](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)) as below. User secrets should only be used in development and are not included in the project files.

```json
{
  "RAWG": {
    "ApiKey": "your-key-goes-here"
  }
}
```

In a production environment this key can be set using an environment variable named `RAWG__ApiKey`.

### Caching
In order to reduce redundant calls to the RAWG API and provide a better user experience, game details are cached 
in memory. The default TTL (time-to-live) for cached game details is 10 minutes. This can be adjusted in `appsettings.json` file as below.

```json
{
  "RAWG": {
    "GameCacheTTL": 60
  }
}
```

## Running the Project
To run the project, open the `GameAPI.sln` solution file in Visual Studio and then from the menu select `Debug -> Start Debugging`.
A browser should then open and display a welcome message.