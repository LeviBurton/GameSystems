﻿using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UI_Main uiMain = null;
    public UI_Game uiGame = null;

    Player rewiredPlayer = null;

    private void OnEnable()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }
	
	void Update ()
    {
        if (rewiredPlayer == null)
            return;

        if (rewiredPlayer.GetButtonDown("action_mainmenu"))
        {
            ToggleMainMenu();
        }
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
