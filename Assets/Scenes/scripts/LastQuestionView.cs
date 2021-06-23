using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastQuestionView : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject lastQuestion;
    bool lastQuestionShowable;
    bool lastQuestionisShowing;

    public void enableLastQuestion(bool enable)
    {
        lastQuestionShowable = enable;
    }

    public void showLastQuestion()
    {
        if (lastQuestionShowable)
        {
            if (!lastQuestionisShowing)
            {
                lastQuestion.GetComponent<UnityEngine.UI.Text>().text = gameManager.currentQuest.getQuestion();
                lastQuestionisShowing = true;
            }
            else
            {
                clearLastQuestion();
            }
        }
    }

    public void clearLastQuestion()
    {
        lastQuestion.GetComponent<UnityEngine.UI.Text>().text = "";
        lastQuestionisShowing = false;
    }
}
