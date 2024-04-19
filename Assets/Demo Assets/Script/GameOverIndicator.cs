using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverIndicator : MonoBehaviour
{
    [SerializeField] public DizzyBar dizzyBar;
    [SerializeField] public QueueManager queueManager;
    private float timer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int dayCounter = queueManager.getDayCounter();
        if (dizzyBar.getDizzyLevel() >= dizzyBar.getMaxDizzyLevel())
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                GameOver();
            }
        }

        if (dayCounter >= 4)
        {
            GameComplete();
        }
    }

    void GameOver()
    {
        saveScore();
        SceneManager.LoadScene("GameOver");
    }

    void GameComplete()
    {
        saveScore();
        SceneManager.LoadScene("GameComplete");
    }

    void saveScore()
    {
        int current_score = queueManager.TotalScore;
        int current_highscore = PlayerPrefs.GetInt("high_score");

        if (current_highscore == 0 || current_highscore < current_score)
        {
            PlayerPrefs.SetInt("high_score", current_score);
        }
        PlayerPrefs.SetInt("current_score", current_score);
    }
}
