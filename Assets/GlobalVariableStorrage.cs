using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableStorrage : MonoBehaviour
{
    public static float DeltaFlick;
    public static float Health;
    public static int Score;
    public static bool GameOver;

    void Start()
    {
        DeltaFlick = 0;
    }
}
