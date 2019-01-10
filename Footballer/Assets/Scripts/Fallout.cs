using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fallout : MonoBehaviour {

    static float time = 0;
    float timeToDisplayAd = 0; //20f !!!!!!!!!!!!!!!!!!

    public Canvas falloutCanvas;
    public Canvas playerCanvasController;

    public static Fallout falloutInstance;

   

    private void Awake()
    {
        falloutInstance = this;
    }

    void Start () {

        

        timeToDisplayAd = Random.Range(40f, 60f);

        Transform ownedScoreChild = transform.Find("OwnedScoreText");
        Text ownedScoreText = ownedScoreChild.GetComponent<Text>();

        Transform pauseChild = transform.Find("PausedText");
        Text pausedText = pauseChild.GetComponent<Text>();

        falloutCanvas = GetComponent<Canvas>();

        falloutCanvas.enabled = false;
        playerCanvasController.enabled = true;
        
	}


    void Update()
    {
        time += Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.Escape)) && (falloutCanvas.enabled) && (Ball.ballInstance.isActiveAndEnabled))
        {
            if (UIManager.uiManagerInstance.paused)
            {
                UIManager.uiManagerInstance.Pause();
            }
            falloutCanvas.enabled = false;
            playerCanvasController.enabled = true;

            //UIManager.uiManagerInstance.Pause();
        }
        else if((Input.GetKeyDown(KeyCode.Escape)) && (((falloutCanvas.enabled) && (!Ball.ballInstance.isActiveAndEnabled)) || ((!falloutCanvas.enabled) && (TapToPlay.tapToPlayInstance.isActive))))
        {
            SceneManager.LoadScene(0);
        }
        else if ((Input.GetKeyDown(KeyCode.Escape)) && (!falloutCanvas.enabled) && (!TapToPlay.tapToPlayInstance.isActive))
        {
            if (!UIManager.uiManagerInstance.paused)
            {
                UIManager.uiManagerInstance.Pause();
            }
            falloutCanvas.enabled = true;
            playerCanvasController.enabled = false;


        }
    }
         


    public void FalloutCanvas()
    {
        falloutCanvas.enabled = true;
        playerCanvasController.enabled = false;


        if (time >= timeToDisplayAd)
        {
            AdManager.AdManagerInstance.ShowRewardBasedAd();
            time = 0;

        }

    }
    
}
