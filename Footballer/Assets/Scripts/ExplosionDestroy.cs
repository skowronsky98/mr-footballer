using UnityEngine;

public class ExplosionDestroy : MonoBehaviour {

    void Update()
    {
        Destroy(gameObject, 0.3f);
    }
}
