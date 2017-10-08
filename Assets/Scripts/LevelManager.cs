using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    Scene currentLevel;

    public void LoadNextLevel()
    {
        StartCoroutine(FadeToNextLevel());
    }
    
    public void LoadPreviousLevel()
    {
        StartCoroutine(FadeToPreviousLevel());
    }

    private IEnumerator FadeToNextLevel()
    {
        currentLevel = SceneManager.GetActiveScene();
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(currentLevel.buildIndex + 1);
    }

    private IEnumerator FadeToPreviousLevel()
    {
        currentLevel = SceneManager.GetActiveScene();
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(currentLevel.buildIndex -1);
    }
}
