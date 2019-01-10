using UnityEngine;

public class Coin : MonoBehaviour
{

    float destroyTime = 5f;
    float timeToDestroy = 0;

    public bool canMove = false;

    public GameObject coinIcon;

    public static Coin Instance;
    public bool canCollect = true;

    private AudioSource audioCount;
    bool audio = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coinIcon = GameObject.FindGameObjectWithTag("Coin");

        canCollect = true;

        audioCount = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (LevelSelection.index != 4)
        {
            CoutingTimeToDestroy();

            CoinMove();

        }
        else if(LevelSelection.index == 4)
        {
            CoinMoveBasketLVL();
        }


    }

    public void CoutingTimeToDestroy()
    {
        if (CoinSpawner.coins != 0 && LevelSelection.index != 4)
        {

            CoinSpawner.coinSpawnerInstance.canCountToSpawn = false;

            timeToDestroy += Time.deltaTime;

            if (timeToDestroy >= destroyTime && canMove == false)
            {
                CoinSpawner.spawnTime = 10f;
                CoinSpawner.coinSpawnerInstance.canCountToSpawn = true;

                CoinSpawner.coins--;

                timeToDestroy = 0;

                Destroy(gameObject);

                audio = true;

            }
        }
    }

    public void CollectDestroy(GameObject gameObject)
    {
        canMove = true;
        CoinSpawner.coinSpawnerInstance.canCountToSpawn = false;

        if (!Mute.muteSounds && audio)
        {
            audioCount.Play();
            audio = false;
        }
    }

    public void CoinMove()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, coinIcon.transform.position, 1f);

            if (transform.position == coinIcon.transform.position)
            {
                canMove = false;

                CoinSpawner.coins--;
                CoinSpawner.spawnTime = 5f;
                CoinSpawner.coinSpawnerInstance.canCountToSpawn = true;

                Destroy(gameObject);
                canCollect = true;
            }
        }
    }

    public void CoinMoveBasketLVL()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, coinIcon.transform.position, 1f);

            if (transform.position == coinIcon.transform.position)
            {
                canMove = false;
                Destroy(gameObject);
                
            }
        }
    }
}
