using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject quest1;
    public GameObject quest2;
    public GameObject quest3;
    public GameObject quest4;
    public GameObject quest5;
    public GameObject quest5extra;
    public GameObject quest6;

    public AbstractQuestion q1;
    public AbstractQuestion q2;
    public AbstractQuestion q3;
    public AbstractQuestion q4;
    public AbstractQuestion q5;
    public AbstractQuestion q5e;
    public AbstractQuestion q6;


    public GameObject Nahe;
    public GameObject Totale;
    public GameObject Journalistin;
    public GameObject Speech;
    public GameObject Uhr;

    public GameObject Startscreen;
    public GameObject normalWin;
    public GameObject goodWin;
    public GameObject gameOver;

    public GameObject questionBackground;
    public GameObject question;
    public GameObject answerBackground;
    public GameObject answer;

    public GameObject lossReasonBackground;
    public GameObject lossReason;

    public GameObject ans1;
    public GameObject ans2;
    public GameObject ans3;

    public GameObject ansBtn1;
    public GameObject ansBtn2;
    public GameObject ansBtn3;

    public questionManager questionManager;

    public database database;
    GameObject currentScene;
    AbstractQuestion currentQuest;

    public AudioSource soundscape;
    public AudioSource soundtrack;

    public GameObject lastQuestion;


    public void setCurrentQuest(AbstractQuestion q) { currentQuest = q; }

    bool readyToStart = true;

    public void showLastQuestion()
    {
        if (!readyToStart) { lastQuestion.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getQuestion(); }
    }

    public void clearLastQuestion()
    {
        lastQuestion.GetComponent<UnityEngine.UI.Text>().text = ""; 
    }


    IEnumerator showLossReason(string reason, int secondsTowait = 0)
    {
        yield return new WaitForSeconds(secondsTowait);
        lossReasonBackground.SetActive(true);
        lossReason.SetActive(true);
        lossReason.GetComponent<UnityEngine.UI.Text>().text = reason;
        Debug.Log(reason);
        yield return new WaitForSeconds(7);
        lossReasonBackground.SetActive(false);
        lossReason.SetActive(false);
    }

    IEnumerator goToNextScene(GameObject nextScene, int secondsTowait)
    {
        yield return new WaitForSeconds(secondsTowait);
        setScene(nextScene);
    }

    IEnumerator showQuestion(int secondsTowait, int secondsToShow, bool firstScene = false)
    {
        yield return new WaitForSeconds(secondsTowait);
        setScene(Totale);
        if (firstScene)
        {
            question.GetComponent<UnityEngine.UI.Text>().text = "So you are saying that the earth is flat?";
        }
        else 
        { 
            question.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getQuestion();
        }

        questionBackground.SetActive(true);
        question.SetActive(true);
        yield return new WaitForSeconds(secondsToShow);

        if (!firstScene)
        {
            ansBtn1.SetActive(true);
            ansBtn2.SetActive(true);
            ansBtn3.SetActive(true);
        }


        question.SetActive(false);
        questionBackground.SetActive(false);
        if(!firstScene)
        {
            setScene(Nahe);
            showPossibleAnswers();
        }
    }

    IEnumerator showAnswer(int secondsTowait, int secondsToShow, string ans)
    {
        yield return new WaitForSeconds(secondsTowait);
        answer.GetComponent<UnityEngine.UI.Text>().text = ans;
        answerBackground.SetActive(true);
        answer.SetActive(true);
        yield return new WaitForSeconds(secondsToShow);
        answer.SetActive(false);
        answerBackground.SetActive(false);
    }

    public void startGame()
    {
        if (!readyToStart) { return; }

        initialize();

        readyToStart = false;
        setScene(Nahe);
        soundtrack.loop = true;
        soundscape.loop = true;
        soundscape.Play();

        StartCoroutine(showQuestion(3, 4, true));
        //StartCoroutine(goToNextScene(Totale, 3));

        StartCoroutine(goToNextScene(Nahe, 7));
        StartCoroutine(goToNextScene(Speech, 10));
        StartCoroutine(goToNextScene(Uhr, 12));

        StartCoroutine(goToNextScene(Nahe, 14));
        StartCoroutine(showAnswer(17, 5, "... um... yes!... that's exactly what I'm trying to say!"));

        poseQuestion(22);
    }

    void initialize()
    {
        lossReason.SetActive(false);
        lossReasonBackground.SetActive(false);

        currentQuest = q1;
        questionManager.currentQuestion = q1;

        database.init();
    }

    void setScene(GameObject newScene)
    {
        clearLastQuestion();
        currentScene.SetActive(false);
        currentScene = newScene;
        currentScene.SetActive(true);
    }

    public void poseQuestion(int secondsToWait = 0)
    {
        StartCoroutine(showQuestion(secondsToWait, 7));
    }

    void showPossibleAnswers()
    {
        ans1.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getOne();
        ans2.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getTwo();
        ans3.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getThree();
    }

    public void clearAnswers()
    {
        ans1.GetComponent<UnityEngine.UI.Text>().text = "";
        ans2.GetComponent<UnityEngine.UI.Text>().text = "";
        ans3.GetComponent<UnityEngine.UI.Text>().text = "";
    }

    public void loose(string reason)
    {
        clearAnswers();
        StartCoroutine(setRdyToStart(11));
        ansBtn1.SetActive(false);
        ansBtn2.SetActive(false);
        ansBtn3.SetActive(false);
        setScene(Journalistin);
        StartCoroutine(showLossReason(reason));
        StartCoroutine(goToNextScene(gameOver, 10));
    }

    public void winBig()
    {
        ansBtn1.SetActive(false);
        ansBtn2.SetActive(false);
        ansBtn3.SetActive(false);
        StartCoroutine(setRdyToStart(4));
        setScene(goodWin);
    }

    public void winNormal()
    {
        ansBtn1.SetActive(false);
        ansBtn2.SetActive(false);
        ansBtn3.SetActive(false);
        StartCoroutine(setRdyToStart(4));
        setScene(normalWin);
    }

    public void win()
    {
        clearAnswers();
        if (database.helpForSouthAfrica || database.newReform) { winBig(); }
        else { winNormal(); }
    }

    IEnumerator setRdyToStart(int secondsTowait)
    {
        yield return new WaitForSeconds(secondsTowait);
        readyToStart = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentScene = Startscreen;
        currentScene.SetActive(true);

        q1 = quest1.GetComponent<AbstractQuestion>();
        q2 = quest2.GetComponent<AbstractQuestion>();
        q3 = quest3.GetComponent<AbstractQuestion>();
        q4 = quest4.GetComponent<AbstractQuestion>();
        q5 = quest5.GetComponent<AbstractQuestion>();
        q5e = quest5extra.GetComponent<AbstractQuestion>();
        q6 = quest6.GetComponent<AbstractQuestion>();

        currentQuest = q1;
        questionManager.currentQuestion = currentQuest;

        soundtrack.loop = true;
    }

    // Update is called once per frame

    bool sound = true;
    public void toggleSound()
    {
        if (sound)
        {
            Debug.Log("hier");
            soundtrack.Stop();
            soundscape.Stop();
            sound = false;
        }

        else
        {
            Debug.Log("da");
            soundtrack.Play();
            soundscape.Play();
            soundscape.loop = true; 
            soundtrack.loop = true; 
            sound = true; 
        }
    }


}
