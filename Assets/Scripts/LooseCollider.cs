/*
 * Script related to the loose collider placed on the bottom of the
 * game screen. It manages the event when the ball touches the collider
 * by implementing the OnTriggerEnter2D.
 * When the ball touches the collider, the game over scene is loaded.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //namespace needed for scene management


public class LooseCollider : MonoBehaviour
{
    /*
     * Method that load the last scene of the Build set when a collision 
     * is detected. 
     * This is when the ball hits the bottom collider.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //The scene namecan be passed as a String to the scene manager
        //SceneManager.LoadScene("Game Over");


        // a better implementation is:          

        /*
         * The last scene of the build set is loaded becouse the
         * Game over scene has been put at the end of it.
         */
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
