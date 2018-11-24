using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  //show text on screen 

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        var numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {

        livesText.text = playerLives.ToString();  //to show the lives on the UI
        scoreText.text = score.ToString();  // to show the score on the UI
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGameSession();
        }
    }


    public void AddToScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        scoreText.text = score.ToString();  // update scrore to UI
    }


    private void TakeLive()
    {
        playerLives--;  //reduce 1 from player lives
        

        StartCoroutine(WaitBeforeRespon());
        
    }

    IEnumerator WaitBeforeRespon()
    {
        yield return new WaitForSecondsRealtime(2);
        livesText.text = playerLives.ToString();  //update lives to UI
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); //load the same scense again after player death 

    }


    private void ResetGameSession()
    {
        StartCoroutine(WaitBeforeMovingToLostScense());
        
    }

    

    IEnumerator WaitBeforeMovingToLostScense()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("PlayerLost");
        Destroy(gameObject);

    }

}
