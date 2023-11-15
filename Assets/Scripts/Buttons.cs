using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void InstructionButton()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void Restart()
    {

    }
}
