using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private AsyncOperation op;
    public void Click()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    { 
        op = SceneManager.LoadSceneAsync("Launch");
        while (!op.isDone && op.progress < 0.9f)
        {
            yield return op.progress;
        }
    }

    public void Update()
    {
        if (op != null)
        {
            // Debug.Log(op.progress);
        }
    }
}
