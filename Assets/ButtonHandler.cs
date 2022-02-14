using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        EditorSceneManager.LoadScene(1);
    }
}
