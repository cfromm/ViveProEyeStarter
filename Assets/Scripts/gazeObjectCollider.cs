//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using UnityEngine;
using UnityEngine.Assertions;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class gazeObjectCollider : MonoBehaviour
            {
                public float objDefaultDist = 5.0f;
                public float objSizeDegs = 1.0f;
                public float sphereCastRadius = 0.05f;

                private void Start()
                {
                    if (!SRanipal_Eye_Framework.Instance.EnableEye)
                    {
                        enabled = false;
                        return;
                    }
                }

                private void Update()
                {

                    if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                        SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

                    Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal;

                    if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                    else if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                    else if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                    else return;

                    Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocal);

                    //float diameterM = 0.05f; //Mathf.Tan(Mathf.Deg2Rad * (objSizeDegs/2.0f)) * hit.distance * 2.0f;
                    //transform.localScale = new Vector3(diameterM, diameterM, diameterM);

                    if (Physics.SphereCast(Camera.main.transform.position, sphereCastRadius, GazeDirectionCombined, out RaycastHit hit, Mathf.Infinity))
                    {
                        //transform.position = hit.point;
                        transform.position = Camera.main.transform.position + GazeDirectionCombined * hit.distance;

                        float diameterM = Mathf.Tan(Mathf.Deg2Rad * (objSizeDegs/2.0f)) * hit.distance * 2.0f;
                        transform.localScale = new Vector3(diameterM, diameterM, diameterM);
                    }
                    else
                    {
                        transform.position = Camera.main.transform.position + GazeDirectionCombined * objDefaultDist;
                        float diameterM = Mathf.Tan(Mathf.Deg2Rad * (objSizeDegs/2.0f)) * objDefaultDist * 2.0f;
                        transform.localScale = new Vector3(diameterM, diameterM, diameterM);
                    }
                    

                    }
            }
        }
    }
}
