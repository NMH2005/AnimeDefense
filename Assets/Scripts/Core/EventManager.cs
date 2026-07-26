using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<int> OnGoldChanged;

    public static void RaiseGoldChanged(int amount)
    {
        OnGoldChanged.Invoke(amount);
    }
}
