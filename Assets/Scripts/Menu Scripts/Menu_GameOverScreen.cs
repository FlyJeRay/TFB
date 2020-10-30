using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Awake() {
        gameOverText.text = "ИГРА ОКОНЧЕНА.\nВЫ ПРОЖИЛИ " + PlayerPrefs.GetString("SurvivedTime") + ".";
    }
}
