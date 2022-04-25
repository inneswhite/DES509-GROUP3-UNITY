using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        UIFadePanel.instance.SceneFadeOut(1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
