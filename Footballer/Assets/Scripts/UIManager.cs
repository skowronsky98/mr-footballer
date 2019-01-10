using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {

    [SerializeField]
    public Text textPause = null;

    public Color colorOfPause;
    public Color colorOfUnPause;

    public FontStyle fontPause;
    public FontStyle fontUnPause;

    public AudioSource musicAudio;

    public bool paused = true;

    public static UIManager uiManagerInstance;

    private void Awake()
    {
        uiManagerInstance = this;
    }

    void Start () {

        musicAudio = GetComponent<AudioSource>();

        if (Mute.muteMusic)
        {
            musicAudio.enabled = false;
        }

        textPause.color = colorOfPause;
        textPause.fontStyle = fontPause;

    }


    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;

            textPause.color = colorOfUnPause;
            textPause.fontStyle = fontUnPause;
            Fallout.falloutInstance.falloutCanvas.enabled = true;
            Fallout.falloutInstance.playerCanvasController.enabled = false;
            paused = true;

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

            textPause.color = colorOfPause;
            textPause.fontStyle = fontPause;
            Fallout.falloutInstance.falloutCanvas.enabled = false;
            Fallout.falloutInstance.playerCanvasController.enabled = true;
            paused = false;
            


        }

    }

    public void HomeScene()
    {

        if (UIManager.uiManagerInstance.paused)
        {
            UIManager.uiManagerInstance.Pause();
        }
        SceneManager.LoadScene(0);

    }

    public void PlayButton()
    {

        if (!paused)
        {
            Fallout.falloutInstance.falloutCanvas.enabled = false;
            Fallout.falloutInstance.playerCanvasController.enabled = true;
            ScoresManager.Instance.ResetScore();
            ScoresManager.Instance.ProcessScores(true);
            Ball.ballInstance.gameObject.SetActive(true);

            textPause.enabled = true;
            if (LevelSelection.index != 4)
            {
                CoinSpawner.coinSpawnerInstance.canCountToSpawn = true;
            }
            else
            {
                if (BasketHookSpawner.Instance.coinMove)                
                    BasketHook.Instance.DestroyBasket();
            }

        }
        else
        {
            Pause();
        }

    }
}
