using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerView : MonoBehaviour, ISkipablePartOfTask_VIEW
{
    public int secondsTowait;
    public int secondsToShow;
    public GameManager gameManager;
    public GameObject answerBackground;
    public GameObject answer;
    private Coroutine currentAnswerView;

    public string ans;

    public void skipToGoal()
    {
        answer.SetActive(false);
        answerBackground.SetActive(false);
        StopCoroutine(currentAnswerView);
        close();
    }
    public void Start()
    {
        currentAnswerView = StartCoroutine(DoTask());
    }

    IEnumerator DoTask()
    {
        yield return new WaitForSeconds(secondsTowait);
        answer.GetComponent<UnityEngine.UI.Text>().text = ans;
        answerBackground.SetActive(true);
        answer.SetActive(true);
        yield return new WaitForSeconds(secondsToShow);
        answer.SetActive(false);
        answerBackground.SetActive(false);
        close();
    }

    void close()
    {
        Destroy(this);
    }
}
