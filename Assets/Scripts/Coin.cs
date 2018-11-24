using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {


    [SerializeField] AudioClip coinPickUpSound;
    [SerializeField] int coinScoreValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(coinScoreValue);
        AudioSource.PlayClipAtPoint(coinPickUpSound, Camera.main.transform.position); //play audio even if the gameobject is destroyed in using main camera location 
        Destroy(gameObject);
    }
}
