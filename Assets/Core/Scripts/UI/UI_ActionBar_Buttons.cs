using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionBar_Buttons : MonoBehaviour
{
    public void OnToggleActionBar(bool toggled)
    {
        var animation = GetComponent<Animation>();

        if (toggled)
        {
            animation.Play("UI_ActionBar_Buttons_Show");
        }
        else
        {
            animation.Play("UI_ActionBar_Buttons_Hide");
        }
    }
}
