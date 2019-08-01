using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye { 

        public class SR_GazeGetter : MonoBehaviour
        {
            private static EyeData eyeData;
            private static VerboseData verboseData;
            public float pupilDiameterLeft, pupilDiameterRight;
            public Vector2 pupilPositionLeft, pupilPositionRight;
            public float eyeOpenLeft, eyeOpenRight;

            void Update()
            {
                SRanipal_Eye.GetEyeData(ref eyeData);
                SRanipal_Eye.GetVerboseData(out verboseData);
                // pupil diameter    
                pupilDiameterLeft = eyeData.verbose_data.left.pupil_diameter_mm;
                pupilDiameterRight = eyeData.verbose_data.right.pupil_diameter_mm;
                // pupil positions    
                pupilPositionLeft = eyeData.verbose_data.left.pupil_position_in_sensor_area;
                pupilPositionRight = eyeData.verbose_data.right.pupil_position_in_sensor_area;
                // eye open    
                eyeOpenLeft = eyeData.verbose_data.left.eye_openness;
                eyeOpenRight = eyeData.verbose_data.right.eye_openness; }

        }
        }
    }
}


