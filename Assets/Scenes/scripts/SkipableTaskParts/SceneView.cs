using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneView : MonoBehaviour, ISkipablePartOfTask_VIEW
{
    public SceneView(int secondsTowait,
                     GameObject nextScene,
                     SceneManager sceneManager)
    {
        this.secondsTowait = secondsTowait;

        this.nextScene = nextScene;

        this.sceneManager = sceneManager;
    }

    public int secondsTowait;
    public GameObject nextScene;
    public SceneManager sceneManager;
    private Coroutine currenSceneView;

    public void skipToGoal()
    {
        sceneManager.setScene(nextScene);
        StopCoroutine(currenSceneView);
        close();
    }
    public void Start()
    {
        currenSceneView = StartCoroutine(DoTask());
    }

    IEnumerator DoTask()
    {
        yield return new WaitForSeconds(secondsTowait);
        sceneManager.setScene(nextScene);
        close();
    }

    void close()
    {
        Destroy(this);
    }

}
