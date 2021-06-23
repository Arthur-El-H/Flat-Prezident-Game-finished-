using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxLossView: MonoBehaviour, ISkipablePartOfTask_VIEW
{
    public int secondsTowait;
    public int secondsToShow;
    public GameManager gameManager;
    public SceneManager sceneManager;
    public AnswerManager answerManager;

    public string reason;

    public GameObject lossReason;
    public GameObject lossReasonBackground;
    public GameObject gameOver;

    private Coroutine currentParadoxLossView;
    public void skipToGoal()
    {
        StopCoroutine(currentParadoxLossView);
        sceneManager.setScene(gameOver);
        gameManager.setRdyToStart();
        lossReasonBackground.SetActive(false);
        lossReason.SetActive(false);
        close();
    }
    public void Start()
    {
        currentParadoxLossView = StartCoroutine(DoTask());
    }

    IEnumerator DoTask()
    {
        answerManager.clearAnswers();
        answerManager.enableAnswerButtons(false);

        yield return new WaitForSeconds(secondsTowait);
        lossReasonBackground.SetActive(true);
        lossReason.SetActive(true);
        lossReason.GetComponent<UnityEngine.UI.Text>().text = reason;
        yield return new WaitForSeconds(7);
        lossReasonBackground.SetActive(false);
        lossReason.SetActive(false);
        yield return new WaitForSeconds(3);
        sceneManager.setScene(gameOver);
        gameManager.setRdyToStart();
        close();
    }

    void close()
    {
        Destroy(this);
    }
}