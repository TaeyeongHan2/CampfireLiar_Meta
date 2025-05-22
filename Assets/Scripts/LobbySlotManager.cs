using UnityEngine;
using System.Collections.Generic;

public class LobbySlotManager : MonoBehaviour
{
    public List<Transform> slots;

    public Transform GetSlot(int index)
    {
        if(index >= 0 && index < slots.Count)
            return slots[index];
        return null;
    }
}
