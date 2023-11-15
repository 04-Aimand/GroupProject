using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime;


    public void PlayButton()
    {
        StartCoroutine(LoadScene("Level1"));
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        StartCoroutine(LoadScene("Level1"));
    }

    public void Menu()
    {
        StartCoroutine(LoadScene("Title"));
    }

    public void InstructionButton()
    {
        StartCoroutine(LoadScene("Instructions"));
    }

    public void Win()
    {
        StartCoroutine(LoadScene("WinScene"));
    }

    public void Lose()
    {
        StartCoroutine(LoadScene("LoseScene"));
    }

    IEnumerator LoadScene(string name)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load Scene
        SceneManager.LoadScene(name);
    }
}
