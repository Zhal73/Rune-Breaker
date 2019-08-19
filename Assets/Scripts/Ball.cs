/*
 * This script controlls the action of the ball.....
 * .... more comment ....
 * ......
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //configuration parameter
    [SerializeField] Paddle paddle1; //variable that referes to the game paddle
    [SerializeField] float xPush = 2f; //indicates the x velocity of which the ball is launched
    [SerializeField] float yPush = 8f; //indicates the y velocity of which the ball is launched
    [SerializeField] AudioClip[] ballSounds; //Array that contains the various sounds of the ball that hit something
    [SerializeField] float randomFactor = 0.2f; //random factor to add to the ball's velocity to avoid boring loops



    //state
    Vector2 paddleToBallDistance; //2D vector that holds the distance between the ball and the paddle
    bool gameHasStarted; //variable that indicated if the game has started

    // cached component references
    AudioSource myAudioSource; //variable that holds a reference to the audio souce
    Rigidbody2D myRigidBody2D; //variable that holds a reference to the ball's Rigidbody

    Level theLevel;


    // Start is called before the first frame update
    void Start()
    {
        //initialise the game status
        gameHasStarted = false;
        //initialise the distance between the ball and the paddle at the beginning of a level
        paddleToBallDistance = transform.position - paddle1.transform.position;
        //initialise the audio source
        myAudioSource = GetComponent<AudioSource>();
        //initialise the Rigidbody reference
        myRigidBody2D = GetComponent<Rigidbody2D>();

        theLevel = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        } 
    }

    /*
     * This method lauches the ball from the 
     * paddle when the player clicks the left 
     * button on the mouse
     */
    private void LaunchOnMouseClick()
    {
        //Actions done when the left mouse button is pressed
        if(Input.GetMouseButtonDown(0))
        {
            //set the status of the game to indicate that the game has started 
            gameHasStarted = true;
            //change the velocity of the ball by accessing the Rigidbody2D velocity
            myRigidBody2D.velocity = new Vector2(xPush,yPush);

            theLevel.hideExplaination();
        }
    }

    /*
     * This method get the paddle postition, change the ball position
     * by adding the distance between ball and paddle to the paddle position. 
     */
    private void LockBallToPaddle()
    {
        //2D vector that holds the position of the paddle
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);

        /*
         * update the ball position so it is locked to the paddle
         * The ball position will be the paddle position plus the distance between
         * ball and paddle.
         */
        transform.position = paddlePos + paddleToBallDistance;
    }


    /*
     * A sound is played whenever the ball 
     * touches an object that has a collider 
     * such as Blocks or walls.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //variable that introduces the random velocity variation
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(-randomFactor,randomFactor), 
                                            UnityEngine.Random.Range(-randomFactor, randomFactor));
     



        /*
         * The sound is produced once the game has started 
         * and a collision occours.
         * This to avoid to hear a sound when the game loads 
         * but the ball has not been launched yet.
         * */
        
        if(gameHasStarted)
        {
            /*
             * If I use only one sound, I assign this sound to
             * the Audio Source on the inspector, and I play it with:
             * 
             * myAudioSource().Play();
             */

            /*
             * When I use an array of sounds, I don't assign any sound to the
             * Audio source on the inspector.
             * the sound to play is randomly chosen  from those in the array of sounds ballSounds
             */
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];

            //the chosen sound is played
            myAudioSource.PlayOneShot(clip);

            //add the velocity tweak
            myRigidBody2D.velocity += velocityTweak;
  
        }
        
    }
}
