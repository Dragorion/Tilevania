using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidbody;
    Animator myAnimator;


	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //myAnimator.SetBool("Walking", true); // set the animation name bool to true or false

        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);  // walking to the right (positive number)
        }else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);  // walking to the left (negative number)
        }

        
	}

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;  // check which way enemy is facing 
    }


    private void OnTriggerExit2D(Collider2D collision) // trigger box collider that check if we touch wall turn around 
    {
       
        //transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);  // force flipping of sprite using the minus sign
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}



