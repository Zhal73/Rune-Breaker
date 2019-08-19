/*
 * This script controlls the action of the paddle.....
 * .... more comment ....
 * ......
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] float screenWidthInUnits = 18f;
    [SerializeField] float minPaddleXPosition = 0.67f;
    [SerializeField] float maxPaddleXPosition = 15.36f;


    //cached references
    GameStatus theGameStatus;
    Ball theBall;


    // Start is called before the first frame update
    void Start()
    {
        theGameStatus = FindObjectOfType<GameStatus>();

        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        // variable that track the x mouse position
        // float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;

        /*
         * 2D vector that holds the paddle position 
         */
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

       /*
        * variable that limits the movement of the paddle so it doesn't go out of the screen
        * by doing this the paddle y position is locked and the x position is that of the mouse 
        * or the ball in autoplay mode.
        * when the mouse goes out of the scree, the paddle position is limited by the min and 
        * max values.
        */
        paddlePos.x = Mathf.Clamp(GetXPos(), minPaddleXPosition, maxPaddleXPosition);

        //update the paddle position
        transform.position = paddlePos;         
    }


    /*
     * if the game is in autoplay mode, returns the posision of 
     * the ball, otherwise it returns the position of the mouse
     */
    private float GetXPos()
    {
        if(theGameStatus.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }

        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits; ;
        }
    }
}
