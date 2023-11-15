using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime;

    public bool result;

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
        result = true;
        StartCoroutine(LoadScene("WinScene"));
    }

    public void Lose()
    {
        result = false;
        StartCoroutine(LoadScene("LoseScene"));
    }

    IEnumerator LoadScene(string name)
    {
        //play animation
        if(result == true)
        {
            transition.SetTrigger("StartWin");
        }
        if(result == false)
        {
            transition.SetTrigger("StartLose");
        }
        else
        {
            transition.SetTrigger("Start");
        }

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load Scene
        SceneManager.LoadScene(name);
    }

    /*IEnumerator WinScene(string name)
    {
        //play animation
        transition.SetTrigger("StartWin");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load Scene
        SceneManager.LoadScene(name);
    }

    IEnumerator LoseScene(string name)
    {
        //play animation
        transition.SetTrigger("StartLose");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load Scene
        SceneManager.LoadScene(name);
    }*/
}
