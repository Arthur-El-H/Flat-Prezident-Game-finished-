using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractQuestion: MonoBehaviour
{
    public database database;

    public questionManager questionManager;


    public abstract string getQuestion();
    public abstract string getOne();
    public abstract string getTwo();
    public abstract string getThree();
    

    public abstract void logInOne();

    public abstract void logInTwo();

    public abstract void logInThree();
}
