using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeRelToHead : MonoBehaviour
{
    public float xOffset = -5;
    
    // Start is called before the first frame update
    void Start()
    {
        // x = -5
        // y = 1.1
        
        transform.position = new Vector3(Camera.main.transform.position.x + xOffset, Camera.main.transform.position.y, Camera.main.transform.position.z);


    }

   
}
