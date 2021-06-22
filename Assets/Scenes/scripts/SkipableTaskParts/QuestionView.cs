using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionView : MonoBehaviour, ISkipablePartOfTask_VIEW
{
    public int secondsTowait;
    public int secondsToShow;
    public GameManager gameManager;
    public SceneManager sceneManager;
    public SceneDatabase sceneData;

    public GameObject questionBackground;
    public GameObject question;
    Coroutine currentQuestionView;

    public bool firstScene;

    public void skipToGoal()
    {
        StopCoroutine(currentQuestionView);
        Debug.Log("Skipped");
        if (!firstScene)
        {
            gameManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            gameManager.showPossibleAnswers();
        }
        question.SetActive(false);
        questionBackground.SetActive(false);
        gameManager.enableLastQuestion(true);
        close();
    }
    public void Start()
    {
        secondsToShow = 4;
        currentQuestionView =  StartCoroutine(DoTask());
    }

    IEnumerator DoTask()
    {
        yield return new WaitForSeconds(secondsTowait);
        sceneManager.setScene(sceneData.Totale);
        if (firstScene)
        {
            question.GetComponent<UnityEngine.UI.Text>().text = "So you are saying that the earth is flat?";
            questionBackground.SetActive(true);
            question.SetActive(true);
            Debug.Log("waiting for " + secondsToShow + " seconds showing the first question.");
            yield return new WaitForSeconds(secondsToShow);
        }
        else
        {
            Debug.Log("Should show: " + gameManager.currentQuest.getQuestion());
            question.GetComponent<UnityEngine.UI.Text>().text = gameManager.currentQuest.getQuestion();
            questionBackground.SetActive(true);
            question.SetActive(true);
            Debug.Log("But first I wait fot 4 seconds.");
            yield return new WaitForSeconds(secondsToShow);
            gameManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            gameManager.showPossibleAnswers();
            gameManager.enableLastQuestion(true);
        }
        Debug.Log("waiting is over!");
        question.SetActive(false);
        questionBackground.SetActive(false);
        close();
    }


    void close()
    {
        Destroy(this);
    }
}
