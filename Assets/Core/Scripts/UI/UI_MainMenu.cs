using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    StartSelection startSelection;

    void Awake()
    {
        startSelection = GetComponent<StartSelection>();
    }

    void OnEnable()
    {
        startSelection.selectable.Select();
    }

    void OnDisable()
    {
    }
}
