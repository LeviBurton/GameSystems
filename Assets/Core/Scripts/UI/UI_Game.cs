using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// The main UI for the game.  Things like the action bar, etc. will be here.
public class UI_Game : MonoBehaviour
{
    [Header("UI Bindings")]
    public UI_ActionBar actionBar;
    public UI_ActionBarPager actionBarPager;

    Player rewiredPlayer = null;

    void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

}
