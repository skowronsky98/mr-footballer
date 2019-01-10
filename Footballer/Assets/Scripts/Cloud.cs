using UnityEngine;

public class Cloud : MonoBehaviour {

    public Transform orginCloud;
    public Transform destiantionCloud;

    Vector3 orginPosition;
    Vector3 destinationPosition;

    float speed;

    void Start () {

        orginPosition = new Vector3(orginCloud.position.x, transform.position.y, 0);
        destinationPosition = new Vector3(destiantionCloud.position.x,transform.position.y,0);

        speed = Random.Range(0.005f, 0.01f);

		
	}
	
	void Update () {

        if (Time.timeScale == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, speed);

        }

        if (transform.position.x >= destiantionCloud.position.x)
        {
            transform.position = orginPosition;
            speed = Random.Range(0.005f, 0.01f);
        }

      
    }
}
