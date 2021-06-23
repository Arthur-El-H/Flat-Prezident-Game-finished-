using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject ans1;
    [SerializeField] GameObject ans2;
    [SerializeField] GameObject ans3;

    private UnityEngine.UI.Text ans1Text;
    private UnityEngine.UI.Text ans2Text;
    private UnityEngine.UI.Text ans3Text;

    [SerializeField] GameObject ansBtn1;
    [SerializeField] GameObject ansBtn2;
    [SerializeField] GameObject ansBtn3;
    private void Start()
    {
        ans1Text = ans1.GetComponent<UnityEngine.UI.Text>();
        ans2Text = ans2.GetComponent<UnityEngine.UI.Text>();
        ans3Text = ans3.GetComponent<UnityEngine.UI.Text>();

    }

    public void showPossibleAnswers()
    {
        AbstractQuestion currentQuest = gameManager.currentQuest;
        ans1Text.text = currentQuest.getOne();
        ans2Text.text = currentQuest.getTwo();
        ans3Text.text = currentQuest.getThree();
    }

    public void clearAnswers()
    {
        ans1Text.text = "";
        ans2Text.text = "";
        ans3Text.text = "";
    }

    public void enableAnswerButtons(bool enable)
    {
        ansBtn1.SetActive(enable);
        ansBtn2.SetActive(enable);
        ansBtn3.SetActive(enable);
    }
}
