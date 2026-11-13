using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingBarFill;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneASync(sceneId));
    }

    IEnumerator LoadSceneASync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/ 0.9f);

            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
