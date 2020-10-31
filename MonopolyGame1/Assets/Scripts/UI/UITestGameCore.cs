using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestGameCore : MonoBehaviour
{
    public int min, max;
    public Dropdown dropdown;
    public InputField inputField;
    public Button move_btn;
    public Button random_btn;
    public RandomSteps randomSteps;

    private void Start()
    {
        move_btn.onClick.AddListener(() => OnClick_Move());
        random_btn.onClick.AddListener(() => OnClick_Random());
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
    }

}
