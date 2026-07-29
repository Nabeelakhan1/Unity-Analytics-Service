using Analytics.Enums;
using AnalyticsService.Interfaces;

namespace Analytics.EventModels
{
    /// <summary>
    /// Represents an ad lifecycle event (show, click, fail, etc.).
    /// </summary>
    public class AdsEvent:IAnalyticsEvent
    {
        public AdType   AdType    { get; }
        public AdResult Result    { get; }
        public string   Placement { get; }
        public string   SdkName   { get; }  // e.g. "AdMob", "IronSource"

        public AdsEvent(AdType adType, AdResult result, string placement, string sdkName)
        {
            AdType    = adType;
            Result    = result;
            Placement = placement;
            SdkName   = sdkName;
        }
    }
}