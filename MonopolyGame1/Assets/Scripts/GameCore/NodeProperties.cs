using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperties : MonoBehaviour
{
    [SerializeField] private bool isStepsEffect = false;
    [SerializeField] private bool isContinueMove = false;

    [SerializeField] private int stepsContinueMove = 0;
    [SerializeField] private int stepsEffectValue = 0;

    public void CheckPropertiesNode(StatusStone _statusStone)
    {
        if (isStepsEffect)
        {
            Debug.Log("isStepsEffect");
            _statusStone.stepsEffect = stepsEffectValue;
        }
        if (isContinueMove)
        {
            Debug.Log("isContinueMove");
            _statusStone.MoveStone(stepsContinueMove);
        }
        else
        {
            return;
        }
    }

}
