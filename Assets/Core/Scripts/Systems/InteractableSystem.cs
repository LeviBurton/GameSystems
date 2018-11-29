using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableSystem : MonoBehaviour
{
    [Header("Properties")]
    public bool checkForInteractables;      // only relevant to the player.  
    public float checkDistance = 0.0f;
    public LayerMask layersToCheck;
    public InteractableSystem currentInteractable = null;

    [Header("Events")]
    public UnityEvent_Interact onInteract;

    void Start()
    {
        SetOutlinesEnabled(false);
    }

    void Update()
    {
        if (checkForInteractables)
        {
            RaycastHit hit;
            Vector3 origin = transform.position + (Vector3.up * 1.5f);

            var didHit = Physics.Raycast(origin, transform.forward, out hit, checkDistance, layersToCheck);

            if (didHit)
            {
                var tmp = hit.collider.GetComponent<InteractableSystem>();
                if (tmp != currentInteractable)
                {
                    currentInteractable = tmp;

                    if (currentInteractable != null)
                        currentInteractable.SetOutlinesEnabled(true);
                }
            }
            else
            {
                if (currentInteractable != null)
                    currentInteractable.SetOutlinesEnabled(false);

                currentInteractable = null;
            }
        }
    }

    public void Interact(EInteractType interactType)
    {
        var ev = new InteractableEvent(this, currentInteractable, interactType);
        currentInteractable.onInteract.Invoke(ev);
    }

    public bool InteractableInSight()
    {
        return currentInteractable != null;
    }

    public void SetOutlinesEnabled(bool enabled)
    {
        foreach (var outliner in GetComponentsInChildren<cakeslice.Outline>())
        {
            outliner.enabled = enabled;
        }
    }
}
