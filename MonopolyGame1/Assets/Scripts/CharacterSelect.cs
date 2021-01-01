using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public PageController pageController;
    public SelectCharacter[] selectCharacters;
    public TypeCharacter typeCharacter;
    public GameObject wall;

    public void Select(TypeCharacter _type)
    {
        for (int i = 0; i < selectCharacters.Length; i++)
        {
            if (selectCharacters[i].GetIsSelect() == true)
            {
                selectCharacters[i].SetArrow(false);
            }
        }
        for (int i = 0; i < selectCharacters.Length; i++)
        {
            if (selectCharacters[i].typeCharacter == _type)
            {
                selectCharacters[i].SetArrow(true);
                typeCharacter = _type;
            }
        }

        pageController.Onclick_play();

    }
    public void OnEnableScript(bool _isEnable)
    {
        for (int i = 0; i < selectCharacters.Length; i++)
        {
            selectCharacters[i].enabled = _isEnable;
        }
        this.enabled = _isEnable;
        wall.SetActive(!_isEnable);
    }
}
