using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_ActionBar : MonoBehaviour
{
    [Header("Properties")]
    public HeroSystem owningHero;
    public List<ActionConfig> actions;
    public int maxPages = 4;
    public int maxSlots = 40;
    public int slotsPerPage = 10;

    [Header("Prefabs")]
    public GameObject actionSlotPrefab;

    [Header("UI Bindings")]
    public UI_ActionBarPager actionBarPager;
    public UI_ActionSlot_Details actionSlotDetails;

    [Header("Events")]
    public UnityEvent_Bool onToggleActionBar;
    public UnityEvent onPageLeft;
    public UnityEvent onPageRight;

    int currentPage = 0;
    bool isToggled = false;
    List<UI_ActionSlot> actionSlots = new List<UI_ActionSlot>();
    Player rewiredPlayer = null;

    private void OnEnable()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
        
    }

    private void OnDisable()
    {
       
    }

    public void Start()
    {
        foreach (Transform child in transform)
        {
            var uiActionSlot = child.GetComponent<UI_ActionSlot>();
            if (uiActionSlot == null)
                continue;

            actionSlots.Add(uiActionSlot);
        }

        // TODO: for now, as a test, just add all the actions of the owning action system.
        actions = owningHero.GetComponent<ActionSystem>().actions;
        for (int i = 0; i < actions.Count; i++)
        {
            actionSlots[i].SetActionConfig(actions[i]);

            //if (i == 0)
            //{
            //    actionSlots[i].GetComponentInChildren<Button>().Select();
            //}
        }
    }

    public void Update()
    {
        if (rewiredPlayer == null)
            return;

        if (rewiredPlayer.GetButtonDown("action_toggle_actionbar"))
        {
            isToggled = !isToggled;

            UpdateControllerMaps();

            onToggleActionBar.Invoke(isToggled);
        }

        if (rewiredPlayer.GetButtonDown("action_page_left"))
        {
            onPageLeft.Invoke();
        }
        else if (rewiredPlayer.GetButtonDown("action_page_right"))
        {
            onPageRight.Invoke();
        }
    }

    #region Events
    public void OnPageLeft()
    {
        SetPage(--currentPage);
    }

    public void OnPageRight()
    {
        SetPage(++currentPage);
    }

    public void OnActionSlotSelected(BaseEventData sender)
    {
        var uiActionSlot = sender.selectedObject.GetComponent<UI_ActionSlot>();

        if (!uiActionSlot.actionConfig)
        {
            actionSlotDetails.gameObject.SetActive(false);
            return;
        }

        actionSlotDetails.SetActionSlot(uiActionSlot);
        actionSlotDetails.gameObject.SetActive(true);
    }

    public void OnToggleActionBar(bool toggled)
    {
        var actionBarAnim = GetComponent<Animation>();

        if (toggled)
        {
            actionBarAnim.Play("UI_ActionBar_Show");
            actionBarPager.GetComponent<Animation>().Play("UI_ActionBar_Pager_Show");
            actionSlots[0].GetComponentInChildren<Button>().Select();
        }
        else
        {
            actionBarAnim.Play("UI_ActionBar_Hide");
            actionSlotDetails.gameObject.SetActive(false);
            actionBarPager.GetComponent<Animation>().Play("UI_ActionBar_Pager_Hide");
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
    }
    #endregion

    public void SetPage(int page)
    {
        if (currentPage >= maxPages) currentPage = 0;
        if (currentPage < 0) currentPage = maxPages - 1;

        actionBarPager.pageNumberText.text = string.Format("{0}/{1}", currentPage + 1, maxSlots / slotsPerPage);
    }

    private void UpdateControllerMaps()
    {
        if (rewiredPlayer != null)
        {
            if (isToggled)
            {
                rewiredPlayer.controllers.maps.SetMapsEnabled(true, "UI");
                rewiredPlayer.controllers.maps.SetMapsEnabled(false, "Exploration");
                rewiredPlayer.controllers.maps.SetMapsEnabled(true, "ActionBar");
            }
            else
            {
                rewiredPlayer.controllers.maps.SetMapsEnabled(false, "UI");
                rewiredPlayer.controllers.maps.SetMapsEnabled(true, "Exploration");
                rewiredPlayer.controllers.maps.SetMapsEnabled(false, "ActionBar");
            }
        }
    }

}
