using Analytics.Enums;
using AnalyticsService.Interfaces;

namespace AnalyticsService.EventModels
{
    /// <summary>
    /// Represents a player's progression through a level or stage.
    /// World and Level are required; Phase and Score are optional.
    /// </summary>
    public class ProgressionEvent:IAnalyticsEvent
    {
        public ProgressionStatus Status { get; }
        public string            World  { get; }  // e.g. "World01"
        public string            Level  { get; }  // e.g. "Level03"
        public string            Phase  { get; }  // optional sub-stage
        public int?              Score  { get; }

        public ProgressionEvent(ProgressionStatus status, string world, string level,
            string phase = null, int? score = null)
        {
            Status = status;
            World  = world;
            Level  = level;
            Phase  = phase;
            Score  = score;
        }
    }
}