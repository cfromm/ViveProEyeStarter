using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public Transform cameraTransform;

    //List<GameObject> targetList;


    void Start()
    {

        // -15 to 15 along az and el
        float[] xPos_target = new float[] { .3882F, .3882F, .3882F, 0F,0F,0F,-.3882F,-.3882F,-.3882F};
        float[] yPos_target = new float[] { -0.375F,0F, 0.375F ,-0.3882F,0F, 0.3882F, -0.375F, 0F, 0.375F };
        float[] zPos_target = new float[] { 1.3995F, 1.4489F, 1.3995F , 1.4489F,1.5F,1.4489F,1.3995F, 1.4489F, 1.3995F };

        // Initialize position
       // transform.position = sc.toCartesian + pivot.position;

        for (int i = 0; i < 9; i++)
        {
            GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            newSphere.name = "gazeTarget_"+ i.ToString();

            newSphere.transform.parent = cameraTransform;
            newSphere.transform.localScale = new Vector3(0.05F, 0.05F, 0.05F);

            newSphere.transform.localPosition = new Vector3(xPos_target[i], yPos_target[i],zPos_target[i]);
            newSphere.GetComponent<Renderer>().material.color = Color.blue;//new Color(1,0,1,1);

        }

        //GameObject backing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //backing.transform.parent = cameraTransform;
        //backing.transform.localPosition = new Vector3(0F, 0F, 1.75F);
        //backing.transform.localScale = new Vector3(2f,2f,0.01f);
        //backing.GetComponent<Renderer>().material.color = Color.blue; //new Color(0, 1, 0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
