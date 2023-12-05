using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText, energyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private AndroidNotificationHandler notificationHandler;

    [SerializeField] private Button _playButton;

    private int energy;

    private const string ENERGYKEY = "Energy";
    private const string ENERGYReadyKEY = "EnergyReady";


    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) { return; }

        CancelInvoke();

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
            else
            {
                _playButton.interactable = false;
                Invoke(nameof(EnergyRecharged),(energyReadyDateTime-DateTime.Now).Seconds);
            }

        }

        energyText.text = $"PLAY ({energy})";
    }

    private void EnergyRecharged()
    {
        _playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt (ENERGYKEY, energy);
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
#if UNITY_ANDROID
            notificationHandler.ScheduleNotification(energyReady);
#endif
        }



        SceneManager.LoadScene(1);

    }
}
