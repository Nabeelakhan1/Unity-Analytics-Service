using Analytics.Core;
using Analytics.Providers;
using AnalyticsService.Interfaces;
using UnityEngine;

namespace Analytics
{
    public class AnalyticsInstaller:MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            var manager = new AnalyticsManager();

            manager.RegisterProvider(new GameAnalyticsService());
            // manager.RegisterProvider(new AdjustService());
            // manager.RegisterProvider(new FirebaseService());

            ServiceLocator.Register<IAnalyticsService>(manager);

            Debug.Log("[AnalyticsInstaller] Analytics service initialized.");
        }
    }
}