using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questFive : AbstractQuestion
{
    public GameManager gameManager;

    public string Question = "Even though Argentina is blocking the vote around Section 104, would you send the ships with aid to South Africa?";
    public string answerOne = "We do it anyway, because Argentina as an invented state has no voting rights at all!";
    public string answerTwo = "No, that's absurd!";
    public string answerThree = "For sure! The ships must arrive!";

    public override void logInOne()
    {
        if (database.noCountryInSouth) { gameManager.loose("How can you send ships to South Africa  if there are no countries south of the equator?"); return; } //loose bc noCountryInSouth
        if (database.tunnelsNeeded) { gameManager.loose("How can you send ships to South Africa when it is only accessible by tunnels?"); return; } //loose bc tunnelsNeeded     
        gotoNextQuestion();
    }

    public override void logInTwo()
    {
        gameManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q5e);
        questionManager.currentQuestion = gameManager.q5e;
        gameManager.poseQuestion();
    }

    public override void logInThree()
    {
        if (database.noCountryInSouth) { gameManager.loose("How can you send ships to South Africa  if there are no countries south of the equator?"); return; } //loose bc noCountryInSouth
        if (database.tunnelsNeeded) { gameManager.loose("How can you send ships to South Africa when it is only accessible by tunnels?"); return; } //loose bc tunnelsNeeded      
        gotoNextQuestion();
    }

    void gotoNextQuestion()
    {
        questionManager.currentQuestion = gameManager.q6;
        gameManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q6);
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