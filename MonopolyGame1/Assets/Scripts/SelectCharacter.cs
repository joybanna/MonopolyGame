using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public CharacterSelect characterSelect;
    public GameObject arrow;
    private bool isSelect;
    public TypeCharacter typeCharacter;
    private void Start()
    {
        arrow.SetActive(false);
        isSelect = false;
    }
    private void OnMouseDown()
    {
        characterSelect.Select(typeCharacter);
        //SetArrow(true);
        //Debug.Log("mousedown");
    }
    private void OnMouseOver()
    {
        arrow.SetActive(true);
        //Debug.Log("MouseOver");
    }
    private void OnMouseExit()
    {
        arrow.SetActive(isSelect);
    }
    public void SetArrow(bool _isSelect)
    {
        isSelect = _isSelect;
        arrow.SetActive(isSelect);
    }
    public bool GetIsSelect()
    {
        return isSelect;
    }
}
