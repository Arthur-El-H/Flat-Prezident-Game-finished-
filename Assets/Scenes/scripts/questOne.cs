using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questOne : AbstractQuestion
{
    public GameManager gameManager;

    public string Question = "Would you say that the Academy of Science is not a trustworthy source?";
    public string answerOne = "The Academy works reliably of course!";
    public string answerTwo = "The academy works reliably, but on geographical issues it would be better for her to remain silent!";
    public string answerThree = "Exactly! And I would also like to install an additional control body to verify its independence!";

    public override void logInOne()
    {
        gameManager.loose("How can you say that the academy is reliable but the earth is flat?");
        //loose bc earth is flat
    }
    public override void logInTwo()
    {
        database.akdNoGeography = true;
        gotoNextQuestion();
    }
    public override void logInThree()
    {
        database.akdUnreliable = true;
        database.akdNeedsControl = true;
        gotoNextQuestion();
    }

    void gotoNextQuestion()
    {
        questionManager.currentQuestion = gameManager.q2;
        gameManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q2);
        gameManager.createQuestionView();
    }

    public override string getQuestion()
    {
        return Question;
    }

    public override string getOne()
    {
        return answerOne;
    }

    public override string getTwo()
    {
        return answerTwo;
    }

    public override string getThree()
    {
        return answerThree;
    }
}

