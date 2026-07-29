using Analytics;
using Analytics.Enums;
using Analytics.EventModels;
using AnalyticsService.EventModels;
using AnalyticsService.Interfaces;
using UnityEngine;

/// <summary>
/// Usage examples — not meant to be shipped, just for reference.
/// </summary>
public class AnalyticsExamples : MonoBehaviour
{
    private IAnalyticsService _analytics;

    private void Start()
    {
        _analytics = ServiceLocator.Get<IAnalyticsService>();
    }

    // ── Ads ────────────────────────────────────────────────────────────────────

    public void OnRewardedAdShown()
    {
        _analytics.TrackAds(new AdsEvent(
            adType    : AdType.RewardedVideo,
            result    : AdResult.Show,
            placement : "coins_double",
            sdkName   : "IronSource"
        ));
    }

    public void OnBannerClicked()
    {
        _analytics.TrackAds(new AdsEvent(
            adType    : AdType.Banner,
            result    : AdResult.Clicked,
            placement : "main_menu_banner",
            sdkName   : "AdMob"
        ));
    }

    // ── Business ───────────────────────────────────────────────────────────────

    public void OnCoinPackPurchased()
    {
        _analytics.TrackBusiness(new BusinessEvent(
            currency : "USD",
            amount   : 99,       // $0.99 in cents
            itemType : "Consumable",
            itemId   : "coins_100",
            cartType : "ShopScreen"
        ));
    }

    // ── Custom ─────────────────────────────────────────────────────────────────
    // Pass clean parts — GameAnalyticsService joins them as "Part1:Part2:Part3"

    public void OnShopOpened()
    {
        // → "World1:Level2:Shops"
        _analytics.TrackCustom(new CustomEvent(null, "World1", "Level2", "Shops"));
    }

    public void OnTutorialStepCompleted(int step)
    {
        // → "Tutorial:StepCompleted" with value
        _analytics.TrackCustom(new CustomEvent(step, "Tutorial", "StepCompleted"));
    }

    public void OnPlayButtonPressed()
    {
        // → "UI:MainMenu:PlayButton"
        _analytics.TrackCustom(new CustomEvent(null, "UI", "MainMenu", "PlayButton"));
    }

    // ── Progression ────────────────────────────────────────────────────────────

    public void OnLevelStarted()
    {
        _analytics.TrackProgression(new ProgressionEvent(
            status : ProgressionStatus.Start,
            world  : "World01",
            level  : "Level03"
        ));
    }

    public void OnLevelCompleted(int score)
    {
        _analytics.TrackProgression(new ProgressionEvent(
            status : ProgressionStatus.Complete,
            world  : "World01",
            level  : "Level03",
            score  : score
        ));
    }

    public void OnLevelFailed()
    {
        _analytics.TrackProgression(new ProgressionEvent(
            status : ProgressionStatus.Fail,
            world  : "World01",
            level  : "Level03",
            phase  : "Phase02"
        ));
    }
}
