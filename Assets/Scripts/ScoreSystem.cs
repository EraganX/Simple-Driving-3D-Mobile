using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public const string HIGHSCOREKEY = "HighScore";

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiPlier = 5;
    float score = 0;

    private void Update()
    {
        score += Time.deltaTime * scoreMultiPlier;
        scoreText.text = Mathf.FloorToInt(score).ToString(); 
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HIGHSCOREKEY, 0);
        if (currentHighScore<score)
        {
            PlayerPrefs.SetInt(HIGHSCOREKEY, Mathf.FloorToInt(score));
        }
    }
}
