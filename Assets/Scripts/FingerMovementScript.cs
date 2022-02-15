using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class FingerMovementScript : MonoBehaviour
{
    SerialPort serialPort;

    #region Props
    public bool OpenSerial { get; set; } = true;
    public Transform Global { get; set; }
    public Transform IndexBase { get; set; }
    public Transform IndexMid { get; set; }
    public Transform IndexTop { get; set; }
    public Transform MiddleBase { get; set; }
    public Transform MiddleMid { get; set; }
    public Transform MiddleTop { get; set; }
    public Transform RingBase { get; set; }
    public Transform RingMid { get; set; }
    public Transform RingTop { get; set; }
    public Transform PinkBase { get; set; }
    public Transform PinkMid { get; set; }
    public Transform PinkTop { get; set; }
    public Transform ThumbBase { get; set; }
    public Transform ThumbMid { get; set; }
    public Transform ThumbTop { get; set; }
    #endregion

    [Range(0.0f, 1.0f)]
    public float flick;


    [Range(0.0f, 1.0f)]
    public float roll;

    [Range(0.0f, 1.0f)]
    public float pitch;

    [Range(0.0f, 1.0f)]
    public float yaw;

    private float lastFlickValue;

    public float maxHealth;

    private Vector3 gloveRotation;
    private GameObject healthBar;
    private HealthbarScript healthbarScript;
    private GameManager gameManager;
    private Thread backgroundThread;


    void Start()
    {
        if (OpenSerial)
        {
            serialPort = new SerialPort(DropDownHandler.portName, 19200);
            serialPort.Open();
            backgroundThread = new Thread(new ThreadStart(ReadSerial));
            backgroundThread.Start();
        }

        maxHealth = 100;

        healthBar = GameObject.Find("Health bar");
        healthbarScript = healthBar.GetComponent<HealthbarScript>();

        GlobalVariableStorrage.Health = maxHealth;
        healthbarScript.SetMaxHealth(maxHealth);

        gameManager = FindObjectOfType<GameManager>();

        string handGlobal = "GLOBAL_cntrl";
        Global = GetComponent<Transform>().Find(handGlobal);

        string path = "GLOBAL_cntrl/Armature/Root_joint/HANDPALM_joint/";

        string indexBasePath = path + "INDEX_BASE_joint/";
        string indexMidPath = indexBasePath + "INDEX_MID_joint/";
        string indexTopPath = indexMidPath + "INDEX_TOP_joint";

        string middleBasePath = path + "MIDDLE_F_BASE_joint/";
        string middleMidPath = middleBasePath + "MIDDLE_F_MID_joint/";
        string middleTopPath = middleMidPath + "MIDDLE_F_TOP_joint";

        string ringBasePath = path + "RING_BASE_joint/";
        string ringMidPath = ringBasePath + "RING_MID_joint/";
        string ringTopPath = ringMidPath + "RING_TOP_joint";

        string pinkBasePath = path + "PINK_BASE_joint/";
        string pinkMidPath = pinkBasePath + "PINK_MID_joint/";
        string pinkTopPath = pinkMidPath + "PINK_TOP_joint";

        string thumbBasePath = path + "THUMB_BASE_joint/";
        string thumbMidPath = thumbBasePath + "THUMB_MID_joint/";
        string thumbTopPath = thumbMidPath + "THUMB_TOP_joint";

        IndexBase = GetComponent<Transform>().Find(indexBasePath);
        IndexMid = GetComponent<Transform>().Find(indexMidPath);
        IndexTop = GetComponent<Transform>().Find(indexTopPath);

        MiddleBase = GetComponent<Transform>().Find(middleBasePath);
        MiddleMid = GetComponent<Transform>().Find(middleMidPath);
        MiddleTop = GetComponent<Transform>().Find(middleTopPath);

        RingBase = GetComponent<Transform>().Find(ringBasePath);
        RingMid = GetComponent<Transform>().Find(ringMidPath);
        RingTop = GetComponent<Transform>().Find(ringTopPath);

        PinkBase = GetComponent<Transform>().Find(pinkBasePath);
        PinkMid = GetComponent<Transform>().Find(pinkMidPath);
        PinkTop = GetComponent<Transform>().Find(pinkTopPath);

        ThumbBase = GetComponent<Transform>().Find(thumbBasePath);
        ThumbMid = GetComponent<Transform>().Find(thumbMidPath);
        ThumbTop = GetComponent<Transform>().Find(thumbTopPath);
    }


    void ReadSerial()
    {
        if (OpenSerial)
        {
            while (true)
            {
                string value = serialPort.ReadLine();
                string[] gloveData = value.Split('/');
                //Debug.Log(gloveData);


                if (gloveData.Length == 4)
                {
                    try
                    {
                        flick = float.Parse(gloveData[3]);
                        gloveRotation.x = -float.Parse(gloveData[0]);
                        gloveRotation.z = -float.Parse(gloveData[1]);
                        gloveRotation.y = -float.Parse(gloveData[2]);
                    }
                    catch(Exception e)
                    {
                        Debug.Log(value);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (Global != null)
        {
            var rotationVector = transform.rotation.eulerAngles;

            if (!OpenSerial)
            {
                rotationVector.z = Mathf.Lerp(-180, 180, roll);
                rotationVector.x = Mathf.Lerp(-180, 180, pitch);
                rotationVector.y = Mathf.Lerp(-180, 180, yaw);
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            else
            {
                transform.rotation = Quaternion.Euler(gloveRotation);
            }

        }

        if (IndexBase != null || IndexMid != null || IndexTop != null ||
            MiddleBase != null || MiddleMid != null || MiddleTop != null ||
            RingBase != null || RingMid != null || RingTop != null ||
            PinkBase != null || PinkMid != null || PinkTop != null ||
            ThumbBase != null || ThumbMid != null || ThumbTop != null)
        {
            IndexBase.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-30, 0, flick), 0, 0));
            IndexMid.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-80, 0, flick), 0, 0));
            IndexTop.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-60, 0, flick), 0, 0));

            ThumbBase.transform.localRotation = Quaternion.Euler(new Vector3(-40, 0, Mathf.Lerp(-50, -30, 1 - flick)));
            ThumbMid.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(0, 20, 1 - flick)));
            ThumbTop.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(-30, 50, 1 - flick)));

            MiddleBase.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-15, 0, flick), 0, Mathf.Lerp(0, 10, flick)));
            //MiddleMid.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-80, 0, flick), 0, 0));
            //MiddleTop.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-60, 0, flick), 0, 0));

            RingBase.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-10, 0, flick), 0, Mathf.Lerp(0, 15, flick)));
            //RingMid.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-80, 0, flick), 0, 0));
            //RingTop.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-60, 0, flick), 0, 0));

            PinkBase.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-5, 0, flick), 0, Mathf.Lerp(0, 20, flick)));
            //PinkMid.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-80, 0, flick), 0, 0));
            //PinkTop.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-60, 0, flick), 0, 0));
        }
        else
        {
            Debug.Log("rip");
        }

        if (GlobalVariableStorrage.Health <= 0)
        {
            backgroundThread.Abort();
            serialPort.Close();

            gameManager.EndGame();
        }

        float deltaFlick = Mathf.Abs(flick - lastFlickValue);

        GlobalVariableStorrage.DeltaFlick = deltaFlick;

        lastFlickValue = flick;
    }
}
