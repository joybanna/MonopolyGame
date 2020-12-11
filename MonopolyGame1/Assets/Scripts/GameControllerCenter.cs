using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerCenter : MonoBehaviour
{
    [HideInInspector] public MasterClientSettingController masterClientSettingController;
    [HideInInspector] public UIGameController uIGameController;
    [HideInInspector] public Route route;
    [HideInInspector] public TurnManagement turnManagement;
    [HideInInspector] public PrepareGame prepareGame;
    [HideInInspector] public RPCController rPCController;
    [HideInInspector] public CheckReady checkReady;
    [HideInInspector] public RandomSteps randomSteps;
    [HideInInspector] public GenerateNodeProperty generateNodeProperty;
    private void Start()
    {
        SettingAllScripts();
        StartCoroutine(prepareGame.SettingStart());
    }
    private void SettingAllScripts()
    {
        masterClientSettingController = FindObjectOfType<MasterClientSettingController>();
        masterClientSettingController.gameControllerCenter = this;

        uIGameController = FindObjectOfType<UIGameController>();
        uIGameController.gameControllerCenter = this;

        route = FindObjectOfType<Route>();
        route.gameControllerCenter = this;

        turnManagement = FindObjectOfType<TurnManagement>();
        turnManagement.gameControllerCenter = this;

        prepareGame = FindObjectOfType<PrepareGame>();
        prepareGame.gameControllerCenter = this;

        rPCController = FindObjectOfType<RPCController>();
        rPCController.gameControllerCenter = this;

        checkReady = FindObjectOfType<CheckReady>();
        checkReady.gameControllerCenter = this;

        randomSteps = FindObjectOfType<RandomSteps>();
        randomSteps.gameControllerCenter = this;

        generateNodeProperty = FindObjectOfType<GenerateNodeProperty>();
        generateNodeProperty.gameControllerCenter = this;
    }
}
