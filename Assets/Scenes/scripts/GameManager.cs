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

    public SoundManager soundManager;
    public questionManager questionManager;
    public SceneDatabase sceneData;
    public SceneManager sceneManager;

    public database database;
    GameObject currentScene;
    public AbstractQuestion currentQuest;


    public GameObject lastQuestion;

    public void setCurrentQuest(AbstractQuestion q) { currentQuest = q; }

    bool readyToStart = true;
    bool lastQuestionShowable = false;
    public void enableLastQuestion(bool enable)
    {
        lastQuestionShowable = enable;
    }

    public void showLastQuestion()
    {
        if (lastQuestionShowable) { lastQuestion.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getQuestion(); }
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
        yield return new WaitForSeconds(7);
        lossReasonBackground.SetActive(false);
        lossReason.SetActive(false);
    }

    IEnumerator goToNextScene(GameObject nextScene, int secondsTowait)
    {
        yield return new WaitForSeconds(secondsTowait);
        sceneManager.setScene(nextScene);
    }

    IEnumerator showQuestion(int secondsTowait, int secondsToShow, bool firstScene = false)
    {
        yield return new WaitForSeconds(secondsTowait);
        sceneManager.setScene(Totale);
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
            sceneManager.setScene(Nahe);
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

        lastQuestionShowable = true;
    }

    public void startGame()
    {
        if (!readyToStart) { return; }

        reinitialize();

        readyToStart = false;
        sceneManager.setScene(Nahe);

        soundManager.startSound();

        poseQuestion(3, true);

        StartCoroutine(goToNextScene(Nahe, 7));
        StartCoroutine(goToNextScene(Speech, 10));
        StartCoroutine(goToNextScene(Uhr, 12));

        StartCoroutine(goToNextScene(Nahe, 14));
        StartCoroutine(showAnswer(17, 5, "... um... yes!... that's exactly what I'm trying to say!"));
        poseQuestion(22);
    }

    int playSetOfScenesCounterVariable;
    int maxFramesOfScenery;
    public void skipPlaySetOfScenes()
    {
        playSetOfScenesCounterVariable = maxFramesOfScenery;
    }

    IEnumerator playSetOfScenes(List <GameObject> Scenes, List <int> secondsTowait)
    {
        maxFramesOfScenery = secondsTowait[secondsTowait.Count] * 30;
        int sceneCounter = 0;
        for (playSetOfScenesCounterVariable = 0; playSetOfScenesCounterVariable < maxFramesOfScenery; playSetOfScenesCounterVariable++)
        {
            if (secondsTowait[sceneCounter] == playSetOfScenesCounterVariable / 30) { sceneManager.setScene(Scenes[sceneCounter]); sceneCounter++; }
            yield return new WaitForEndOfFrame();
        }
    }

    void reinitialize()
    {
        lossReason.SetActive(false);
        lossReasonBackground.SetActive(false);

        currentQuest = q1;
        questionManager.currentQuestion = q1;

        database.init();
    }

    public void poseQuestion(int secondsToWait = 0, bool firstQuestion = false)
    {
        Debug.Log("Creation qv");
        createQuestionView(secondsToWait, 4, firstQuestion);
        //StartCoroutine(showQuestion(secondsToWait, 7, firstQuestion));
    }

    public void showPossibleAnswers()
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
        sceneManager.setScene(Journalistin);
        StartCoroutine(showLossReason(reason));
        StartCoroutine(goToNextScene(gameOver, 10));
    }

    public void winBig()
    {
        enableAnswerButtons(false);
        StartCoroutine(setRdyToStart(4));
        sceneManager.setScene(goodWin);
    }

    public void winNormal()
    {
        enableAnswerButtons(false);
        StartCoroutine(setRdyToStart(4));
        sceneManager.setScene(normalWin);
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

    void Start()
    {
        sceneManager.setScene(Startscreen);

        q1 = quest1.GetComponent<AbstractQuestion>();
        q2 = quest2.GetComponent<AbstractQuestion>();
        q3 = quest3.GetComponent<AbstractQuestion>();
        q4 = quest4.GetComponent<AbstractQuestion>();
        q5 = quest5.GetComponent<AbstractQuestion>();
        q5e = quest5extra.GetComponent<AbstractQuestion>();
        q6 = quest6.GetComponent<AbstractQuestion>();

        currentQuest = q1;
        questionManager.currentQuestion = currentQuest;

        Application.targetFrameRate = 30;
    }

    public void enableAnswerButtons(bool enable)
    {
        ansBtn1.SetActive(enable);
        ansBtn2.SetActive(enable);
        ansBtn3.SetActive(enable);
    }


    private QuestionView createQuestionView(int secondsToWait, int secondsToShow, bool firstScene = false)
    {
        GameObject taskHolder = new GameObject();
        QuestionView qv = new QuestionView(secondsToWait, secondsToShow,
                                questionBackground, question, this, sceneManager, sceneData,
                                firstScene);
        Debug.Log("Created qv");
        taskHolder.AddComponent<QuestionView>(qv);
        return qv;
    }
    private AnswerView createAnswerView(int secondsToWait, int secondsToShow, string ans)
    {
        AnswerView av = new AnswerView(secondsToWait, secondsToShow,
                                answerBackground, answer, this,
                                ans);
        return av;
    }
    private SceneView createSceneView(int secondsToWait, GameObject nextScene)
    {
        SceneView sv = new SceneView(secondsToWait,
                                nextScene,
                                sceneManager);
        return sv;
    }
}
