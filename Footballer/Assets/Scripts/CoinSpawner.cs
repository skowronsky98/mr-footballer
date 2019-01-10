using UnityEngine;

public class CoinSpawner : MonoBehaviour {

    public Transform[] spawnLocation;
    public GameObject coinToSpawn;

    public static int coins = 0;

    public static float spawnTime = 10f;//---------------------------
    public float time = 0;

    private float xRange;
    private float yRange;

    public static CoinSpawner coinSpawnerInstance;

    private Vector3 coinPosition;

    public bool canCountToSpawn = false;

    private void Awake()
    {
        coinSpawnerInstance = this;
    }


    void Update()
    {
        if (canCountToSpawn)
        {
            SpawnTimeCount();
        }
        else
        {
            time = 0;
        }

     //   Debug.Log("1 " + countToDestroy+"\n2 "+time);  
    }

    void Spawn()
    {
        if (coins == 0)
        {
            xRange = Random.Range(spawnLocation[0].transform.position.x, spawnLocation[1].transform.position.x);
            yRange = Random.Range(spawnLocation[0].transform.position.y, spawnLocation[1].transform.position.y);

            coinPosition = new Vector3(xRange, yRange, 0);

            int spawnIndex = Random.Range(0, spawnLocation.Length);

            Instantiate(coinToSpawn, coinPosition, spawnLocation[spawnIndex].rotation);

            coins++;
        }
       


    }

    void SpawnTimeCount()
    {
        time += Time.deltaTime;

        if (time >= spawnTime)
        {
            canCountToSpawn = false;
            Spawn();
            time = 0;

        }

    }

}
