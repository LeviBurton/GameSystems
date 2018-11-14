using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class UI_Main : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadMenu;
    public GameObject saveMenu;
    public GameObject newMenu;
    public GameObject continueMenu;

    Player rewiredPlayer = null;

    void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    private void OnDestroy()
    {
        rewiredPlayer = null;
    }

    void OnEnable()
    {
        // main menu is active by default
        mainMenu.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        // update controller maps -- disable UI map and enable Exploration map.
        if (rewiredPlayer != null)
        {
            rewiredPlayer.controllers.maps.SetMapsEnabled(false, "UI");
            rewiredPlayer.controllers.maps.SetMapsEnabled(true, "Exploration");
        }

        mainMenu.gameObject.SetActive(false);
        loadMenu.gameObject.SetActive(false);
        saveMenu.gameObject.SetActive(false);
    }
}
