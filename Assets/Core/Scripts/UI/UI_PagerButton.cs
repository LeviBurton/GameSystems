using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PagerButton : MonoBehaviour
{
    public void PlayAnimationClip(string clipName)
    {
        GetComponent<Animation>().Play(clipName);
    }
}
