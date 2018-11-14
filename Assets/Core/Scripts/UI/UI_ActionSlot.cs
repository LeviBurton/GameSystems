using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionSlot : MonoBehaviour
{
    [Header("Properties")]
    public ActionConfig actionConfig;

    [Header("UI Bindings")]
    public Image iconImage;

    public void SetActionConfig(ActionConfig config)
    {
        actionConfig = config;
        iconImage.sprite = actionConfig.spriteIcon;
        iconImage.color = new Color(255, 255, 255, 255);
    }
}
