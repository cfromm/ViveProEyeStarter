using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class EyeballUpdate : MonoBehaviour
            {
                private static EyeData eyeData;
                private static Vector3 GazeOrigin;
                private static Vector3 GazeDirection;


                void Start()
                {   //Parent eye objects to main camera so they are tracked with the head
                    transform.SetParent(Camera.main.transform);
                    //Check to make sure eyetracking is enabled
                    if (!SRanipal_Eye_Framework.Instance.EnableEye)
                    {
                        enabled = false;
                        Debug.Log("please check the 'Enable Eye' box in the inspector view of SRanipal_Eye_Framework object");
                        return;
                    }
                    
                    //Set initial positions for left and right eyes
                    if (tag == "Left")
                    {
                        //Use GazeIndex.LEFT for the left eye
                        SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOrigin, out GazeDirection);
                        //GazeOrigin and GazeDirection are with respect to the origin of the main camera, which is our parent
                        //Set the local position of this eyeball to GazeOrigin
                        transform.localPosition =  GazeOrigin ;
                        
                    }
                    if (tag == "Right")
                    {
                        //Use GazeIndex.RIGHT for the left eye
                        SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOrigin, out GazeDirection);
                        //GazeOrigin and GazeDirection are with respect to the origin of the main camera, which is our parent
                        //Set the local position of this eyeball to GazeOrigin
                        transform.localPosition = GazeOrigin;
                    }
                    
                }

                //Update the position of the eyeball each frame, and the direction of the gaze vector shown in the Scene View
                void Update()
                {
                    if (tag == "Left")
                    {
                        SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOrigin, out GazeDirection);
                        transform.localPosition = GazeOrigin;
                        //Draw a line from the left eye position (in world coordinates obtained by using the TransformPoint function), 
                        //in the direction of gaze (also in world coordinates obtained by the TransformDirection function)
                        //with length 10m and with the color blue
                        Debug.DrawRay(Camera.main.transform.TransformPoint(GazeOrigin), Camera.main.transform.TransformDirection(GazeDirection) * 10, Color.yellow);

                    }
                    if (tag == "Right")
                    {
                        SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOrigin, out GazeDirection);
                        transform.localPosition = GazeOrigin;
                        //Draw a line from the right eye position (in world coordinates obtained by using the TransformPoint function), 
                        //in the direction of gaze (also in world coordinates obtained by the TransformDirection function)
                        //with length 10m and with the color red
                        Debug.DrawRay(Camera.main.transform.TransformPoint(GazeOrigin),  Camera.main.transform.TransformDirection(GazeDirection)*10, Color.blue);
                    }
                    
                }
            }
        }
    }
}
