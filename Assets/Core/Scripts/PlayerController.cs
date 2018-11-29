using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// The PlayerController currently acts as a player input event dispatcher.
// It also allows global control over the main menu (so we can always exit, etc.)
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public UI_Main uiMain = null;
    public UI_Game uiGame = null;

    [Header("Events")]
    public UnityEvent_Vector3 onInputMove;
    public UnityEvent_Float onCameraRotate;
    public UnityEvent_Float onCameraZoom;

    Player rewiredPlayer = null;

    private void OnEnable()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (rewiredPlayer == null)
            return;

        if (rewiredPlayer.GetButtonDown("action_mainmenu"))
        {
            ToggleMainMenu();
        }

        // if we are in the "UI", then we don't want to process any input other than UI input.
        foreach (ControllerMap map in rewiredPlayer.controllers.maps.GetAllMapsInCategory("UI"))
        {
            if (map.enabled)
                return;
        }

        Vector3 inputMove = new Vector3(rewiredPlayer.GetAxis("move_side"), 0, rewiredPlayer.GetAxis("move_forward"));
        onInputMove.Invoke(inputMove);

        float inputCameraRotate = rewiredPlayer.GetAxis("camera_rotate");
        onCameraRotate.Invoke(inputCameraRotate);

        float inputCameraZoom = rewiredPlayer.GetAxis("camera_zoom");
        onCameraZoom.Invoke(inputCameraZoom);

       // Debug.LogFormat("{0} {1} {2}", inputMove, inputCameraRotate, inputCameraZoom);
    }

    void ToggleMainMenu()
    {
        uiMain.gameObject.SetActive(!uiMain.gameObject.activeSelf);

        if (uiMain.gameObject.activeSelf)
        {
            uiGame.gameObject.SetActive(false);
        }
        else
        {
            uiGame.gameObject.SetActive(true);
        }

        uiMain.GetComponent<StartSelection>().selectable.Select();

        // TODO: consider changing these in OnEnable/OnDisable
        rewiredPlayer.controllers.maps.SetMapsEnabled(uiMain.gameObject.activeSelf, "UI");
        rewiredPlayer.controllers.maps.SetMapsEnabled(!uiMain.gameObject.activeSelf, "Exploration");
    }
}
