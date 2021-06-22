using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionManager : MonoBehaviour
{
    public AbstractQuestion currentQuestion;
    public GameManager gameManager;

    public void logInOne()
    {
        gameManager.clearAnswers();
        currentQuestion.logInOne();
        gameManager.enableLastQuestion(false);
    }
    public void logInTwo()
    {
        currentQuestion.logInTwo();
        gameManager.enableLastQuestion(false);
    }

    public void logInThree()
    {
        currentQuestion.logInThree();
        gameManager.enableLastQuestion(false);
    }
}
