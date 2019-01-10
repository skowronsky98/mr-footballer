using UnityEngine;

public class BasketHookSpawner : MonoBehaviour
{
    public Transform[] spawnLocation;
    public GameObject spawnObject;
    Vector3 spawnPosition;


    public float spawnTime = 4;
    public float time = 0;
    public bool canSpawn = true;
    public float delayDestroy = 0;
    bool firstSpawn = true;
    

    float xRange, yRange;

    public static BasketHookSpawner Instance;
    public bool exists = false;
    public bool coinMove = false;

    private void Awake()
    {
        Instance = this;
        delayDestroy = spawnTime / 2;
    }
   

    private void Update()
    {
        if (GameObject.Find("CoinBasket(Clone)") == null)
            coinMove = false;
         else
            coinMove = true;
        
        if (canSpawn && Ball.ballInstance.isActiveAndEnabled)
        {

            SpawnTimeCount();
            //Spawn();
        }
        else if(exists && !Ball.ballInstance.isActiveAndEnabled && !coinMove)
        {
            BasketHook.Instance.DestroyBasket();
        }
       
    }

    public void Spawn()
    {
        xRange = Random.Range(spawnLocation[0].transform.position.x, spawnLocation[1].transform.position.x);
        yRange = Random.Range(spawnLocation[0].transform.position.y, spawnLocation[1].transform.position.y);

        spawnPosition = new Vector3(xRange, yRange, 0);

        int spawnIndex = Random.Range(0, spawnLocation.Length);

        Instantiate(spawnObject, spawnPosition, spawnLocation[spawnIndex].rotation);

        canSpawn = false;
        exists = true;

    }


    public void SpawnTimeCount()
    {
        time += Time.deltaTime;

        if (time >= delayDestroy && !firstSpawn && exists)
        {
            BasketHook.Instance.DestroyBasket();
            firstSpawn = true;
        }

        if (time >= spawnTime && !exists)
        {
            Spawn();
            time = 0;
            firstSpawn = false;

        }

    }
}
