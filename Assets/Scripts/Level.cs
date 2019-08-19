using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    //configuration parameters
    [SerializeField] int breakableBlocks; // Serialized for debug purposes
    [SerializeField] TextMeshProUGUI explainatoryText;

    //cached references
    SceneLoader sceneLoader;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>(); //assign the SceneLoader object to the variable sceneLoader
    }


    /*
     * method that increase the value of the variable breakableBlocks
     * It is called by each block to count them all.
     */
    public void CountBlocks()
    {   
        //add a block to the number of breakable blocks
        breakableBlocks++;
    }

    /*
     * method that decrease the variable breakableBlocks
     * It is called by a block when it is destroyed.
     * When there are no blocks left it loads the next Level.
     */
    public void BlockDestroyed()
    {
        //decrease the count of the remaining blocks
        breakableBlocks--;

        
        if(breakableBlocks <= 0)
        {
            sceneLoader.loadNextScene();   
        }
    }

    /*
     * Hides the explainatory text on an intro level
     */
    public void hideExplaination()
    {
        explainatoryText.text = " ";
    }

    
}
