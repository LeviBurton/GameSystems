using System;
using UnityEngine;
using UnityEngine.Events;

public enum EInteractType
{
    Open, Close, Toggle
};

public struct InteractableEvent
{
    public InteractableSystem sender;
    public InteractableSystem receiver;
    public EInteractType interactType;

    public InteractableEvent(InteractableSystem sender, InteractableSystem receiver, EInteractType interactType)
    {
        this.sender = sender;
        this.receiver = receiver;
        this.interactType = interactType;
    }
}

[Serializable] public class UnityEvent_Vector3 : UnityEvent<Vector3> { }
[Serializable] public class UnityEvent_Float : UnityEvent<float> { }
[Serializable] public class UnityEvent_Bool : UnityEvent<bool> { }
[Serializable] public class UnityEvent_String : UnityEvent<string> { }

[Serializable] public class UnityEvent_WeaponSystem_Equip : UnityEvent<WeaponSystem> { }
[Serializable] public class UnityEvent_WeaponSystem_UnEquip : UnityEvent<WeaponSystem> { }
[Serializable] public class UnityEvent_WeaponSystem_Use : UnityEvent<WeaponSystem, TargetableSystem> { }

[Serializable] public class UnityEvent_Interact : UnityEvent<InteractableEvent> { }




