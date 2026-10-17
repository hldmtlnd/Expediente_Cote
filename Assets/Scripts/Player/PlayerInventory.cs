using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int maxSlots = 5;
    public List<InventoryItem> slots = new List<InventoryItem>();

    public GameObject revolverPrefab;
    public GameObject advancedCameraPrefab;

    void Start()
    {
        for (int i = 0; i < maxSlots; i++)
            slots.Add(null);
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                return true;
            }
        }
        return false;
    }

    public void SwapSlots(int from, int to)
    {
        if (from < 0 || to < 0 || from >= slots.Count || to >= slots.Count) return;
        var temp = slots[from];
        slots[from] = slots[to];
        slots[to] = temp;
    }

    public bool HasRevolver() => slots.Exists(s => s != null && s.itemType == ItemType.Revolver);
    public bool HasAdvancedCamera() => slots.Exists(s => s != null && s.itemType == ItemType.AdvancedCamera);
}

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public ItemType itemType;
    public GameObject prefab;
}

public enum ItemType
{
    Revolver,
    AdvancedCamera,
    Other
}
