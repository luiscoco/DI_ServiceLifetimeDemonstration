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

## Dependency injection in .NET

In .NET, the dependency injection container is responsible for managing the instantiation and lifetime of objects (services) and their dependencies. It helps facilitate the practice of dependency injection by allowing you to register and resolve dependencies throughout your application.

Here are the core concepts and features of the dependency injection container in .NET:

## Service Registration: 
You can register your services and their implementations with the container. This is typically done during application startup.

## Service Resolution: 
The container can resolve dependencies for you by creating instances of services and their dependencies. You can request a service from the container, and it will construct the object graph for you.

## Lifetime Management: 
The container allows you to specify the lifetime of a service registration (transient, scoped, or singleton) to control how instances are created and shared.

## Constructor Injection: 
The container can automatically inject dependencies into the constructor of the dependent class, helping to enforce the principle of inversion of control.

Configuration Flexibility: The container often provides ways to configure the behavior of service registration, including specifying custom factory methods, conditions for registration, and more.

 Here's a simple example that demonstrates the five core concepts of dependency injection using the Microsoft.Extensions.DependencyInjection library. This example is based on previous versions of .NET Core, and while the syntax may be similar in .NET 7, please refer to the official documentation for any specific changes.

Let's assume you have the following interfaces and classes:

```csharp
// Services

public interface IWeatherService
{
    string GetWeather();
}

public class WeatherService : IWeatherService
{
    public string GetWeather()
    {
        return "It's sunny today!";
    }
}

public interface IOutputService
{
    void Write(string message);
}

public class ConsoleOutputService : IOutputService
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}

// Application

public class WeatherReporter
{
    private readonly IWeatherService _weatherService;
    private readonly IOutputService _outputService;

    public WeatherReporter(IWeatherService weatherService, IOutputService outputService)
    {
        _weatherService = weatherService;
        _outputService = outputService;
    }

    public void ReportWeather()
    {
        var weather = _weatherService.GetWeather();
        _outputService.Write(weather);
    }
}
```

Now, let's set up dependency injection and demonstrate the five concepts in action:

```csharp
using Microsoft.Extensions.DependencyInjection;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Step 1: Create a service collection
        var services = new ServiceCollection();

        // Step 2: Register services
        services.AddTransient<IWeatherService, WeatherService>();
        services.AddSingleton<IOutputService, ConsoleOutputService>();
        services.AddScoped<WeatherReporter>();

        // Step 3: Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Step 4: Resolve and use services
        var weatherReporter = serviceProvider.GetRequiredService<WeatherReporter>();
        weatherReporter.ReportWeather();

        // Dispose of the service provider when done
        serviceProvider.Dispose();
    }
}
```

In this example:

## Service Registration: 
We register IWeatherService and IOutputService with their respective implementations, WeatherService and ConsoleOutputService. We also register WeatherReporter which has dependencies on the two services.

## Service Resolution: 
We use the GetRequiredService method to resolve an instance of WeatherReporter, which in turn resolves its dependencies (IWeatherService and IOutputService).

## Lifetime Management: 
We use AddTransient for IWeatherService, which creates a new instance every time it's requested, and AddSingleton for IOutputService, which shares a single instance. WeatherReporter is registered with AddScoped, meaning a new instance is created for each scope (in this case, the whole application).

## Constructor Injection: 
The WeatherReporter constructor is automatically injected with instances of IWeatherService and IOutputService.

## Configuration Flexibility: 
While not shown in this example, the ServiceCollection provides various methods for configuring service registrations, including custom factory methods and options.
