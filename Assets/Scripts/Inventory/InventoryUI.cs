using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlotUI[] uislots;
    public ItemTooltipUI TooltipUI;

    public void UpdateUI (ItemSlot[] items)
    {
        for(int i = 0; i < uislots.Length; i++)
        {
            uislots[i].SetItemSlot(items[i]);
        }
    }
}
