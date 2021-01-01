using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperties : MonoBehaviour
{
    public bool isStepsEffect = false;
    public bool isContinueMove = false;

    public int stepsContinueMove = 0;
    public int stepsEffectValue = 0;
    private MeshRenderer modelBlock;
    private Material m_blue, m_red;
    private SoundBox soundBox;
    public void SettingStart()
    {
        m_blue = (Material)Resources.Load("m_blue");
        m_red = (Material)Resources.Load("m_red");
        soundBox = FindObjectOfType<SoundBox>();
        if (GetComponentInChildren<MeshRenderer>() != null && GetComponentInChildren<MeshRenderer>().name == "model")
        {
            modelBlock = GetComponentInChildren<MeshRenderer>();
        }
    }
    public void CheckPropertiesNode(StatusStone _statusStone)
    {
        if (isStepsEffect)
        {
            Debug.Log("isStepsEffect");
            PlaySound(stepsEffectValue);
            _statusStone.stepsEffect = stepsEffectValue;
        }
        if (isContinueMove)
        {
            Debug.Log("isContinueMove");
            PlaySound(stepsContinueMove);
            _statusStone.MoveStone(stepsContinueMove);
        }
        else
        {
            return;
        }
    }
    public void ChangeColorBlock(string _color)
    {
        switch (_color)
        {
            case "blue":
                modelBlock.material = m_blue;
                break;
            case "red":
                modelBlock.material = m_red;
                break;
            default: break;
        }


    }
    private void PlaySound(int _value)
    {
        if (_value > 0)
        {
            soundBox.PalySoundEffect("forward");
        }
        else
        {
            soundBox.PalySoundEffect("backward");
        }
    }

}
