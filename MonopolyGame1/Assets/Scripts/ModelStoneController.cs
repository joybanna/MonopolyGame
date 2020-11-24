using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelStoneController : MonoBehaviour
{
    public List<GameObject> modellist;
    public TypeCharacter typeCharacter;
    public void StartModelStoneController(TypeCharacter _typeCharacter)
    {
        typeCharacter = _typeCharacter;
        switch (_typeCharacter)
        {
            case TypeCharacter.normal:
                modellist[0].SetActive(true);
                Debug.Log("TypeCharacter.normal");
                break;
            case TypeCharacter.fast:
                modellist[1].SetActive(true);
                Debug.Log("TypeCharacter.fast");
                break;
            case TypeCharacter.slow:
                modellist[2].SetActive(true);
                Debug.Log("TypeCharacter.slow");
                break;
            default:
                Debug.LogError("!!! switch (_typeCharacter) == default : " + _typeCharacter);
                break;
        }

        if (modellist.Count != 0)
        {

        }
        else
        {
            Debug.LogError("!!! modellist.Count == 0");
        }

    }



}
