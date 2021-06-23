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
    public AnswerManager answerManager;

    public GameObject questionBackground;
    public GameObject question;
    Coroutine currentQuestionView;

    public bool firstScene;

    public void skipToGoal()
    {
        StopCoroutine(currentQuestionView);
        if (!firstScene)
        {
            answerManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            answerManager.showPossibleAnswers();
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
            yield return new WaitForSeconds(secondsToShow);
        }
        else
        {
            question.GetComponent<UnityEngine.UI.Text>().text = gameManager.currentQuest.getQuestion();
            questionBackground.SetActive(true);
            question.SetActive(true);
            yield return new WaitForSeconds(secondsToShow);
            answerManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            answerManager.showPossibleAnswers();
            gameManager.enableLastQuestion(true);
        }
        question.SetActive(false);
        questionBackground.SetActive(false);
        close();
    }

    void close()
    {
        Destroy(this);
    }
}
