using AnalyticsService.Interfaces;

namespace Analytics.EventModels
{
    /// <summary>
    /// Represents a custom design event.
    /// Pass clean string parts — the provider is responsible for
    /// joining them with whatever separator its SDK requires.
    ///
    /// Example:
    ///   new CustomEvent(null, "World1", "Level2", "Shops")
    ///   → GameAnalytics receives "World1:Level2:Shops"
    /// </summary>
    public class CustomEvent:IAnalyticsEvent
    {
        public string[] EventParts { get; }
        public float?   Value      { get; }

        /// <param name="value">Optional numeric value attached to the event.</param>
        /// <param name="eventParts">Ordered parts that form the event ID.</param>
        public CustomEvent(float? value = null, params string[] eventParts)
        {
            EventParts = eventParts;
            Value      = value;
        }
    }
}