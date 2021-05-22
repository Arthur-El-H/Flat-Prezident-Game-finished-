using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questTwo : AbstractQuestion
{
    public GameManager gameManager;

    public string Question = "Would you say that all countries south of the equator do not exist?";
    public string answerOne = "We would never deny the existence of our diplomatic partners!";
    public string answerTwo = "You got it! They do not exist!";
    public string answerThree = "Not at all! However, they only exist accessible by tunnels on the underside of the disc!";
    public override void logInOne()
    {
        gameManager.loose("How can there be countries south of the equator if the earth is flat?");
        //loose bc earth is flat ??
    }

    public override void logInTwo()
    {
        database.noCountryInSouth = true;
        gotoNextQuestion();
    }

    public override void logInThree()
    {
        database.tunnelsNeeded = true;
        gotoNextQuestion();
    }

    void gotoNextQuestion()
    {
        questionManager.currentQuestion = gameManager.q3;
        gameManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q3);
        gameManager.poseQuestion();
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
