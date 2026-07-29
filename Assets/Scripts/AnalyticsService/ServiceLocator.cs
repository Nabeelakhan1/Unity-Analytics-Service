using System;
using System.Collections.Generic;
using UnityEngine;

namespace Analytics
{
    /// <summary>
    /// Lightweight service locator.
    /// Swap this out for Zenject / VContainer if your project uses a DI framework.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public static T Get<T>()
        {
            if (_services.TryGetValue(typeof(T), out var service))
                return (T)service;

            Debug.LogError($"[ServiceLocator] Service of type {typeof(T).Name} is not registered.");
            return default;
        }

        public static bool IsRegistered<T>() => _services.ContainsKey(typeof(T));
    }
}
