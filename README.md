# Fuzzy Sets Calculator

Web application made with ASP.Net Core MVC and Docker to perform fuzzy set calculations. Work in progress.

The application uses dependency injection (DI) through `IServiceProvider` interface.

Because of certain limitations, this app doesn't use a database. Instead it uses a singleton service to store current state of the application in memory.

To support saving and loading of fuzzy sets, the Command pattern has been implemented.

Currently the app uses `Newtonsoft.Json` to save commands to a target filesystem.
