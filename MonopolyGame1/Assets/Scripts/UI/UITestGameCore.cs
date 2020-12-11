using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestGameCore : MonoBehaviour
{
    private int min, max;
    public Dropdown dropdown;
    public InputField inputField;
    public Button move_btn;
    public Button random_btn;
    public RandomSteps randomSteps;

    private void Start()
    {
        move_btn.onClick.AddListener(() => OnClick_Move());
        random_btn.onClick.AddListener(() => OnClick_Random());
        move_btn.gameObject.SetActive(false);
        dropdown.gameObject.SetActive(false);
    }
    private void OnClick_Move()
    {
        randomSteps.no_player = dropdown.value;
        randomSteps.stepsRandom = int.Parse(inputField.text);
        randomSteps.MoveStone();
    }
    private void OnClick_Random()
    {
        inputField.text = Random.Range(min, max).ToString();
        randomSteps.stepsRandom = int.Parse(inputField.text);
        randomSteps.MoveStone();
        random_btn.gameObject.SetActive(false);
    }
    public void SetMaxMinRoll(TypeCharacter _typeCharacter)
    {
        switch (_typeCharacter)
        {
            case TypeCharacter.normal:
                max = 4;
                min = 1;
                break;
            case TypeCharacter.fast:
                max = 5;
                min = 0;
                break;
            case TypeCharacter.slow:
                max = 3;
                min = 1;
                break;
            default:
                Debug.LogWarning("!!! SetMaxMinRoll : " + _typeCharacter);
                break;
        }
    }

}




