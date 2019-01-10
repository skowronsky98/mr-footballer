using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class HomePageManager : MonoBehaviour {

    public Text coinsText;
    private string numberOfCoins;

    public Text signInOutText;

    private string lang_signInVal = "Sign In";
    private string lang_signOutVal = "Sign Out";

    [SerializeField]
    private Canvas settingsCanvas;
    [SerializeField]
    private GameObject menuCanvas;


    private bool pressedSettingBtn = false;
      
    public static HomePageManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {

        SecurePlayerPrefs.Init();
        numberOfCoins = SecurePlayerPrefs.GetInt("CoinScore").ToString();
               
        coinsText.text = numberOfCoins + " x";

        menuCanvas.SetActive(true);
        settingsCanvas.enabled = false;
        pressedSettingBtn = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
       
    }


    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void GoToShop()
    {
        SceneManager.LoadScene(2);
    }

    public void SettingBtn()
    {
        if (!pressedSettingBtn)
        {
            menuCanvas.SetActive(false);
            settingsCanvas.enabled = true;

            pressedSettingBtn = true;
        }
        else
        {
            menuCanvas.SetActive(true);
            settingsCanvas.enabled = false;

            pressedSettingBtn = false;
        }
       

    }

    public void ShowLeaderboard()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesManager.ShowLeaderboardsUI();
        }
        else
        {
            SignInOutAction();
        }
    }

    #region PlayGamesSettings

    private void SignInOutButtonText(bool isSignedIn)
    {
        if(isSignedIn)
        {
            signInOutText.text = lang_signOutVal.ToString();
        }
        else
        {
            signInOutText.text = lang_signInVal.ToString();
        }
    }

    public void CheckAuthButtonState()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            SignInOutButtonText(true);
        }
        else
        {
            SignInOutButtonText(false);
        }
    }


    public void SignInOutAction()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //Authenticate
            PlayGamesManager.Instance.DoAuthenticate();
        }
        else
        {
            //Sign Out
            PlayGamesManager.Instance.SignOut();
        }
    }

    #endregion /PlayGamesSettings

    public void ShowTips()
    {
        SecurePlayerPrefs.SetBool("NotFirstTime", false);
    }


}
