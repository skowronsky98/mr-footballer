using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayGamesManager : MonoBehaviour {

    public static PlayGamesManager Instance;
    private bool wasAuth, triedAuth;

    ShowToast Toaster = new ShowToast();

    private static readonly PlayGamesClientConfiguration ClientConfiguration =
    new PlayGamesClientConfiguration.Builder()
        //github issue 340->  .EnableSavedGames()
        .RequestEmail()
        .RequestServerAuthCode(false)
        .RequestIdToken()
        .Build();

    private string idToken = "";
    private string authCode = "";

    void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start () {
        SecurePlayerPrefs.Init();

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
     
        triedAuth = SecurePlayerPrefs.GetBool("triedAuth");
        wasAuth = SecurePlayerPrefs.GetBool("wasAuth");

        if (!triedAuth) //running only after first app run
        {
            DoAuthenticate();
            SecurePlayerPrefs.SetBool("triedAuth", true);
            triedAuth = true;
         }
         else if (wasAuth) //running only after first scene run
         {
              if (!PlayGamesPlatform.Instance.IsAuthenticated())
              {
                  DoAuthenticate();
              }
         }

       // DoAuthenticate();

    }



    internal void DoAuthenticatetest()
    {
        PlayGamesPlatform.InitializeInstance(ClientConfiguration);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log( "Authenticated. Hello, " + Social.localUser.userName + " (" +
                Social.localUser.id + ")");
                Toaster.MyShowToast("Authenticated. Hello, " + Social.localUser.userName + " (" +
                Social.localUser.id + ")");
               

            }
            else
            {
                Debug.Log( "*** Failed to authenticate.");
                Toaster.MyShowToast("*** Failed to authenticate.");

            }

        });
    }


    internal void DoAuthenticate()
    {
        PlayGamesPlatform.InitializeInstance(ClientConfiguration);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
             // if we signed in successfully, load data from cloud
             Toaster.MyShowToast("Authenticated. Hello, " + Social.localUser.userName + " (" +
             Social.localUser.id + ")");
             
             SecurePlayerPrefs.SetBool("wasAuth", true); //store info to login user again next time

        //    ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
            }
            else
            {
                Toaster.MyShowToast("Failed to sign in with Google Play Games.");
            }
            HomePageManager.Instance.CheckAuthButtonState();
        });
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        Toaster.MyShowToast("Signed out");
        HomePageManager.Instance.CheckAuthButtonState();

        SecurePlayerPrefs.SetBool("wasAuth", false); //do not login automatically
        SecurePlayerPrefs.SetBool("triedAuth", false); //prompt login on start next time
    }

    #region Achievements

    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100.0f, success => 
        {
            Debug.Log("achivment collected" + success.ToString());
        });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => {

        });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards

}
