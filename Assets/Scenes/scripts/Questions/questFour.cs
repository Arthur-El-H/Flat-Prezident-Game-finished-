using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questFour : AbstractQuestion
{
    public GameManager gameManager;

    public string Question = "So would you say that your impressively successful diplomatic stay in Australia didn't happen at all?";
    public string answerOne = "Without this stay, Section 104 would not have even come up for a vote!";
    public string answerTwo = "On the flight back it seemed a bit surreal myself!";
    public string answerThree = "Where should I have been?";
    public override void logInOne()
    {
        if(database.noCountryInSouth) { gameManager.loose("How can you have been to Australia if there are no countries south of the equator?"); return; } //loose bc noCountryInSouth
        gotoNextQuestion();
    }

    public override void logInTwo()
    {
        if (database.noCountryInSouth) { gameManager.loose("How can you have been to Australia if there are no countries south of the equator?"); return; } //loose bc noCountryInSouth
        if (database.tunnelsNeeded) { gameManager.loose("How can you have flown when all countries south of the equator are only accessible by tunnel?"); return; } //loose bc tunnelsNeeded
        gotoNextQuestion();
    }

    public override void logInThree()
    {
        database.noPoll = true;
        gotoNextQuestion();
    }

    void gotoNextQuestion()
    {
        questionManager.currentQuestion = gameManager.q5;
        gameManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q5);
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
