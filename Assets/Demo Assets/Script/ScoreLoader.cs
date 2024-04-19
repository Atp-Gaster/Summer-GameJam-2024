using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] public TMP_Text yourScoreText;
    [SerializeField] public TMP_Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        int current_score = PlayerPrefs.GetInt("current_score");
        int high_score = PlayerPrefs.GetInt("high_score");

        yourScoreText.text = "Your Score: " + current_score;
        highScoreText.text = "High Score: " + high_score;
    }
}
