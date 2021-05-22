using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questSix : AbstractQuestion
{
    public GameManager gameManager;

    public string Question = "Mr.Prime Minister.What education reform do you support?";
    public string answerOne = "10,000,- Funding for all subjects; curriculum change as recommended by the Academy of Science: Baltic 'geography', formal logic 'mathematics'.";
    public string answerTwo = "20,000,- Grants for mathematics and physics; curriculum change as recommended by the Academy: Rome 'history', formal logic 'mathematics'.";
    public string answerThree = "No funding, no curriculum changes";

    public override void logInOne()
    {
        if (database.akdUnreliable) { gameManager.loose("How can you accept a program from the unreliable Academy of Science?"); return; } //loose bc akdUnreliable
        if (database.akdNoGeography) { gameManager.loose("How can you agree with reform 1 when the academy should remain  silent on geography?"); return; } //loose bc akdNoGeography        
        else { database.newReform = true; } // win
        gameManager.win();

    }

    public override void logInTwo()
    {
        if (database.changeHistory) { gameManager.loose("How to change the history books without funding?"); return; } //loose bc history needs to be changed and needs Money
        if (database.akdUnreliable) { gameManager.loose("How can you accept a program from the unreliable Academy of Science?"); return; } //loose bc akdUnreliable
        else { database.newReform = true; } //win
        gameManager.win();

    }

    public override void logInThree()
    {
        gameManager.win();
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
