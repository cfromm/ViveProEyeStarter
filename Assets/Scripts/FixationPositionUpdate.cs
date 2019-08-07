using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR;

public class FixationPositionUpdate : MonoBehaviour {

    //SMI.SMIEyeTrackingUnity smiInstance = null;
    public GameObject eyetracker;
    public GameObject head_camera;
    public ViveSR.anipal.Eye.SR_GazeGetter eyetrackerData;

    Quaternion offsets;
    public Vector3 gazeVector;

    public float fixationRadiusDeg;
    public float fixationDepthM;
    public Renderer rend;



    // Use this for initialization
    void Start()
    {
        head_camera = GameObject.FindGameObjectWithTag("MainCamera");
        transform.SetParent(head_camera.transform);
        UpdateWithGazePosition();
        rend = GetComponent<Renderer>();

        
    }


    /// <summary>
    /// Get the SMI gaze position and add the desired offset
    /// To be called in update if gaze-contingent stimulus is desired
    /// </summary>
    void UpdateWithGazePosition()
    { 
        transform.localPosition =  Vector3.forward *fixationDepthM;
        transform.LookAt(Camera.main.transform.position);
        transform.localRotation*=Quaternion.Euler(90,0,0);
        transform.localScale = new Vector3(2 * Mathf.Tan((fixationRadiusDeg * Mathf.PI) / 180) * fixationDepthM, 0, 2 * Mathf.Tan(fixationRadiusDeg * Mathf.PI / 180) * fixationDepthM);
        


    }


    void Update()
    {
       // angularError = smiInstance.smi_GetCameraRaycast() * Stimulus.StimDepth - transform.position;
        //set up size conversions to degrees here
  
      UpdateWithGazePosition();
            
    

    }
}

