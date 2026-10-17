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
        for (int i = 0; i < slotImages.Length && i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i] != null)
            {
                // Set icon based on item type
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
