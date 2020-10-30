using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_ChooseCharacterButton : MonoBehaviour
{
    public void ChooseCharacter(string name_ofCharacter) {
        PlayerPrefs.SetString("ChosenHero", name_ofCharacter);
    }
}
