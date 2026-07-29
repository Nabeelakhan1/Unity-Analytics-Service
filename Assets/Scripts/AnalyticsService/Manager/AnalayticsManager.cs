using System;
using System.Collections.Generic;
using Analytics.EventModels;
using AnalyticsService.Interfaces;
using UnityEngine;

namespace Analytics.Core
{
    /// <summary>
    /// Orchestrates all registered analytics providers.
    /// Fans every event out to each provider in sequence.
    ///
    /// SOLID:
    ///   - OCP : Register new providers at runtime and no code changes needed here.
    ///   - SRP : Only responsible for dispatching; knows nothing about SDKs.
    ///   - DIP : Works entirely against IAnalyticsService abstractions.
    ///
    /// Usage:
    ///   var manager = new AnalyticsManager();
    ///   manager.RegisterProvider(new GameAnalyticsService());
    ///   // manager.RegisterProvider(new AdjustService());
    ///   // manager.RegisterProvider(new FirebaseService());
    /// </summary>
    public class AnalyticsManager : IAnalyticsService
    {
        private readonly List<IAnalyticsService> _providers = new List<IAnalyticsService>();

        // ── Registration ──

        public void RegisterProvider(IAnalyticsService provider)
        {
            if (provider == null)
            {
                Debug.LogWarning("[AnalyticsManager] Attempted to register a null provider.");
                return;
            }

            provider.Initialize();
            _providers.Add(provider);
            Debug.Log($"[AnalyticsManager] Registered provider: {provider.GetType().Name}");
        }

        // ── IAnalyticsService ──

        /// <summary>
        /// Re-initializes all registered providers.
        /// Prefer calling Initialize() per provider via RegisterProvider().
        /// </summary>
        public void Initialize()
        {
            foreach (var provider in _providers)
                Dispatch(provider, p => p.Initialize());
        }

        public void TrackAds(AdsEvent analyticsEvent)
        {
            foreach (var provider in _providers)
                Dispatch(provider, p => p.TrackAds(analyticsEvent));
        }

        public void TrackBusiness(BusinessEvent analyticsEvent)
        {
            foreach (var provider in _providers)
                Dispatch(provider, p => p.TrackBusiness(analyticsEvent));
        }

        public void TrackCustom(CustomEvent analyticsEvent)
        {
            foreach (var provider in _providers)
                Dispatch(provider, p => p.TrackCustom(analyticsEvent));
        }

        public void TrackProgression(ProgressionEvent analyticsEvent)
        {
            foreach (var provider in _providers)
                Dispatch(provider, p => p.TrackProgression(analyticsEvent));
        }

        // ── Private ───

        private void Dispatch(IAnalyticsService provider, Action<IAnalyticsService> action)
        {
            try
            {
                action(provider);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AnalyticsManager] {provider.GetType().Name} threw an exception: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
