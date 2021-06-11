using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameManager gameManager;
    GameObject currentScene;

    public void setScene(GameObject newScene)
    {
        gameManager.clearLastQuestion();
        currentScene?.SetActive(false);
        currentScene = newScene;
        currentScene.SetActive(true);
    }
}
