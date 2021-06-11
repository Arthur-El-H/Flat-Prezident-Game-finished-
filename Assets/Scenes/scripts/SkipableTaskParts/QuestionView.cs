using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionView : MonoBehaviour, ISkipablePartOfTask_VIEW
{
    public QuestionView(int secondsTowait, int secondsToShow,
                  GameObject questionBackground, GameObject question, 
                  GameManager gameManager, SceneManager sceneManager, SceneDatabase sceneData,
                  bool firstScene)
    {
        this.secondsTowait = secondsTowait;
        this.secondsToShow = secondsToShow;

        this.question = question;
        this.questionBackground = questionBackground;

        this.gameManager = gameManager;
        this.sceneManager = sceneManager;
        this.sceneData = sceneData;
        this.firstScene = firstScene;
    }

    int secondsTowait;
    int secondsToShow;
    GameManager gameManager;
    SceneManager sceneManager;
    SceneDatabase sceneData;

    public GameObject questionBackground;
    public GameObject question;

    bool firstScene;

    public void skipToGoal()
    {
        if (!firstScene)
        {
            gameManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            gameManager.showPossibleAnswers();
        }
        close();
    }
    public void Start()
    {
        Debug.Log("Created and concious!");
        StartCoroutine(DoTask());
    }

    IEnumerator DoTask()
    {
        Debug.Log("Station 1");
        yield return new WaitForSeconds(secondsTowait);
        sceneManager.setScene(sceneData.Totale);
        if (firstScene)
        {
            question.GetComponent<UnityEngine.UI.Text>().text = "So you are saying that the earth is flat?";
            questionBackground.SetActive(true);
            question.SetActive(true);
            yield return new WaitForSeconds(secondsToShow);
            question.SetActive(false);
            questionBackground.SetActive(false);
        }
        else
        {
            question.GetComponent<UnityEngine.UI.Text>().text = gameManager.currentQuest.getQuestion();
            questionBackground.SetActive(true);
            question.SetActive(true);
            yield return new WaitForSeconds(secondsToShow);
            gameManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            gameManager.showPossibleAnswers();
        }
        Debug.Log("Station 9");
    }


    void close()
    {
        Destroy(this);
    }
}
