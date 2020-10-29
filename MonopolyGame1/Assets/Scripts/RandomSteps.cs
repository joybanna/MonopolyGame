using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSteps : MonoBehaviour
{
    public int min, max;
    public Stone playerStone;
    [SerializeField] private int stepsRandom;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            stepsRandom = Random.Range(min, max);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerStone.MoveSteps(stepsRandom);
        }
    }

}
