using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{

    public GameObject loaderUI;
    public Slider progressSlider;

    //Play
    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    }

    //Exit
    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadScene_Coroutine(int index)
    {
        progressSlider.value = 0;
        loaderUI.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index); //Controlls the scene loading, using an operation (AsyncOperation)
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            progressSlider.value = Mathf.MoveTowards(progressSlider.value, asyncOperation.progress, Time.deltaTime);
            if(progressSlider.value >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;

        }

    }

}
