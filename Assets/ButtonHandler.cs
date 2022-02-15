using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        //EditorSceneManager.LoadScene(1);
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        GlobalVariableStorrage.GameOver = false;
        //EditorSceneManager.LoadScene(1);
        SceneManager.LoadScene(1);
    }
}
