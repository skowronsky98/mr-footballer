using UnityEngine;

public class Sun : MonoBehaviour {

    float speed = 10f;
	
	void Update () {

        transform.Rotate(0, 0, speed * Time.deltaTime);

	}
}
