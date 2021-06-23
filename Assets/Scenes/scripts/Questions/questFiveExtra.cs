using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questFiveExtra : AbstractQuestion
{
    public GameManager gameManager;
    [SerializeField] AnswerManager answerManager;

    public string Question = "So there are no relief supplies for South Africa?";
    public string answerOne = "There will be relief supplies!";
    public string answerTwo = "There won't be relief supplies!";
    public string answerThree = "";

    public override void logInOne()
    {
        if (database.noCountryInSouth) { gameManager.loose("How can you send ships to South Africa  if there are no countries south of the equator?"); return; } //loose bc noCountryInSouth
        if (database.noPoll) { gameManager.loose("How can South Africa get aid if the vote on Section 104 did not happen?"); return; } //loose bc there was no Poll     

        else { database.helpForSouthAfrica = true; }
        gotoNextQuestion();
    }

    public override void logInTwo()
    {
        //next question
        gotoNextQuestion();
    }

    public override void logInThree()
    {
        Debug.Log("nothing happens");
    }

    void gotoNextQuestion()
    {
        questionManager.currentQuestion = gameManager.q6;
        answerManager.clearAnswers();
        gameManager.setCurrentQuest(gameManager.q6);
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