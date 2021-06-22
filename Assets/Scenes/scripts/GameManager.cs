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
    //GameObject currentScene;
    public AbstractQuestion currentQuest;
    public void setCurrentQuest(AbstractQuestion q) { currentQuest = q; }

    [SerializeField] GameObject QuestionViewHolderPrefab;
    [SerializeField] GameObject AnswerViewHolderPrefab;
    [SerializeField] GameObject SceneViewHolderPrefab;
    [SerializeField] GameObject ParadoxLossViewHolderPrefab;

    Queue<ISkipablePartOfTask_VIEW> runningSkipableTasks = new Queue<ISkipablePartOfTask_VIEW>();


    public GameObject lastQuestion;
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


    bool readyToStart = true;
    bool lastQuestionShowable = false;
    bool lastQuestionisShowing;

    public void enableLastQuestion(bool enable)
    {
        lastQuestionShowable = enable;
    }

    public void showLastQuestion()
    {
        if (lastQuestionShowable) 
        {
            if (!lastQuestionisShowing)
            {
                lastQuestion.GetComponent<UnityEngine.UI.Text>().text = currentQuest.getQuestion(); 
                lastQuestionisShowing = true;
            }
            else
            {
                clearLastQuestion();
            }
        }
    }

    public void clearLastQuestion()
    {
        lastQuestion.GetComponent<UnityEngine.UI.Text>().text = "";
        lastQuestionisShowing = false;
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

    public void startGame()
    {
        if (!readyToStart) { return; }

        reinitialize();

        readyToStart = false;
        sceneManager.setScene(Nahe);

        soundManager.startSound();

        createQuestionView(3, true);

        createSceneView(7, Nahe);
        createSceneView(10, Speech);
        createSceneView(12, Uhr);
        createSceneView(14, Nahe);

        createAnswerView(17, 5, "... um... yes!... that's exactly what I'm trying to say!");
        //StartCoroutine(showAnswer(17, 5, "... um... yes!... that's exactly what I'm trying to say!"));
        createQuestionView(22);
    }

    void reinitialize()
    {
        lossReason.SetActive(false);
        lossReasonBackground.SetActive(false);

        currentQuest = q1;
        questionManager.currentQuestion = q1;

        database.init();
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
        //sceneManager.setScene(Journalistin);
        createSceneView(0, Journalistin);
        createParadoxLossView(reason);
    }

    //Winning
    #region
    public void winBig()
    {
        enableAnswerButtons(false);
        setRdyToStart();
        sceneManager.setScene(goodWin);
    }

    public void winNormal()
    {
        enableAnswerButtons(false);
        setRdyToStart();
        sceneManager.setScene(normalWin);
    }

    public void win()
    {
        clearAnswers();
        if (database.helpForSouthAfrica || database.newReform) { winBig(); }
        else { winNormal(); }
    }
    #endregion

    public void setRdyToStart()
    {
        readyToStart = true;
    }


    public void enableAnswerButtons(bool enable)
    {
        ansBtn1.SetActive(enable);
        ansBtn2.SetActive(enable);
        ansBtn3.SetActive(enable);
    }

    public ISkipablePartOfTask_VIEW createParadoxLossView(string reason)
    {
        GameObject taskHolder = Instantiate(ParadoxLossViewHolderPrefab);
        ParadoxLossView pv = taskHolder.GetComponent<ParadoxLossView>();

        pv.gameManager = this;
        pv.sceneManager = sceneManager;
        pv.reason = reason;

        pv.lossReason = lossReason;
        pv.lossReasonBackground = lossReasonBackground;
        pv.gameOver = gameOver;

        runningSkipableTasks.Enqueue(pv);
        return pv;
    }

    public ISkipablePartOfTask_VIEW createQuestionView(int secondsToWait = 0, bool firstScene = false)
    {
        GameObject taskHolder = Instantiate(QuestionViewHolderPrefab);
        QuestionView qv = taskHolder.GetComponent<QuestionView>();
        qv.question = question; // right one?
        qv.secondsTowait = secondsToWait;
        qv.gameManager = this;
        qv.sceneData = sceneData;
        qv.sceneManager = sceneManager;
        qv.questionBackground = questionBackground;
        qv.firstScene = firstScene;

        runningSkipableTasks.Enqueue(qv);
        Debug.Log("Created qv");
        //taskHolder.AddComponent<QuestionView>(qv);
        return qv;
    }
    public ISkipablePartOfTask_VIEW createAnswerView(int secondsToWait, int secondsToShow, string ans)
    {
        GameObject taskHolder = Instantiate(AnswerViewHolderPrefab);
        AnswerView av = taskHolder.GetComponent<AnswerView>();
        av.secondsToShow = secondsToShow;
        av.secondsTowait = secondsToWait;
        av.gameManager = this;
        av.answerBackground = answerBackground;
        av.answer = answer;
        av.ans = ans;

        runningSkipableTasks.Enqueue(av);
        Debug.Log("Created av");
        return av;
    }
    public ISkipablePartOfTask_VIEW createSceneView(int secondsToWait, GameObject nextScene)
    {
        GameObject taskHolder = Instantiate(SceneViewHolderPrefab);
        SceneView sv = taskHolder.GetComponent<SceneView>();
        sv.secondsTowait = secondsToWait;
        sv.sceneManager = sceneManager;
        sv.nextScene = nextScene;

        runningSkipableTasks.Enqueue(sv);
        return sv;
    }

    public void skip()
    {
        while(runningSkipableTasks.Count > 0)
        {
            runningSkipableTasks.Dequeue().skipToGoal();
        }
        Debug.Log("Got out of skip-loop");
    }
}
