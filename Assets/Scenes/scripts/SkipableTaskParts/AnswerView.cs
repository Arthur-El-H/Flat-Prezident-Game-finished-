using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerView : MonoBehaviour, ISkipablePartOfTask_VIEW
{
    public AnswerView(int secondsTowait, int secondsToShow, 
                      GameObject answerBackGround, GameObject answer, GameManager gameManager, 
                      string ans)
    {
        this.secondsTowait = secondsTowait;
        this.secondsToShow = secondsToShow;
        this.answer = answer;
        this.answerBackground = answerBackGround;
        this.gameManager = gameManager;
        this.ans = ans;
    }

    public int secondsTowait;
    public int secondsToShow;
    public GameManager gameManager;
    public GameObject answerBackground;
    public GameObject answer;

    string ans;

    public void skipToGoal()
    {
        answer.SetActive(false);
        answerBackground.SetActive(false);
        gameManager.enableLastQuestion(true);
        close();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(secondsTowait);
        answer.GetComponent<UnityEngine.UI.Text>().text = ans;
        answerBackground.SetActive(true);
        answer.SetActive(true);
        yield return new WaitForSeconds(secondsToShow);
        answer.SetActive(false);
        answerBackground.SetActive(false);
        gameManager.enableLastQuestion(true);
        close();
    }

    void close()
    {
        Destroy(this);
    }
}
