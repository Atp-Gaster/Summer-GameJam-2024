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
        
        if (dizzyBar.getDizzyLevel() >= dizzyBar.getMaxDizzyLevel())
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        PlayerPrefs.SetInt("high_score", queueManager.TotalScore);
        SceneManager.LoadScene("TODO", LoadSceneMode.Additive);
    }
}
