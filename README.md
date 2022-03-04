# Fuzzy Sets Calculator

Web application made with ASP.Net Core MVC and Docker to perform fuzzy set calculations. Work in progress.

The application uses dependency injection (DI) through `IServiceProvider` interface.

Because of certain limitations, this app doesn't use a database. Instead it uses a singleton service to store current state of the application in memory.

To support saving and loading of fuzzy sets, the Command pattern has been implemented.

Currently the app uses `Newtonsoft.Json` to save commands in JSON format for saving to a target filesystem.
The application makes use of `RGraph`'s Line chart. `RGraph` is a JavaScript library.

## Supported functionalities

- Creation of normal trapezoid fuzzy sets
  - L0 means a point at which trapezoid fuzzy set begins
  - L1 means a point at which trapezoid fuzzy set reaches 1
  - R1 means a point at which trapezoid fuzzy set starts decreasing from 1
  - R0 means a point at which trapezoid fuzzy set reaches 0
- Intersection of two fuzzy sets as Minimum function
- Union of two fuzzy sets as Maximum function

## Usage

You can use the project with or without Docker.

If you want to run it without Docker, simply change configuration on the toolbar of Visual Studio to run FuzzySetCalc or IIS Express instead of Docker.

To save your changes, use the Download Sets link at the top and save the JSON file on your device.
To load your changes, use the Load Sets link at the top, pick a file using that form and choose Upload.
