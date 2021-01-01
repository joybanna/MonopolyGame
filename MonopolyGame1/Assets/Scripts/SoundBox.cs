using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    [HideInInspector] public GameControllerCenter gameControllerCenter;
    public AudioClip move, roll, forward, backward;
    public AudioSource audioSource_Ef;
    public AudioSource audioSource_Bg;

    public void PalySoundEffect(string _sound)
    {
        switch (_sound)
        {
            case "move":
                audioSource_Ef.PlayOneShot(move);
                break;
            case "roll":
                audioSource_Ef.PlayOneShot(roll);
                break;
            case "forward":
                audioSource_Ef.PlayOneShot(forward);
                break;
            case "backward":
                audioSource_Ef.PlayOneShot(backward);
                break;
            default:
                Debug.Log("sound not found !!!");
                break;
        }
    }
    public void PalySoundBG()
    {
        audioSource_Bg.Play();
    }


}
