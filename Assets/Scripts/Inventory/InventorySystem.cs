using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class InventorySystem : Singleton<InventorySystem>
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    private Dictionary<InventorySlot, InventoryItem> itemSlotDictionary;
    [SerializeField] private int stackLimit;
    private int originalStackLimit;

    public override void Awake() {
        base.Awake();
        originalStackLimit = stackLimit;
        itemSlotDictionary = new Dictionary<InventorySlot, InventoryItem>();
        foreach(InventorySlot inventorySlot in inventorySlots) {
            itemSlotDictionary.Add(inventorySlot, null);
        }
    }

    public bool TryAddItem(InventoryItemSO inventoryItemSO, int amount) {
        stackLimit = inventoryItemSO.isStackable ? 1 : stackLimit; 
        if (!CanAddItem(inventoryItemSO, amount)) {
            stackLimit = originalStackLimit;
            return false;
        }

        InventoryItem item = itemSlotDictionary.Values.ToList().Find(item => item != null &&  item.GetItemSO() == inventoryItemSO && item.GetAmount() < stackLimit);

        if(item != null) {
            int newItemAmount = item.GetAmount() + amount;
            item.SetAmount(newItemAmount);
            if(item.GetAmount() > stackLimit) {
                int leftOver = newItemAmount - stackLimit;
                item.SetAmount(stackLimit);
                TryAddItem(item.GetItemSO(), leftOver);
            }
            InventorySlot slotToUpdate = itemSlotDictionary.First(itemSlotPair => itemSlotPair.Value == item).Key;
            slotToUpdate.UpdateSlot();
        } else {
            InventorySlot inventorySlot = GetNextEmptySlot();
            InventoryItem inventoryItem = new(inventoryItemSO, amount);
            itemSlotDictionary[inventorySlot] = inventoryItem;

            if(amount > stackLimit) {
                int leftOver = amount - stackLimit;
                inventoryItem.SetAmount(stackLimit);
                TryAddItem(inventoryItem.GetItemSO(), leftOver);
            }

            inventorySlot.UpdateSlot();
        }

        stackLimit = originalStackLimit;
        return true;
    }

    private bool CanAddItem(InventoryItemSO inventoryItemSO, int amount = 1) {
        int avaiableSpace = 0;
        foreach(var item in itemSlotDictionary.Values.ToList()) {
            if (item == null) continue;
            if (item.GetItemSO() != inventoryItemSO) continue;
            if (item.GetAmount() >= stackLimit) continue;
            avaiableSpace += stackLimit - item.GetAmount();
        }


        foreach(InventorySlot slot in itemSlotDictionary.Keys) {
            if(itemSlotDictionary[slot] == null) {
                avaiableSpace += stackLimit;
            }
        }

        return avaiableSpace >= amount;
    }

    public bool TryAddItem(InventoryItemSO inventoryItemSO) => TryAddItem(inventoryItemSO, 1);



    public bool Contains(InventoryItemSO inventoryItemSO) => 
        itemSlotDictionary.Any(itemSlotPair => 
            itemSlotPair.Value != null && 
            itemSlotPair.Value.GetItemSO() == inventoryItemSO
        );

    private InventorySlot GetNextEmptySlot() {
        foreach(var slot in itemSlotDictionary.Keys) {
            if(itemSlotDictionary[slot] == null) {
                return slot;
            }
        }
        return null;
    }

    public InventoryItem GetItemOnSlot(InventorySlot inventorySlot) {
        return itemSlotDictionary[inventorySlot];
    }
}

[Serializable]
public class InventoryItem {
    private InventoryItemSO itemSO;
    private int amount;

    public InventoryItem(InventoryItemSO itemSO, int amount) {
        this.itemSO = itemSO;
        this.amount = amount;
    }

    public int GetAmount() => amount;
    public void SetAmount(int newAmount) => amount = newAmount; 
    public InventoryItemSO GetItemSO() => itemSO;
}