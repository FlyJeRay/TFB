using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart_MC_Creator : MonoBehaviour
{
    [SerializeField] private GameObject prefab_Elistratov;
    [SerializeField] private GameObject prefab_Apanasik;
    [SerializeField] private GameObject prefab_Titov;
    [SerializeField] private GameObject prefab_Boguslavskii;
    [SerializeField] private GameObject playerParent;
    
    [HideInInspector] public GameObject currentPlayer;

    private void Awake() {
        GameObject startHero;

        switch(PlayerPrefs.GetString("ChosenHero")) {
            case "Elistratov":
                startHero = prefab_Elistratov;
                break;
            case "Apanasik":
                startHero = prefab_Apanasik;
                break;
            case "Titov":
                startHero = prefab_Titov;
                break;
            case "Boguslavskii":
                startHero = prefab_Boguslavskii;
                break;
            default:
                startHero = prefab_Apanasik;
                break;
        }

        GameObject createdHero = Instantiate(startHero, new Vector2(0, 0), Quaternion.identity, playerParent.transform);
        currentPlayer = createdHero;
        Camera.main.gameObject.transform.SetParent(createdHero.transform);
    }
}
