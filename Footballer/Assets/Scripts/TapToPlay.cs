using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToPlay : MonoBehaviour
{

    public Canvas tapToPlayMenu;
    public Button tapButton;

    public bool isActive;

    public static TapToPlay tapToPlayInstance;

    private void Awake()
    {
        tapToPlayInstance = this;
    }

    void Start()
    {

        tapToPlayMenu = GetComponent<Canvas>();
        tapButton = GetComponent<Button>();

        tapToPlayMenu.enabled = true;
        isActive = true;

        UIManager.uiManagerInstance.textPause.enabled = true;

        // ServerConm.nection.CheckNetwork();

    }


    public void Tap()
    {
        isActive = false;
        UIManager.uiManagerInstance.textPause.enabled = true;

        Ball.ballInstance.gameObject.SetActive(true);

        if (LevelSelection.index != 4)
        {
            CoinSpawner.coinSpawnerInstance.canCountToSpawn = true;
        }

        Destroy(gameObject);
        //Tips To DO !!!!!!!
        //Tips.Instance.CheckForTips();
        Tips.Instance.DesactivateTips();

    }
}
