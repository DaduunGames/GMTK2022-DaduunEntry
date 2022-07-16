using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator animator;


    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    //public void Options(bool ToOptions)
    //{

    //    animator.SetBool("ToOptions", ToOptions);
    //}

    //public void HowTo(bool ToHowTo)
    //{

    //    animator.SetBool("ToHowTo", ToHowTo);
    //}

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
