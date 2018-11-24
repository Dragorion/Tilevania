using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{

    // Config

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    // State
    bool isAlive = true;
    bool attack = false;
    bool slide = false;


    // Cached component references

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;  // collider to bump into walls and enemies etc
    BoxCollider2D myFeetCollider2D; // player collider to prevent wall jumping 

    float myGravityScaleStart;
    float myAnimatorSpeedStart;
    

    // Messages & methods

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();  // capsule collider is used for contact with enemy
        myFeetCollider2D = GetComponent<BoxCollider2D>();  // box collider is used for disable jumping on walls if not touching ground
        myGravityScaleStart = myRigidbody.gravityScale; // getting our player garvity scale in order to make it zero when climbing ladder so we will not fall during climb 
        myAnimatorSpeedStart = myAnimator.speed;  // getting the player animator speed so we can stop the climb animation when we are on the ladder but not moving 
    }

    // Update is called once per frame

    void Update()
    {
         if (!isAlive) { return; } // if player is dead we don't use the below methods 
        run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
        HandleInput(); //check key down for attacks
        HandleAttacks(); // check activated attacks 
    }



    private void run()
    {
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack")) // check if we are attacking so we will not run 
        {
            return;
        }

        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.speed = myAnimatorSpeedStart;
        }

        if (slide && this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
        {
            myAnimator.SetTrigger("Slide");
            slide = false;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // from -1 to +1 x movement
        Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, myRigidbody.velocity.y);  //getting the player x velocity and current y (not 0 because it will stop it on y axis if we do this .)
        myRigidbody.velocity = playerVelocity;  // speed of player on x axis

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;  //true or false :check if player has velocity and is greather then zero (absulute value no negative number)
        myAnimator.SetBool("Running", playerHasHorizontalSpeed); // set the animation name bool to true or false
    }


    private void ClimbLadder()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climb", false); // set the climbing animation to false if we are not touching the ladder
            myRigidbody.gravityScale = myGravityScaleStart; // when we are not on the ladder use the gravity we start with 
            return;
        }  // if player is not touching the ladder don't climb ( we configured the ladder with label climbing)


        
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);  //getting the player x velocity and current y (not 0 because it will stop it on y axis if we do this .)
        myRigidbody.velocity = climbVelocity;  // speed of player on y axis

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;  //true or false :check if player has velocity y and is greather then zero (absulute value no negative number)
        myAnimator.SetBool("Climb", playerHasVerticalSpeed); // set the animation name bool to true or false
        myAnimator.speed = myAnimatorSpeedStart;
        myRigidbody.gravityScale = 0f;

        if (myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")) && myRigidbody.velocity.y == 0)  // stop animation when we are on ladder but not moving 
        {
            myAnimator.SetBool("Climb", true); // set the climbing animation to false if we are not touching the ladder 
            myAnimator.SetBool("Jump", false); // set the jump animation to false if we are not touching the ladder 
            myAnimator.speed = 0;  // stop the climb animation so it will be in the animation but not moving 
        }
    }


    private void Jump()
    {

        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }  // if player is not touching the ground don't jump ( we configured the gound with label ground
        

        if (CrossPlatformInputManager.GetButtonDown("Jump"))   // button for jump in settings is space
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed); // set velocity to y axis
            myRigidbody.velocity += (jumpVelocityToAdd);  // add the velocity
   
        }

        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) // if player stopped on ladder don't go to jumping animation
        {
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;  //true or false :check if player has velocity y and is greather then zero (absulute value no negative number)
            myAnimator.SetBool("Jump", playerHasVerticalSpeed); // set the animation name bool to true or false
        } 

    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
        {
            slide = true;
        }
    }

    private void HandleAttacks()
    {
        if (attack)
        {
            myAnimator.SetTrigger("Attack");
            
            attack = false;               
        }
    }


    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))  // if player collider touch the enemy collider ( enemy layer )
        {
            isAlive = false;
            myAnimator.SetTrigger("PlayerDie");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
            

        }
    }



    private void FlipSprite() // changing player sides image
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;  //check if player has velocity and is greather then zero (absulute value no negative number)
        if (playerHasHorizontalSpeed)
        {
            if (myRigidbody.velocity.x > 0 && transform.localScale.x < 0) // if we going right and we are now facing left
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }

            if (myRigidbody.velocity.x < 0 && transform.localScale.x > 0) // if we going right and we are now facing left
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }


            //transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), transform.localScale.y); // will change the local scale depend of the way we are moving -1 for right and +1 for left
        }
    }



}