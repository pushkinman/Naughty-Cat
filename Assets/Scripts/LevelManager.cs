using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadLevel(int level)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLeveExtra(level));
    }

    IEnumerator LoadLeveExtra(int level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

    public void OpenYoutube()
    {
        Debug.Log("XX");
        Application.OpenURL("https://www.youtube.com/channel/UCCihVzmZA0S0L9a13DXS7Ew?view_as=subscriber");
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/pushkinman/");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
}
