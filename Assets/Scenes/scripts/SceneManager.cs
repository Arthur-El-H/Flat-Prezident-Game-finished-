using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] LastQuestionView lastQuestionViewManager;
    GameObject currentScene;

    public void setScene(GameObject newScene)
    {
        lastQuestionViewManager.clearLastQuestion();
        currentScene?.SetActive(false);
        currentScene = newScene;
        currentScene.SetActive(true);
    }
}
