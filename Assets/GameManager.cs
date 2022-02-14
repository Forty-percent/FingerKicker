using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private bool end = false;


    public void EndGame()
    {
        if (!end)
        {
            //Debug.Log("end");
            
            GlobalVariableStorrage.GameOver = true;

            end = true;
        }
    }
}
