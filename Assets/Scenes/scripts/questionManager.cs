using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionManager : MonoBehaviour
{
    public AbstractQuestion currentQuestion;
    public GameManager gameManager;
    [SerializeField] AnswerManager answerManager;
    [SerializeField] LastQuestionView lastQuestionViewManager;


    public void logInOne()
    {
        answerManager.clearAnswers();
        currentQuestion.logInOne();
        lastQuestionViewManager.enableLastQuestion(false);
    }
    public void logInTwo()
    {
        currentQuestion.logInTwo();
        lastQuestionViewManager.enableLastQuestion(false);
    }

    public void logInThree()
    {
        currentQuestion.logInThree();
        lastQuestionViewManager.enableLastQuestion(false);
    }
}
