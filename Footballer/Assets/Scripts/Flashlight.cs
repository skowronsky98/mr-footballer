using UnityEngine;

public class Flashlight : MonoBehaviour {

    public static Flashlight Instance;
    Transform flashlightTransform;

    float leftRotationZ = -25 , rightRotationZ = 25;
    bool turn = false;

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        flashlightTransform = GetComponent<Transform>();
    }
   

    public void RotationLeft()
    {

        rightRotationZ = 25;

        if (Time.timeScale == 1)
        {
            if (leftRotationZ <= 25)
            {
                flashlightTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, leftRotationZ += 2);
            }
            else
            {
                flashlightTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, 26);
            }
            turn = true;
        }
       

    }
    public void RotationRight()
    {
        leftRotationZ = -25;

        if (Time.timeScale == 1)
        {
            if (rightRotationZ >= -25 && turn)
            {
                flashlightTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, rightRotationZ -= 2);
            }
            else
            {
                flashlightTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, -26);
            }
            turn = true;
        }

       

    }

}
