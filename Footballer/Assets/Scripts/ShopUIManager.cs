using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUIManager : MonoBehaviour {

    public Text coinsText;
    private string numberOfCoins;

    public GameObject playerPanel;
    public GameObject ballPanel;
    public GameObject bGPanel;
    public GameObject coinPanel;

    public Canvas buttonCanvas;

    public static ShopUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {

        SecurePlayerPrefs.Init();
        numberOfCoins = SecurePlayerPrefs.GetInt("CoinScore").ToString();

        coinsText.text = numberOfCoins + " x";

        playerPanel.SetActive(false);
        ballPanel.SetActive(false);
        bGPanel.SetActive(false);
        coinPanel.SetActive(false);

    }

    void Update ()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }

    public void BackBtuttonShop()
    {
        if (buttonCanvas.enabled)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            if (playerPanel.activeSelf)
            {
                playerPanel.SetActive(false);
                buttonCanvas.enabled = true;
            }
            if (ballPanel.activeSelf)
            {
                ballPanel.SetActive(false);
                buttonCanvas.enabled = true;
            }
            if (bGPanel.activeSelf)
            {
                bGPanel.SetActive(false);
                buttonCanvas.enabled = true;
            }
            if (coinPanel.activeSelf)
            {
                coinPanel.SetActive(false);
                buttonCanvas.enabled = true;
            }
        }
    }

    public void UpdateCoin()
    {
        numberOfCoins = SecurePlayerPrefs.GetInt("CoinScore").ToString();

        coinsText.text = numberOfCoins + " x";
    }


    public void PlayerBtn()
    {
        buttonCanvas.enabled = false;
        playerPanel.SetActive(true);

    }

    public void BallBtn()
    {
        buttonCanvas.enabled = false;
        ballPanel.SetActive(true);

    }

    public void BGBtn()
    {
        buttonCanvas.enabled = false;
        bGPanel.SetActive(true);

    }

    public void CoinBtn()
    {
        buttonCanvas.enabled = false;
        coinPanel.SetActive(true);

    }


}
