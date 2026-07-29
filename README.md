# Unity Analytics Service

A lightweight, provider-based analytics abstraction for Unity games.

Instead of coupling your game directly to a specific analytics SDK, this package provides a common interface for logging analytics events. Providers can be swapped or extended without changing your gameplay code.

Currently includes support for **GameAnalytics**, with the architecture designed to support additional providers such as Firebase or custom analytics services.

## Features

- Clean, provider-based architecture
- Decoupled from any specific analytics SDK
- Strongly typed analytics events
- Easily extendable with custom providers
- Simple integration into existing Unity projects

## Project Structure

```
AnalyticsService/
├── Enums/
├── EventModels/
├── Interfaces/
├── Manager/
├── Providers/
│   └── GameAnalytics/
├── AnalyticsInstaller.cs
├── AnalyticsExamples.cs
└── ServiceLocator.cs
```

## Supported Events

- Progression Events
- Business Events
- Ad Events
- Custom Events

The event system is designed to be extended with additional event types while keeping the public API consistent.

## Architecture

```
Game Code
      │
      ▼
AnalyticsManager
      │
      ▼
IAnalyticsService
      │
      ▼
GameAnalyticsService
```

Your game communicates only with `AnalyticsManager`. The manager delegates all analytics calls to an implementation of `IAnalyticsService`, allowing providers to be replaced without modifying gameplay code.

## Example

```csharp
AnalyticsManager.Instance.LogEvent(
    new ProgressionEvent(
        ProgressionStatus.Complete,
        "Level_10"
    )
);
```

## Adding a New Provider

1. Create a new class implementing `IAnalyticsService`.
2. Implement the required event logging methods.
3. Register the provider during initialization.
4. No gameplay code needs to change.

## Motivation

Most Unity projects become tightly coupled to a single analytics SDK. Replacing that SDK later often requires changes throughout the codebase.

This project aims to solve that problem by introducing a simple abstraction layer that keeps gameplay systems independent of the underlying analytics implementation.

## Future Plans

- Firebase Analytics provider
- Unity Analytics provider
- Dependency Injection support
- UPM package distribution
- Unit tests
- Sample scene

## License

MIT
