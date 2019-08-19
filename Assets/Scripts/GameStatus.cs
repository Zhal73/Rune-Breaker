using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // configuration parameters

    /*
     * this variable indicates the speed of the game
     * it is set to one, but can have values from
     * 0.1 and 10.7
     */
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;

    //points per block 
    [SerializeField] int pointsPerBlockDestroyed = 83;

    //holder for the scre
    [SerializeField] TextMeshProUGUI scoreText;

    //toggle autoplay on and off
    [SerializeField] bool isAutoPlayEnabled;

    //state variables
    [SerializeField] int currentScore = 0;


    /*
     * This method chechs if there are other GameStatus objects.
     * If it is the case the new objcet is destroyes and only
     * the first one is retained.
     * This is an implementation of the Singleton Pattern
     * to keep track of the score when a new level is loaded.
     */
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if(gameStatusCount > 1)
        {
            /*
             * Avoids having two game object active in the same frame, 
             * because Destroy is performed at th very end of the frame
             */
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        scoreText.text = currentScore.ToString();  
    }

    // Update is called once per frame
    void Update()
    {
        //set the actual game speed
        Time.timeScale = gameSpeed;    
    }

    /*
     * method called by the Block script
     * it updates the score when a block
     * is destoryed.
     */
    public void AddToScore()
    {
        //update the score value
        currentScore += pointsPerBlockDestroyed;
        // update the score text
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);   
    }

    /*
     * Returns the value of the variable IsAutoPalyEnabled
     */
     public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
