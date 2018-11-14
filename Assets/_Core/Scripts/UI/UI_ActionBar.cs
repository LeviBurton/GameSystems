using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionBar : MonoBehaviour
{
    [Header("Properties")]
    public HeroSystem owningHero;
    public List<ActionConfig> actions;

    [Header("Prefabs")]
    public GameObject actionSlotPrefab;

    List<UI_ActionSlot> actionSlots = new List<UI_ActionSlot>();

    public void Start()
    {
        foreach (Transform child in transform)
        {
            actionSlots.Add(child.GetComponent<UI_ActionSlot>());
        }

        // TODO: for now, as a test, just add all the actions of the owning action system.
        actions = owningHero.GetComponent<ActionSystem>().actions;
        for (int i = 0; i < actions.Count; i++)
        {
            actionSlots[i].SetActionConfig(actions[i]);
            if (i == 0)
            {
                actionSlots[i].GetComponentInChildren<Button>().Select();
            }
        }
    }
}
