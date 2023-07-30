using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image cover;

    private void Start()
    {
        StartCoroutine(CO_StartMenu());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(CO_LoadScene(sceneName));
    }

    private void FadeCover(bool fadeIn, float fadeTime)
    {
        Color toColor = fadeIn ? Color.black : Color.clear;
        LeanTween.color(cover.rectTransform, toColor, fadeTime).setIgnoreTimeScale(true);
    }

    private IEnumerator CO_StartMenu()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        FadeCover(false, 0.5f);
    }

    private IEnumerator CO_LoadScene(string sceneName)
    {
        FadeCover(true, 0.5f);
        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene(sceneName);
    }
}
