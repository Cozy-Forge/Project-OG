using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneLoader : MonoBehaviour
{

    public static string nextScene;
    [SerializeField] Image progressBar;

    private void Start()
    {

        StartCoroutine(LoadScene());

    }

    public static void LoadScene(string sceneName)
    {

        nextScene = sceneName;
        SceneManager.LoadScene("Loading");

    }

    IEnumerator LoadScene()
    {

        yield return new WaitForSeconds(0.1f);

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {

            yield return null;

            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {

                    timer = 0f;

                }

            }
            else
            {

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {

                    break;

                }

            }

        }

        yield return new WaitForSeconds(0.1f);
        op.allowSceneActivation = true;

    }

}
