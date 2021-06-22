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
        close();
    }
    public void Start()
    {
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
            gameManager.enableAnswerButtons(true);
            sceneManager.setScene(sceneData.Nahe);
            gameManager.showPossibleAnswers();
        }
        Debug.Log("This called it all!");
        question.SetActive(false);
        questionBackground.SetActive(false);
    }


    void close()
    {
        Debug.Log("This is closed now");
        Destroy(this);
    }
}
