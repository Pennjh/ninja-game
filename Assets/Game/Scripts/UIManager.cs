using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added to import UI pieces !!**

public class UIManager : MonoBehaviour
{

    [SerializeField] private Sprite[] lifeImages;
    //array of life images
    [SerializeField] private Image livesImageDisplay;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text bestScoreDisplay;
    private int score = 0;
    private int best = 0;
    [SerializeField] private GameObject titleScreen;


    // Start is called before the first frame update
    void Start()
    {
        //read the highscore file
        best = PlayerPrefs.GetInt("HighScore", 0);
        //updates UI
        bestScoreDisplay.text = "Best: " + best;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lifeImages[currentLives];
        //update the UI picture
        //with the correct 3,2,1,0 life picture
    }
    public void UpdateScore()
    {
        score += 10;
        scoreDisplay.text = "Score: " + score; //concatenation
        CheckForBestScore();
    }
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false); //turn it off
        score = 0;
        scoreDisplay.text = "Score: " + score; //concatenation
    }
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true); //turn it on
    }
    public void CheckForBestScore()
    {
        if(score > best)
        {
            best = score; //updates best score
            bestScoreDisplay.text = "Best: " + best;

            //save to player Prefs file
            PlayerPrefs.SetInt("HighScore", best);
        }

    }
}







