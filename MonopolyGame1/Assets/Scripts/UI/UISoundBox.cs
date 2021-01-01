using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundBox : MonoBehaviour
{
    public AudioClip click, conneted, disconneted;
    public AudioSource audioSource_Ef;
    public AudioSource audioSource_Bg;
    public void PalySoundEffect(string _sound)
    {
        switch (_sound)
        {
            case "click":
                audioSource_Ef.PlayOneShot(click);
                break;
            case "conneted":
                audioSource_Ef.PlayOneShot(conneted);
                break;
            case "disconneted":
                audioSource_Ef.PlayOneShot(disconneted);
                break;
            default:
                Debug.Log("sound not found !!!");
                break;
        }
    }

}
