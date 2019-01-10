using UnityEngine;

public class BasketHook : MonoBehaviour
{
    Transform basketTransform;
    public static BasketHook Instance;

    int coinReward = 3;
    public GameObject coinPrefab;
    public Coin coinObj;
    public GameObject coinIcon;
    GameObject goCoin;
    public Coin coin;
    bool canCollect = true; //can collect only once from one basket
    float time = 0.3f;

    private void Start()
    {
        basketTransform = GetComponent<Transform>();
        coinIcon = GameObject.FindGameObjectWithTag("Coin");
        canCollect = true;
    }
   
    private void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && Ball.ballInstance.transform.position.y > basketTransform.position.y && canCollect)
        {
            canCollect = false;
            BasketHookSpawner.Instance.canSpawn = true;
            SpawnBasketCoins();
            Invoke("SpawnBasketCoins", time);
            Invoke("SpawnBasketCoins", time*2);
            for (int i = 0; i < coinReward; i++)
            {
                ScoreManagerText.Instance.UpdateCoinScore();
            }
            
        }
    }

    public void DestroyBasket()
    {
        Destroy(gameObject);
        BasketHookSpawner.Instance.canSpawn = true;
        BasketHookSpawner.Instance.exists = false;
        BasketHookSpawner.Instance.time = 0;
        BasketHookSpawner.Instance.coinMove = false;
    }

    public void SpawnBasketCoins()
    {

        goCoin = Instantiate(coinPrefab, transform);
        goCoin.GetComponent<BoxCollider2D>().enabled = false;
        coin = goCoin.GetComponent<Coin>();
        coin.coinIcon = coinIcon;
        coin.canMove = true;
    }
    
}
