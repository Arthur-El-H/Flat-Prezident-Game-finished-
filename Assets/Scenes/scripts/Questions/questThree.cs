using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questThree : AbstractQuestion
{
    public GameManager gameManager;

    public string Question = "Would you say that the history books need to be changed?";
    public string answerOne = "No, the physics books need this more urgently!";
    public string answerTwo = "Of course! This is urgently needed!";
    public string answerThree = "No! The Academy of Science must first prepare a comprehensive report to fully clarify what the flat earth is like!";
    public override void logInOne()
    {
        database.changePhysics = true;
        gotoNextQuestion();
    }

    public override void logInTwo()
    {
        database.changeHistory = true;
        gotoNextQuestion();
    }

    public override void logInThree()
    {
        if (database.akdUnreliable) { gameManager.loose("Why should the Academy prepare a report if it is unreliable?"); return; /* loose because akdUnreliable; */ }
        gotoNextQuestion();
    }
    void gotoNextQuestion()
    {
        questionManager.currentQuestion = gameManager.q4;
        gameManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q4);
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
