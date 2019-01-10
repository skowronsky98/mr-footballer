using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

    private AudioSource audioCount;

    Rigidbody2D rbBall;

    float time = 0;
    float delay = 0.4f;
    float gameplayTime = 0;

    public float maxGravity = 0.3f,
                    minGravity = 0.15f;

    bool underLine = false;

    public static Ball ballInstance;
    public GameObject explosion;

    int skinID = 0;

    [SerializeField]
    public Sprite[] balls;
    SpriteRenderer ballSkin;

    
    public Transform linePosition;
    public GameObject scoreTxt;

    //public GameObject loadedTXR;


    private void Awake()
    {
        ballInstance = this;
    }

    void Start () {

        rbBall = GetComponent<Rigidbody2D>();
        ballSkin = GetComponent<SpriteRenderer>();
        audioCount = GetComponent<AudioSource>();

        skinID = SecurePlayerPrefs.GetInt("SelectedBall", skinID);

        ballSkin.sprite = balls[skinID];         // Skin ID

        gameObject.SetActive(false);

        rbBall.gravityScale = minGravity;
        gameplayTime = 0;

        underLine = false;

    }
	
	void Update ()
    {
        //if (AdManager.AdManagerInstance.RewardBaseVideoAd.IsLoaded())
        //{
        //    loadedTXR.SetActive(true);
        //}
        //else
        //{
        //    loadedTXR.SetActive(false);
        //}

        time += Time.deltaTime;

        GravitySpeed();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Grass")
        {

            ScoresManager.Instance.ProcessScores(false);

            //vibrate
            Vibration.Vibrate(40);
           
            Instantiate(explosion, rbBall.position, transform.rotation =  Quaternion.identity);

            Fallout.falloutInstance.FalloutCanvas();

            UIManager.uiManagerInstance.textPause.enabled = false;

            transform.position = new Vector3(0, 6.35f, 90.39f);

            gameObject.SetActive(false);

            if (LevelSelection.index != 4)
            {
                if (CoinSpawner.coins != 0)
                {
                    Destroy(Coin.Instance.gameObject);
                    CoinSpawner.coins--;
                }

                CoinSpawner.spawnTime = 10f;

                CoinSpawner.coinSpawnerInstance.canCountToSpawn = false;
            }

           

            rbBall.gravityScale = minGravity;
            gameplayTime = 0;
            Player.Instace.ResetSpeed();

            underLine = false;

        }
        else if (coll.gameObject.tag == "Player")
        {
            if (time >= delay)
            {
                underLine = true;

                if (LevelSelection.index == 2 || LevelSelection.index == 3)                                 //New LEVEL!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    ScoreManagerText.Instance.UpdateScore();// Ball !!!!!!


                    if (!Mute.muteSounds)
                    {
                        audioCount.Play();

                    }
                }


                time = 0;
            }
           
        }
       

    }

    private void OnTriggerEnter2D(Collider2D coinCol)
    {
        if (coinCol.CompareTag("Coin"))
        {
            if (Coin.Instance.canCollect)
            {
               
                ScoreManagerText.Instance.UpdateCoinScore();
                Coin.Instance.canCollect = false;
            }

            Coin.Instance.CollectDestroy(coinCol.gameObject);
            //ScoresManager.Instance.UpdateCoinScore();
        }

       else if (coinCol.CompareTag("Line"))
        {
            if ( LevelSelection.index != 2 || LevelSelection.index != 3)
            {
                if (underLine)
                {
                    ScoreManagerText.Instance.UpdateScore();
                    scoreTxt.SetActive(true);
                    Invoke("LineScore", 0.5f);

                    if (!Mute.muteSounds)
                    {
                        audioCount.Play();

                    }

                    underLine = false;
                }
               
            }
           
        }

    }

    private void GravitySpeed()
    {
        gameplayTime += Time.deltaTime;

        if (gameplayTime >= 45 && rbBall.gravityScale < maxGravity)
        {
            rbBall.gravityScale += 0.02f;
            Player.Instace.Speed();
            gameplayTime = 0;
        }

    }

    void LineScore()
    {
        scoreTxt.SetActive(false);
    }




}

