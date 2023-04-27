using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemData[] starterItems;
    [SerializeField] private int inventorySize;
    private ItemSlot[] itemSlots;

    public InventoryUI UI;

    public static Inventory Instance;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Initialize the Item Slots
        itemSlots = new ItemSlot[inventorySize];

        for(int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new ItemSlot();
        }

        // Add the starter items
        for(int i = 0; i < starterItems.Length; i++)
        {
            AddItem(starterItems[i]);
        }
    }

    // Adds Item to the Inventory
    public void AddItem (ItemData item)
    {
        ItemSlot slot = FindAvailableItemSlot(item);

        if(slot != null)
        {
            slot.Quantity++;
            //Update UI
            UI.UpdateUI(itemSlots);
            return;
        }

        slot = GetEmptySlot();
        
        if(slot != null)
        {
            slot.Item = item;
            slot.Quantity = 1;
        }
        else
        {
            Debug.Log("Inventory Full");
            return;
        }
        //Update UI
        UI.UpdateUI(itemSlots);
        
        
    }

    // Removes the requested Item from the Inventory
    public void RemoveItem (ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item)
            {
                RemoveItem(itemSlots[i]);
                return;
            }
        }
    }

    // Removes the requested Item from requested slot
    public void RemoveItem (ItemSlot slot)
    {
        if(slot == null)
        {
            Debug.Log("can't remove item from inventory");
            return;
        }

        slot.Quantity--;
        if(slot.Quantity <= 0)
        {
            slot.Item = null;
            slot.Quantity = 0;
        }

        //Update UI
        UI.UpdateUI(itemSlots);
    }

    // returns in item slot that the requested item can fit into
    ItemSlot FindAvailableItemSlot (ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item && itemSlots[i].Quantity < item.MaxStackSize)
                return itemSlots[i];
        }
        return null;
    }

    // Returns an item slot without an item in it
    ItemSlot GetEmptySlot ()
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
                return itemSlots[i];
        }

        return null;
    }

    // Called when we click on an item slot
    public void UseItem (ItemSlot slot)
    {
        if(slot.Item is MeleeWeaponItemData || slot.Item is RangedWeaponItemData)
        {
            Player.Instance.EquipCtrl.Equip(slot.Item);
        }
        else if(slot.Item is FoodItemData)
        {
            FoodItemData food = slot.Item as FoodItemData;
            if(Player.Instance.CurHp >= Player.Instance.MaxHp)
                return;
            Player.Instance.Heal(food.HealthToGive);
            RemoveItem(slot);
        }
            

    }

    // Do we have the requested item?
    public bool HasItem (ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item && itemSlots[i].Quantity > 0)
                return true;
        }
        return false;
    }
}
