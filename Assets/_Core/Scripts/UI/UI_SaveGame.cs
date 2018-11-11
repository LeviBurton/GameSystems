using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

[System.Serializable]
public class UI_SaveGameEvent : UnityEvent<string> { }

public class UI_SaveGame : MonoBehaviour
{
    public UI_SaveGameEvent onCancel;
    public UI_SaveGameEvent onSubmit;
    public UI_SaveGameEvent onDelete;

    public GameObject uiSaveSlotScrollViewContent;
    public GameObject uiSaveSlotPrefab;
    public Text uiInputSlotNameText;
    public string selectedSlotName;

    List<GameObject> uiSaveSlots = new List<GameObject>();
    SaveGameSystem saveGameSystem;

    void Awake()
    {
        saveGameSystem = FindObjectOfType<SaveGameSystem>();
    }

    void OnEnable()
    {
        RefreshSlots();
    }

    void OnDisable()
    {
    }

    public void OnCancel()
    {
        onCancel.Invoke(selectedSlotName);
    }

    public void OnSubmit()
    {
        onSubmit.Invoke(selectedSlotName);
    }

    public void RefreshSlots()
    {
        Debug.Log("RefreshSlots");

        var saveSlots = saveGameSystem.GetSaveSlots();

        foreach (var slot in uiSaveSlots)
        {
            DestroyImmediate(slot);
        }

        uiSaveSlots.Clear();

        for (int i = 0; i < saveSlots.Count; i++)
        {
            var saveSlot = saveSlots[i];
            var slotGameObject = Instantiate(uiSaveSlotPrefab, uiSaveSlotScrollViewContent.transform);
            var uiSaveSlot = slotGameObject.GetComponent<UI_SaveSlot>();

            uiSaveSlot.uiSaveGame = this;
            uiSaveSlot.slotName.text = saveSlot.slotName;
            uiSaveSlot.slotNum.text = string.Format("{0}", saveSlot.slotNum);
            uiSaveSlot.slotDate.text = saveSlot.slotDate.ToString("MM/dd/yyyy hh:mm tt");

            uiSaveSlots.Add(uiSaveSlot.gameObject);
        }

        for (int i = 0; i < uiSaveSlots.Count; i++)
        {
            var uiSaveSlot = uiSaveSlots[i];

            var selectable = uiSaveSlot.GetComponentInChildren<Selectable>();

            if (i == 0)
            {
                var navigation = new Navigation
                {
                    mode = Navigation.Mode.Explicit
                };

                var previousSelectable = uiSaveSlots[uiSaveSlots.Count - 1].GetComponentInChildren<Selectable>();
                var nextSelectable = uiSaveSlots[1].GetComponentInChildren<Selectable>();

                navigation.selectOnUp = previousSelectable;
                navigation.selectOnDown = nextSelectable;
                selectable.navigation = navigation;
                selectable.Select();
            }

            if (i == uiSaveSlots.Count - 1)
            {
                var navigation = new Navigation
                {
                    mode = Navigation.Mode.Explicit
                };

                var previousSelectable = uiSaveSlots[i - 1].GetComponentInChildren<Selectable>();
                var nextSelectable = uiSaveSlots[0].GetComponentInChildren<Selectable>();

                navigation.selectOnUp = previousSelectable;
                navigation.selectOnDown = nextSelectable;
                selectable.navigation = navigation;
            }
        }
    }
}
