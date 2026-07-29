using Analytics.EventModels;

namespace AnalyticsService.Interfaces
{
    /// <summary>
    /// Contract every analytics provider must fulfil.
    /// Depend on this abstraction — never on a concrete provider.
    ///
    /// SOLID:
    ///   - OCP  : Add new providers without modifying this interface.
    ///   - LSP  : Any implementation can substitute another transparently.
    ///   - DIP  : Call-sites depend on this, not on concrete classes.
    /// </summary>
    public interface IAnalyticsService
    {
        void Initialize();

        void TrackAds        (AdsEvent         analyticsEvent);
        void TrackBusiness   (BusinessEvent    analyticsEvent);
        void TrackCustom     (CustomEvent      analyticsEvent);
        void TrackProgression(ProgressionEvent analyticsEvent);
    }
}
