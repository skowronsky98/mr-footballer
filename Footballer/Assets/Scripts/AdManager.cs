using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public RewardBasedVideoAd RewardBaseVideoAd { get; set; }

    public static AdManager AdManagerInstance { get; set; }

    private int rewardCoins = 5;

    public static bool dontDestroy = true;

    public void Start()
    {
        RewardBaseVideoAd = RewardBasedVideoAd.Instance;

        AdManagerInstance = this;

        if (dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
            dontDestroy = false;
        }



        RewardBaseVideoAd.OnAdClosed += HandleOnAdClosed;
        RewardBaseVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        RewardBaseVideoAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        RewardBaseVideoAd.OnAdLoaded += HandleOnAdLoaded;
        RewardBaseVideoAd.OnAdOpening += HandleOnAdOpening;
        RewardBaseVideoAd.OnAdRewarded += HandleOnAdRewarded;
        RewardBaseVideoAd.OnAdStarted += HandleOnAdStarted;


    }
   
    public void LoadRewardBasedAd()
    {
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-2158257675236524/6114116867";
#elif UNITY_IPHONE
            string adUnitId = "";
#else
            string adUnitId = "unexpected_platform";
#endif
        // My Android Video: ca-app-pub-2158257675236524/6114116867

        // Test video: ca-app-pub-3940256099942544/5224354917
        RewardBaseVideoAd.LoadAd(new AdRequest.Builder().Build(), adUnitId);
                
    }

    public void ShowRewardBasedAd()
    {
        if (RewardBaseVideoAd.IsLoaded())
        {
            RewardBaseVideoAd.Show();
        }
      
    }





    #region Rewarded Video callback handlers

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Invoke("LoadRewardBasedAd", 3f);
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        // Pause the action
        Time.timeScale = 0;
    }

    public void HandleOnAdStarted(object sender, EventArgs args)
    {
        // Mute audio
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // Crank the party back up
        Time.timeScale = 1;

        LoadRewardBasedAd();

    } 

    public void HandleOnAdRewarded(object sender, Reward args)
    {
        // Reward the user 

        string type = args.Type;
        double amount = args.Amount;

        MonoBehaviour.print(String.Format("You just got {0} {1}!", amount, type));

        ScoresManager.Instance.UpdateCoinVideoAdScore(rewardCoins);
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {

    }
    #endregion

    


}
