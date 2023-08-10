# DI_ServiceLifetimeDemonstration

Dependency injection lifetime refers to how long a particular instance of a registered service should be kept and reused within the application. In .NET Core,
there are typically three main lifetime options for registered services:

## Transient: 
A new instance of the service is created every time it's requested from the dependency injection container. Transient services are suitable for lightweight,
stateless components.

## Scoped: 
A single instance of the service is created for each scope. A scope usually corresponds to a web request in web applications. 
Within the same scope, the same instance of the service is reused. Scoped services are suitable for components that maintain state throughout the duration 
of a specific operation.

## Singleton: 
Only one instance of the service is created and shared across the entire application's lifetime. Singleton services are suitable for components that need
to maintain state globally and need to be shared across multiple parts of the application.

Here's an example of how you might register services with different lifetimes in .NET Core's Startup class:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IMyTransientService, MyTransientService>();
    services.AddScoped<IMyScopedService, MyScopedService>();
    services.AddSingleton<IMySingletonService, MySingletonService>();
}
```

In the above example:

IMyTransientService is registered as transient, meaning a new instance of MyTransientService will be created every time it's requested.

IMyScopedService is registered as scoped, meaning one instance of MyScopedService will be created per scope (usually per web request).

IMySingletonService is registered as a singleton, meaning only one instance of MySingletonService will be created and shared across the entire application.

It's important to choose the appropriate lifetime for each service based on its intended usage and the requirements of your application. Choosing the right lifetime ensures that your application behaves correctly, maintains the desired state management, and performs efficiently.
