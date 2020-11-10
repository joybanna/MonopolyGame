using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelStoneController : MonoBehaviour
{
    public List<GameObject> modellist;
    public void StartModelStoneController(TypeCharacter _typeCharacter)
    {
        if (modellist.Count != 0)
        {
            for (int i = 0; i < modellist.Count; i++)
            {
                modellist[i].SetActive(false);
            }

            switch (_typeCharacter)
            {
                case TypeCharacter.normal:
                    modellist[0].SetActive(true);
                    break;
                case TypeCharacter.fast:
                    modellist[1].SetActive(true);
                    break;
                case TypeCharacter.slow:
                    modellist[2].SetActive(true);
                    break;
                default:
                    Debug.LogError("!!! switch (_typeCharacter) == default : " + _typeCharacter);
                    break;
            }

        }
        else
        {
            Debug.LogError("!!! modellist.Count == 0");
        }

    }



}
