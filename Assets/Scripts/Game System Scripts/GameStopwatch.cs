using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStopwatch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    private int secondsSurvived;
    [HideInInspector] public string timeSurvived;

    private void Awake() {
        StartCoroutine(Stopwatch());
    }

    private IEnumerator Stopwatch() {
        while (true) {
            secondsSurvived++;
            if (secondsSurvived >= 60) {
                int minutes = (secondsSurvived / 60) - 1;
                int seconds = secondsSurvived % 60;

                string secondsToText;
                if (seconds < 10) secondsToText = "0" + seconds;
                else secondsToText = seconds.ToString();

                string minutesToText;
                if (minutes < 10) minutesToText = "0" + minutes;
                else minutesToText = minutes.ToString();
                
                timeSurvived = minutesToText + ":" + secondsToText;

                timeText.text = "Вы живёте " + timeSurvived;

                yield return new WaitForSeconds(1);
            }
        }
    }

    public void SaveTime() {
        PlayerPrefs.SetString("SurvivedTime", timeSurvived);
    }
}
