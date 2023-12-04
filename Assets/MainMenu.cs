using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText, energyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;

    private const string ENERGYKEY = "Energy";
    private const string ENERGYReadyKEY = "EnergyReady";

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HIGHSCOREKEY, 0);
        highScoreText.text = "HighScore : "+highScore.ToString();

        energy = PlayerPrefs.GetInt(ENERGYKEY, maxEnergy);

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(ENERGYReadyKEY, string.Empty);

            if (energyReadyString == string.Empty)
            {
                return;
            }
            DateTime energyReadyDateTime = DateTime.Parse(energyReadyString);

            if (DateTime.Now> energyReadyDateTime)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(ENERGYKEY, energy);
            }

        }

        energyText.text = $"PLAY ({energy})";
    }

    public void Play()
    {
        if (energy<1)
        {
            return;
        }

        energy--;
        PlayerPrefs.SetInt(ENERGYKEY, energy);

        if (energy==0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(ENERGYReadyKEY, energyReady.ToString());
        }



        SceneManager.LoadScene(1);

    }
}
