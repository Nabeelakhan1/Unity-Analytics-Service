using Analytics.Enums;
using Analytics.EventModels;
using AnalyticsService.Interfaces;
using GameAnalyticsSDK;

namespace Analytics.Providers
{
    /// <summary>
    /// GameAnalytics implementation of IAnalyticsService.
    ///
    /// Single Responsibility:
    ///   - Knows only how to map our event models to the GameAnalytics SDK.
    ///   - Owns the ":" separator logic for CustomEvents — callers stay clean.
    /// </summary>
    public class GameAnalyticsService : IAnalyticsService
    {
        // ── Lifecycle ──────────────────────────────────────────────────────────

        public void Initialize()
        {
            GameAnalytics.Initialize();
        }

        // ── Ads ────────────────────────────────────────────────────────────────

        public void TrackAds(AdsEvent analyticsEvent)
        {
            GAAdType   adType    = MapAdType(analyticsEvent.AdType);
            GAAdAction adAction  = MapAdResult(analyticsEvent.Result);

            GameAnalytics.NewAdEvent(adAction, adType, analyticsEvent.SdkName, analyticsEvent.Placement);
        }

        // ── Business ───────────────────────────────────────────────────────────

        public void TrackBusiness(BusinessEvent analyticsEvent)
        {
            GameAnalytics.NewBusinessEvent(
                currency : analyticsEvent.Currency,
                amount   : analyticsEvent.Amount,
                itemType : analyticsEvent.ItemType,
                itemId   : analyticsEvent.ItemId,
                cartType : analyticsEvent.CartType ?? "default"
            );
        }

        // ── Custom ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Joins EventParts with ":" as required by the GameAnalytics SDK.
        /// e.g. ["World1", "Level2", "Shops"] → "World1:Level2:Shops"
        /// </summary>
        public void TrackCustom(CustomEvent analyticsEvent)
        {
            string eventId = string.Join(":", analyticsEvent.EventParts);

            if (analyticsEvent.Value.HasValue)
                GameAnalytics.NewDesignEvent(eventId, analyticsEvent.Value.Value);
            else
                GameAnalytics.NewDesignEvent(eventId);
        }

        // ── Progression ────────────────────────────────────────────────────────

        public void TrackProgression(ProgressionEvent analyticsEvent)
        {
            GAProgressionStatus status = MapProgressionStatus(analyticsEvent.Status);
            string phase = analyticsEvent.Phase ?? string.Empty;

            if (analyticsEvent.Score.HasValue)
            {
                GameAnalytics.NewProgressionEvent(
                    status, analyticsEvent.World, analyticsEvent.Level, phase, analyticsEvent.Score.Value);
            }
            else
            {
                GameAnalytics.NewProgressionEvent(
                    status, analyticsEvent.World, analyticsEvent.Level, phase);
            }
        }

        // ── Private Mappers ────────────────────────────────────────────────────

        private GAAdType MapAdType(AdType adType) => adType switch
        {
            AdType.RewardedVideo => GAAdType.RewardedVideo,
            AdType.Interstitial  => GAAdType.Interstitial,
            AdType.Banner        => GAAdType.Banner,
            AdType.OfferWall     => GAAdType.OfferWall,
            _                    => GAAdType.Undefined
        };

        private GAAdAction MapAdResult(AdResult result) => result switch
        {
            AdResult.Clicked    => GAAdAction.Clicked,
            AdResult.Show       => GAAdAction.Show,
            AdResult.FailedShow => GAAdAction.FailedShow,
            _                   => GAAdAction.Undefined
        };

        private GAProgressionStatus MapProgressionStatus(ProgressionStatus status) => status switch
        {
            ProgressionStatus.Start    => GAProgressionStatus.Start,
            ProgressionStatus.Complete => GAProgressionStatus.Complete,
            ProgressionStatus.Fail     => GAProgressionStatus.Fail,
            _                          => GAProgressionStatus.Undefined
        };
    }
}
