using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DoorRuntime
{
    public string saveGameId;
    public bool isOpen;
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InteractableSystem))]
public class DoorSystem : MonoBehaviour
{
    [Header("Properties")]
    public bool isOpen = false;         // TODO: not sure we need this since the animator state tracks it now.

    [Header("Events")]
    public UnityEvent onDoorOpenFinished;
    public UnityEvent onDoorCloseFinished;

    Animator animator = null;
    int id_IsOpen = Animator.StringToHash("IsOpen");

    // TODO: start getting components in Awake since if we use Start(), the will be null
    // when we try to access them in OnSceneLoaded after loading a game.
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
   
    // TODO: get rid of using events like this and switch to either UnityEvents,
    // or interfaces.
    void OnEnable()
    {
        var globalEvents = FindObjectOfType<GlobalEvents>();
        if (globalEvents != null)
        {
            globalEvents.onSave += OnSave;
        }
    }

    void OnDisable()
    {
        var globalEvents = FindObjectOfType<GlobalEvents>();
        if (globalEvents != null)
        {
            globalEvents.onSave -= OnSave;
        }
    }

    #region Public callable methods
    // TODO: consider just using a Set(bool open) rather than explicit Open()/Close().
    // (I like explicit open/close better, though)
    public void SetDoor(bool isOpen)
    {
        this.isOpen = isOpen;
        animator.SetBool(id_IsOpen, isOpen);
    }

    public void Open()
    {
        SetDoor(true);
    }

    public void Close()
    {
        SetDoor(false);
    }
    #endregion

    #region Interactable event handling
    public void OnInteract(InteractableEvent ev)
    {
        switch (ev.interactType)
        {
            case EInteractType.Open:
                if (isOpen == true)
                    return;

                Open();
                break;

            case EInteractType.Close:
                if (isOpen == false)
                    return;

                Close();
                break;

            case EInteractType.Toggle:
                if (isOpen)
                    Close();
                else
                    Open();
                break;

            default:
                break;
        }
    }
    #endregion

    #region Test Animation Event Handling
    // These are just test handling of the events we send.
    public void DoorOpenHandler()
    {
        Debug.Log("Door Opened");
    }

    public void DoorCloseHandler()
    {
        Debug.Log("Door Closed");
    }
    #endregion

    #region Animation Events
    void OnAnimationDoorOpenFinished()
    {
        onDoorOpenFinished.Invoke();
    }

    void OnAnimationDoorCloseFinished()
    {
        onDoorCloseFinished.Invoke();
    }
    #endregion

    #region Load/Save

    public void OnSave(MySaveGame saveGame)
    {
        Debug.Log("DoorSystem OnSave: " + GetComponent<SaveGameIdSystem>().SaveGameId);

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var runtime = new DoorRuntime();
        runtime.saveGameId = saveGameIdSystem.SaveGameId;
        runtime.isOpen = isOpen;
        saveGame.doorRuntimes.Add(runtime);
    }

    public void OnLoad(MySaveGame saveGame)
    {
        Debug.Log("DoorSystem OnLoad");

        var saveGameIdSystem = this.GetComponent<SaveGameIdSystem>();
        var runtime = saveGame.doorRuntimes.SingleOrDefault(x => x.saveGameId == saveGameIdSystem.SaveGameId);
        if (runtime == null)
        {
            throw new UnityException(string.Format("No HeroRuntime for SaveGameId {0}", saveGameIdSystem.SaveGameId));
        }

        SetDoor(runtime.isOpen);
    }
    #endregion
}
