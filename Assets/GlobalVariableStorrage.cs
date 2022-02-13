using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableStorrage : MonoBehaviour
{
    public static float DeltaFlick;

    void Start()
    {
        DeltaFlick = 0;
    }
}
