using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 0.1f;
    [SerializeField] float exitSlowMotion = 0.1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(RestartGame());
    }


    IEnumerator RestartGame()
    {
        Time.timeScale = exitSlowMotion;
        yield return new WaitForSecondsRealtime (levelLoadDelay);
        Time.timeScale = 1f;
        var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex + 1);
    }

}
