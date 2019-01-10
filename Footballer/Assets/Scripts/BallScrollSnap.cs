using UnityEngine;
using UnityEngine.UI;

public class BallScrollSnap : ScrollSnapRect
{

    public static bool[] unlocked = new bool[3];
    public static bool[] selected = new bool[3];

    int coins;

    [SerializeField]
    public Button selectButton;
    [SerializeField]
    public Text selectText;

    static int selectedIdex = 0;

    public override void Start()
    {
        base.Start();


        SaveUnlockedLevel();

        selectButton.gameObject.SetActive(true);

        selectedIdex =  SecurePlayerPrefs.GetInt("SelectedBall", selectedIdex);
        selected[selectedIdex] = true; //Set selected item
        IsSelected();

    }
    public override void Update()
    {
        base.Update();

        IsSelected();
    }

    public override void LerpToPage(int aPageIndex)
    {
        base.LerpToPage(aPageIndex);

        switch (currentPage)
        {
            case 0:
                buyButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(true);

                IsSelected();

                break;
            case 1:

                OnCurrentPage();

                break;
            case 2:

                OnCurrentPage();
                
                break;
        }
    }

    public void BuyButton()
    {

        if (coins >= priceInCoins[currentPage - 1])
        {
            ScoresManager.Instance.Buy(priceInCoins[currentPage - 1]);

            container.GetChild(currentPage).GetComponent<SpriteRenderer>().color = unlockedColor;

            buyButton.gameObject.SetActive(false);

            unlocked[currentPage] = true;
            SecurePlayerPrefs.SetBool("BallSkin" + currentPage.ToString(), unlocked[currentPage]);

            ShopUIManager.Instance.UpdateCoin();

           // Debug.Log("-" + priceInCoins[currentPage - 1]);
            selectButton.gameObject.SetActive(true);


        }

    }

    void SaveUnlockedLevel()
    {
        SecurePlayerPrefs.Init();
        coins = SecurePlayerPrefs.GetInt("CoinScore");

        for (int i = 1; i < unlocked.Length; i++)
        {
            unlocked[i] = SecurePlayerPrefs.GetBool("BallSkin" + i.ToString(), unlocked[i]);
        }
    }

    public void SelectButton()
    {
        selectedIdex = currentPage;
        SecurePlayerPrefs.SetInt("SelectedBall", selectedIdex);

        //Ball.ballInstance.skinID = selectedIdex;

        for (int i = 0; i < selected.Length; i++)
        {
            if (i == currentPage)
                selected[i] = true;
            else
                selected[i] = false;
        }
    }

    void OnCurrentPage()
    {
        if (!unlocked[currentPage])
        {
            buyButton.gameObject.SetActive(true);
            unlockTextButton.text = "-" + priceInCoins[currentPage - 1] + " x";
            selectButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(false);
            container.GetChild(currentPage).GetComponent<SpriteRenderer>().color = unlockedColor;
            selectButton.gameObject.SetActive(true);

            IsSelected();
        }
    }

    void IsSelected()
    {
        if (!selected[currentPage])
            selectText.text = "SELECT";
        
        else
            selectText.text = "SELECTED";
    }
}
