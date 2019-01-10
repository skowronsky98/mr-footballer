using UnityEngine;

public class HeadLight : MonoBehaviour {

    
    Transform lightTransform;

    public static HeadLight Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        lightTransform = GetComponent<Transform>();	
	}


    public void RotationLeft()
    {
        lightTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, 0);
    }
    public void RotationRight()
    {
        lightTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, 0);
    }


}
