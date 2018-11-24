using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePersist : MonoBehaviour {

    //Coins and pickups will not load again if player dies and scene is loaded again
    //coins need to be a child of this gameobject for this to work .

    int startingSceneIndex;

    private void Awake()
    {
        var numGamePersist = FindObjectsOfType<GamePersist>().Length;
        if (numGamePersist > 1)
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
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (startingSceneIndex != currentSceneIndex)
        {
            Destroy(gameObject);
        }

    }
}
