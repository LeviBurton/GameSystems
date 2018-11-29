using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ActionSlot_Details : MonoBehaviour
{
    public UI_ActionSlot actionSlot;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;

    public void SetActionSlot(UI_ActionSlot actionSlot)
    {
        this.actionSlot = actionSlot;
        Refresh();
    }

    public void Refresh()
    {
        if (actionSlot == null)
            return;

        nameText.text = actionSlot.actionConfig.actionName;
        typeText.text = actionSlot.actionConfig.actionType;
        descriptionText.text = actionSlot.actionConfig.Description;
    }
}
