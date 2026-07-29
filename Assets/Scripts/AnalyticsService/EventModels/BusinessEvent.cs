using AnalyticsService.Interfaces;

namespace Analytics.EventModels
{
    
    /// <summary>
    /// Represents a real-money purchase event.
    /// Amount should be provided in cents (e.g. $0.99 = 99).
    /// Currency should follow ISO 4217 (e.g. "USD", "EUR").
    /// </summary>
    public class BusinessEvent:IAnalyticsEvent
    {
        public string Currency { get; }
        public int    Amount   { get; }  // in cents
        public string ItemType { get; }
        public string ItemId   { get; }
        public string CartType { get; }

        public BusinessEvent(string currency, int amount, string itemType, string itemId, string cartType = null)
        {
            Currency = currency;
            Amount   = amount;
            ItemType = itemType;
            ItemId   = itemId;
            CartType = cartType;
        }
    }
}