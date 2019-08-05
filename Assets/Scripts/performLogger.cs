using UnityEngine;
using System;
using System.IO;
using System.Text;


/// <summary>
/// This script records data each frame in a text file in the following tab-delimited format
/// Frame   Start		HeadX	HeadY	HeadZ	HandX   HandY   HandZ				
///------------------------------------------------------------------------------
/// 1       1241.806	float	float	float   float	float	float	
/// 2       4619.335	float	float	float   float	float	float	
/// </remark>
/// 

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class performLogger : MonoBehaviour
            {

                public string FolderName = "D:\\Data\\ViveProEye";
                private string OutputDir;

                private int frameNumber, timeStamp;
                private static EyeData eyeData;
                private static VerboseData verboseData;
                private float pupilDiameterLeft, pupilDiameterRight;
                private Vector2 pupilPositionLeft, pupilPositionRight;
                private float eyeOpenLeft, eyeOpenRight;
                private Vector3 combinedEIHDir, combinedGazeOriginMM, combinedGIWDir;


                //Things you want to write out, set them in the inspector
                public Transform cameraTransform;

                //Gives user control over when to start and stop recording, trigger this with spacebar;
                public bool startWriting;

                //Initialize some containers
                FileStream streams;
                FileStream trialStreams;
                StringBuilder stringBuilder = new StringBuilder();
                String writeString;
                Byte[] writebytes;


                Vector3 etEIH2_xyz;
                Vector3 etEIH0_xyz;
                Vector3 etEIH1_xyz;

                Vector3 etEyeCenter0_xyz;
                Vector3 etEyeCenter1_xyz;

                float etConfidence;
                float etTimeStamp;
                float uTimeStamp;


                void Start()
                {
                    // create a folder 
                    string OutputDir = Path.Combine(FolderName, string.Concat(DateTime.Now.ToString("MM-dd-yyyy-HH-mm")));
                    Directory.CreateDirectory(OutputDir);

                    // create a file to record data
                    String trialOutput = Path.Combine(OutputDir, DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".txt");
                    trialStreams = new FileStream(trialOutput, FileMode.Create, FileAccess.Write);

                    //Call the function below to write the column names
                    WriteHeader();

                }


                void WriteHeader()
                {

                    //output file-- order of column names here should match the order you use when writing out each value 
                    stringBuilder.Length = 0;

                    //add column names
                    stringBuilder.Append(
                        "FrameNumber\t"
                        + "uTimestamp\t"
                        + Environment.NewLine
                                    );


                    writeString = stringBuilder.ToString();
                    writebytes = Encoding.ASCII.GetBytes(writeString);
                    trialStreams.Write(writebytes, 0, writebytes.Length);

                }

                void Update()
                {
                    //Use spacebar to initiate/stop recording values, you can change this if you want 
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        startWriting = !startWriting;
                        if (startWriting)
                        {
                            Debug.Log("Start writing");
                        }
                        else
                        {
                            Debug.Log("Stop writing");
                        }
                    }

                    if (startWriting)
                    {

                        if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING)
                        {
                            VerboseData data;
                            if (SRanipal_Eye.GetVerboseData(out data) &&
                                data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY) &&
                                data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY)
                                )
                            {

                                SRanipal_Eye.GetEyeData(ref eyeData);
                                SRanipal_Eye.getVerboseData(ref verboseData);

                                combinedEIHDir = verboseData.combined.eye_data.gaze_direction_normalized;
                                combinedGIWDir = Camera.main.transform.TransformDirection(combinedEIHDir);
                                combinedGazeOriginMM = verboseData.combined.eye_data.gaze_origin_mm;

                                frameNumber = eyeData.frame_sequence;
                                timeStamp = eyeData.timestamp;

                                // pupil diameter    
                                pupilDiameterLeft = verboseData.left.pupil_diameter_mm;
                                pupilDiameterRight = verboseData.right.pupil_diameter_mm;
                                // pupil positions    
                                pupilPositionLeft = verboseData.left.pupil_position_in_sensor_area;
                                pupilPositionRight = verboseData.right.pupil_position_in_sensor_area;
                                // eye open    
                                eyeOpenLeft = verboseData.left.eye_openness;
                                eyeOpenRight = verboseData.right.eye_openness;
                            }


                        }


                    }
                }
            
            
                void WriteFile()
                {


                    //Vector4 camRow0 = cameraTransform.localToWorldMatrix.GetRow(0);
                    //Vector4 camRow1 = cameraTransform.localToWorldMatrix.GetRow(1);
                    //Vector4 camRow2 = cameraTransform.localToWorldMatrix.GetRow(2);
                    //Vector4 camRow3 = cameraTransform.localToWorldMatrix.GetRow(3);

                    


                    stringBuilder.Length = 0;
                    stringBuilder.Append(

                                Time.frameCount + "\t"
                                + uTimeStamp + "\t"

                                + Environment.NewLine
                            );

                    writeString = stringBuilder.ToString();
                    writebytes = Encoding.ASCII.GetBytes(writeString);
                    trialStreams.Write(writebytes, 0, writebytes.Length);
                }

             

                void OnApplicationQuit()
                {
                    trialStreams.Close();

                }

            }
        }
    }
}