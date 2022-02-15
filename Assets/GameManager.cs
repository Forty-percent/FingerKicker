using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private bool end = false;
    private GameObject gameOverScreen;
    public PostProcessVolume ppVolume;
    private DepthOfField dof;

    private GameObject score;
    private Text scoreText;

    public GameObject directionalLightObject;
    private Light directionalLight;

    void Start()
    {
        directionalLight = directionalLightObject.GetComponent<Light>();
        directionalLight.intensity = 0.18f;

        gameOverScreen = GameObject.Find("GameOver screen");
        gameOverScreen.SetActive(false);

        ppVolume.profile.TryGetSettings(out dof);
        Debug.Log("START");
        dof.enabled.value = false;

        score = GameObject.Find("ScoreText");
        scoreText = score.GetComponent<Text>();
        scoreText.text = "0";
    }

    public void EndGame()
    {
        if (!end)
        {
            GlobalVariableStorrage.GameOver = true;
            dof.enabled.value = true;
            gameOverScreen.SetActive(true);

            end = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            directionalLight.intensity -= 0.1f;
        }
        else if (Input.GetKeyDown("p"))
        {
            directionalLight.intensity += 0.1f;
        }
    }
}
