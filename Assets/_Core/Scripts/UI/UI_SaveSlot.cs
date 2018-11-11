using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SaveSlot : MonoBehaviour, ICancelHandler
{
    public UI_SaveGame uiSaveGame;
    public TextMeshProUGUI slotName;
    public TextMeshProUGUI slotDate;
    public TextMeshProUGUI slotNum;

    public void OnSelect()
    {
        uiSaveGame.selectedSlotName = slotName.text;
    }

    public void OnSubmit()
    {
        uiSaveGame.OnSubmit();
    }

    public void OnCancel(BaseEventData eventData)
    {
        uiSaveGame.OnCancel();
    }
}
