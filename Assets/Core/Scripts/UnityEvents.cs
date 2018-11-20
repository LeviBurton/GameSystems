using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class UnityEvent_Vector3 : UnityEvent<Vector3> { }
[Serializable] public class UnityEvent_Float : UnityEvent<float> { }
[Serializable] public class UnityEvent_Bool : UnityEvent<bool> { }
[Serializable] public class UnityEvent_String : UnityEvent<string> { }
[Serializable] public class UnityEvent_WeaponEquipped : UnityEvent<WeaponConfig> { }


