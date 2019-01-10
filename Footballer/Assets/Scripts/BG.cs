using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour {

    float playerPosition;
   
    public float slowScale = 7;
   
    void Update () {

        transform.position = new Vector3(Player.Instace.transform.position.x/slowScale*-1, transform.position.y,transform.position.z);
	}
}
