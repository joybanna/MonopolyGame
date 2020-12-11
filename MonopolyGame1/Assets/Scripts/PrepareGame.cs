using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareGame : MonoBehaviour
{
    //start map setting ---
    //spawn stone ---
    //set position stone ---
    //generate option map ---
    //start trunmanagement
    //check All ready
    public GameControllerCenter gameControllerCenter;
    public IEnumerator SettingStart()
    {
        gameControllerCenter.route.SettingRoute();
        yield return new WaitForEndOfFrame();
        gameControllerCenter.checkReady.SettingStart();
        gameControllerCenter.uIGameController.SettingStart();
        yield return new WaitForEndOfFrame();
        gameControllerCenter.generateNodeProperty.SettingStart();
        gameControllerCenter.checkReady.enabled = true;
    }
}
