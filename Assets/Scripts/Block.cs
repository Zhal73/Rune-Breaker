using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip blockDestroiedSound; //contains the sound played when a block is destroied
  //  [SerializeField] int maxHits;  //contains the numebr of maximum hits a bock can receive before being destoryed
    [SerializeField] Sprite[] hitsprites; //Array that contains the sprites related to different damage level

    //cached references
    Level level;  //variable that reference the level

    //state variables
    [SerializeField] int timesHit; // contains the numebr of time a block has been hit, serialized for debug purpose


    private void Start()
    {
        CountBreakableBlocks();
    }

    /*
     * Count the brakable blocks present in the level
     */
    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();  //assign the Level object to the level variable. 

        //add itself to the total of blocks if is a breakable block
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    /*
     * when a collision is detected and the
     * block is breakable, the method
     * that evaluate if the block has to be destroyed is called
     */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit(); 
        }
    }

    /*
     * Causes the destruction of a breakable block 
     * when a collision is detected,
     * If a block has received the max number fo hits,
     * otherwise it changes the damage level of the block
     */
    private void HandleHit()
    {
        timesHit++;

        //Set the number of maximum hits according to the numbr of damage sprites.
        int maxHits = hitsprites.Length + 1;  
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprites();
        }
    }

    /*
     * Shows a different sprite, representing 
     * a different level of damage
     * of the block. The sprites are contained in the hitSprites array
     */
    private void ShowNextHitSprites()
    {
        int spriteIndex = timesHit - 1;
        /*
         * if statement for debug purpose, it checks if the 
         * a sprite is missing in the array contains the sprites
         */
        if(hitsprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitsprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from array in "+  gameObject.name);
        }

    }


    /*
     * When a block has to be destroyed, the score is updated
     * a sound is played, the total of the remaining blocks
     * is updated and the block object is destroyed.
     */
    private void DestroyBlock()
    {
        
        PlayBlockDestoySFX();

        //the count of the block is updated
        level.BlockDestroyed();

        //The Block is destroied
        Destroy(gameObject);
        
        


    }

    /*
     * Playes a sound when the block is destroyed
     */
    private void PlayBlockDestoySFX()
    {
        //update the score
        FindObjectOfType<GameStatus>().AddToScore();
        /*
         * plays a sound when a block is destroied,
         * the sound is positioned where by the camera.
         */
        AudioSource.PlayClipAtPoint(blockDestroiedSound, Camera.main.transform.position);
    }
}
