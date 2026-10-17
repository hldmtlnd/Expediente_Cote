using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public Image[] slotImages;
    public Sprite emptySlotSprite;

    void Update()
    {
        RefreshSlots();
    }

    void RefreshSlots()
    {
        if (inventory == null || slotImages == null) return;

        for (int i = 0; i < slotImages.Length; i++)
        {
            if (slotImages[i] == null) continue;

            if (i < inventory.slots.Count && inventory.slots[i] != null)
            {
                slotImages[i].color = Color.white;
            }
            else
            {
                slotImages[i].sprite = emptySlotSprite;
                slotImages[i].color = new Color(1, 1, 1, 0.3f);
            }
        }
    }

    public void OnSlotClicked(int index)
    {
        // Select or use item
    }
}
