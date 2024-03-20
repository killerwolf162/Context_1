using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWords : MonoBehaviour
{

    private void Update()
    {
        if(this.transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
