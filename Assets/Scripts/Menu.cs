using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

    [SerializeField] CanvasGroup canvasGroup;

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenCreditsWindow() //open canvasgroup 
    {
        // This will set the canvas group to active if it is inactive OR set it to inactive
        canvasGroup.gameObject.SetActive(!canvasGroup.gameObject.activeSelf);


    }

}
