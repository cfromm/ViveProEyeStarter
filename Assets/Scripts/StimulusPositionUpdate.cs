using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StimulusPositionUpdate : MonoBehaviour {

    public GameObject eyetracker;
    public GameObject head_camera;
    public ViveSR.anipal.Eye.SR_GazeGetter eyetrackerData;

    Vector3 cameraRaycast;
    Vector3 basePoint;
    public float X_offset_deg;
    public float Y_offset_deg;
    Quaternion offsets;
    public float stimulusRadiusDeg;
    public float stimulusDepthM;


	
    // Use this for initialization
    void Start() {

		offsets = Quaternion.Euler(X_offset_deg, Y_offset_deg, 0);
        head_camera = GameObject.FindGameObjectWithTag("MainCamera");
        eyetracker = GameObject.FindGameObjectWithTag("Eyetracker");
        eyetrackerData = eyetracker.GetComponent<ViveSR.anipal.Eye.SR_GazeGetter>();
        transform.SetParent(head_camera.transform);
        UpdateWithGazePosition();

    }


    /// <summary>
    /// Get the  gaze position and add the desired offset
    /// To be called in update if gaze-contingent stimulus is desired
    /// </summary>
    void UpdateWithGazePosition()
    {
        cameraRaycast = offsets * Vector3.forward; //* eyetrackerData.binocularEIHdirection;
        basePoint = Camera.main.transform.position;//eyetrackerData.binocularEIHorigin;
        transform.localPosition =  cameraRaycast * stimulusDepthM;
        //transform.LookAt(Camera.main.transform.TransformPoint(eyetrackerData.binocularEIHorigin));
        transform.LookAt(Camera.main.transform.position);
        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        transform.localScale = new Vector3(2*Mathf.Tan((stimulusRadiusDeg*Mathf.PI)/180) * stimulusDepthM, 0, 2*Mathf.Tan(stimulusRadiusDeg * Mathf.PI / 180) * stimulusDepthM);
        Debug.DrawLine(transform.parent.position, transform.position, Color.magenta);
	}

    void Update () { 
		
        //set up size conversions to degrees here

        UpdateWithGazePosition();



    }
}
