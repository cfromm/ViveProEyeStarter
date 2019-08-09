using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationAssesment : MonoBehaviour
{
    public int target_number = 0;
    public float targetRadius_deg;
    public float targetSpacing_deg;
    public float targetDepth_meters;
    private float corner_angle;

    public Vector3 displacementVector;
    public Vector3 gazeVector;
    public float angularError;
    public GameObject current_target;


    public GameObject eyetracker;
    public GameObject head_camera;
    public ViveSR.anipal.Eye.SR_GazeGetter eyetrackerData;
    // Start is called before the first frame update
    void Start()
    {
        head_camera = GameObject.FindGameObjectWithTag("MainCamera");
        eyetracker = GameObject.FindGameObjectWithTag("Eyetracker");
        eyetrackerData = eyetracker.GetComponent<ViveSR.anipal.Eye.SR_GazeGetter>();
        corner_angle = Mathf.Sqrt(Mathf.Pow(targetSpacing_deg,2)* 2);
        float[] xoffset_target = new float[] { 0, -targetSpacing_deg, targetSpacing_deg, 0, 0, -targetSpacing_deg, targetSpacing_deg, -targetSpacing_deg, targetSpacing_deg };
        float[] yoffset_target = new float[] { 0, 0, 0, -targetSpacing_deg, targetSpacing_deg, targetSpacing_deg, targetSpacing_deg, -targetSpacing_deg, -targetSpacing_deg };
        

        for (int i = 0; i < 9; i++)
        {
            GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            newSphere.name = "gazeTarget_" + i.ToString();

            newSphere.transform.parent = head_camera.transform;
            newSphere.transform.localScale = new Vector3(2 * Mathf.Tan((targetRadius_deg * Mathf.PI) / 180) * targetDepth_meters, 2 * Mathf.Tan((targetRadius_deg * Mathf.PI) / 180) * targetDepth_meters, 2 * Mathf.Tan(targetRadius_deg * Mathf.PI / 180) * targetDepth_meters); ;

            newSphere.transform.localPosition = Quaternion.Euler(xoffset_target[i], yoffset_target[i], 0)*Vector3.forward * targetDepth_meters;
            newSphere.GetComponent<Renderer>().material.color = Color.blue;//new Color(1,0,1,1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        current_target = GameObject.Find("gazeTarget_" + target_number.ToString());
        current_target.GetComponent<Renderer>().material.color = Color.red;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Step to next position in array
            current_target.GetComponent<Renderer>().material.color = Color.blue;
            target_number++;

            // If Array count excedes Array index then go back to first position in array
            if (target_number >= 8)
            {
                target_number = 0;
            }
            current_target = GameObject.Find("gazeTarget_" + target_number.ToString());
            current_target.GetComponent<Renderer>().material.color = Color.red;
        }
        gazeVector = eyetrackerData.binocularEIHorigin + (eyetrackerData.binocularEIHdirection * targetDepth_meters);
        Debug.DrawRay(head_camera.transform.TransformPoint(eyetrackerData.binocularEIHorigin), head_camera.transform.TransformDirection(eyetrackerData.binocularEIHdirection), Color.green);
        Debug.DrawRay(head_camera.transform.position, head_camera.transform.TransformDirection(current_target.transform.localPosition), Color.magenta);
        displacementVector = gazeVector - current_target.transform.localPosition;
        angularError = 2 * Mathf.Sin((displacementVector.magnitude / 2) / targetDepth_meters) * 180 / Mathf.PI;
        if (Input.GetKeyDown(KeyCode.R)) 
        { Debug.Log("Angular error for target " + target_number.ToString() + " is " + angularError.ToString() + " degrees"); }

    }

}
