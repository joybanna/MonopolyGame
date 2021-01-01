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
    private int lastRoll;
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

        StartCoroutine(WaitForMove());
    }
    public void SetMaxMinRoll(TypeCharacter _typeCharacter)
    {
        switch (_typeCharacter)
        {
            case TypeCharacter.normal:
                max = 5;
                min = 1;
                break;
            case TypeCharacter.fast:
                max = 6;
                min = 2;
                break;
            case TypeCharacter.slow:
                max = 4;
                min = 1;
                break;
            default:
                Debug.LogWarning("!!! SetMaxMinRoll : " + _typeCharacter);
                break;
        }
    }
    public int Roll()
    {
        int temp_roll = Random.Range(min, max);

        while (ReRoll(temp_roll, lastRoll))
        {
            temp_roll = Random.Range(min, max);
        }

        lastRoll = temp_roll;
        return temp_roll;
    }
    public bool ReRoll(int _temproll, int _lastroll)
    {
        return _lastroll == _temproll;
    }

    IEnumerator WaitForMove()
    {
        randomSteps.gameControllerCenter.soundBox.PalySoundEffect("roll");
        int temp_move = 0;
        for (int i = 0; i < 7; i++)
        {
            temp_move = Random.Range(min, max);
            yield return new WaitForEndOfFrame();
            inputField.text = temp_move.ToString();
        }

        temp_move = Roll();
        inputField.text = temp_move.ToString();
        randomSteps.stepsRandom = temp_move;
        random_btn.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        randomSteps.MoveStone();
    }
}




