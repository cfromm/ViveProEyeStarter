using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class SR_GazeGetter : MonoBehaviour
            {
                private static EyeData eyeData;
                private static VerboseData verboseData;
                public float pupilDiameterLeft, pupilDiameterRight;
                public bool DummyEyeData;

                public Vector2 pupilPositionLeft, pupilPositionRight;
                public float eyeOpenLeft, eyeOpenRight;
                public Vector3 binocularEIHorigin, binocularEIHdirection;

                void Update()
                {
                    if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING && !DummyEyeData)
                    {
                        VerboseData data;
                        if (SRanipal_Eye.GetVerboseData(out data) &&
                            data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY) &&
                            data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY)
                            )
                        {
                            SRanipal_Eye.GetEyeData(ref eyeData);
                            SRanipal_Eye.GetVerboseData(out verboseData);
                            SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out binocularEIHorigin, out binocularEIHdirection);
                            // pupil diameter    
                            pupilDiameterLeft = eyeData.verbose_data.left.pupil_diameter_mm;
                            pupilDiameterRight = eyeData.verbose_data.right.pupil_diameter_mm;
                            // pupil positions    
                            pupilPositionLeft = eyeData.verbose_data.left.pupil_position_in_sensor_area;
                            pupilPositionRight = eyeData.verbose_data.right.pupil_position_in_sensor_area;
                            // eye open    
                            eyeOpenLeft = eyeData.verbose_data.left.eye_openness;
                            eyeOpenRight = eyeData.verbose_data.right.eye_openness;
                        }
                    }
                    if (DummyEyeData)
                    {
                        binocularEIHdirection = new Vector3(0, 0, 1);
                        binocularEIHorigin = new Vector3(0.005f, 0.001f, -0.03f);
                        pupilDiameterLeft = 4f;
                        pupilDiameterRight = 4f;
                        eyeOpenLeft = 1;
                        eyeOpenRight = 1;
                        pupilPositionLeft = new Vector2(0.45f, 0.6f);
                        pupilPositionRight = new Vector2(0.45f, 0.6f);

                    }

                }

            }
        }
    }
}



