using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionManager : MonoBehaviour
{
    public AbstractQuestion currentQuestion;
    public GameManager gameManager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void logInOne()
    {
        gameManager.clearAnswers();
        currentQuestion.logInOne();
    }
    public void logInTwo()
    {
        currentQuestion.logInTwo();
    }

    public void logInThree()
    {
        currentQuestion.logInThree();
    }

    public void test()
    {
        Debug.Log("test");
    }

}
