using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class InventorySystem : Singleton<InventorySystem>
{
    [Serializable]
    public class ItemAdder {
        public InventoryItemSO item;
        public int amount;
    }

    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private int stackLimit;
    [SerializeField] private List<ItemAdder> inventoryItemSOs;
    private Dictionary<InventorySlot, InventoryItem> itemSlotDictionary;
    private int originalStackLimit;
    public Action<InventoryItemSO> OnAnyItemRemoved;
    [SerializeField] private InventorySlot selectedSlot;

    public override void Awake() {
        base.Awake();
        originalStackLimit = stackLimit;
        itemSlotDictionary = new Dictionary<InventorySlot, InventoryItem>();
        foreach(InventorySlot inventorySlot in inventorySlots) {
            itemSlotDictionary.Add(inventorySlot, null);
        }
        InventorySlot.OnAnySlotClicked += InventorySlot_OnAnySlotClicked;
    }

    private void InventorySlot_OnAnySlotClicked(InventorySlot slot){
        selectedSlot = slot;
    }

    public InventorySlot GetSelectedSlot() => selectedSlot;

    private IEnumerator Start() {
        yield return null;
        foreach(var keyValuePair in inventoryItemSOs){
            for(int i = 0; i < keyValuePair.amount; i++) {
                yield return new WaitForSeconds(.1f);
                TryAddItem(keyValuePair.item, 1);
            }
        }
    }

    public bool TryAddItem(InventoryItemSO inventoryItemSO, int amount = 1) {
        stackLimit = !inventoryItemSO.isStackable ? 1 : stackLimit; 

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

    public void RemoveItem(InventoryItemSO itemSO, int amount = 1, InventorySlot start = null) {
        bool IsSlotWithSO(InventorySlot slot) {
            return itemSlotDictionary[slot] != null && slot != start && itemSlotDictionary[slot].GetItemSO() == itemSO;
        }

        List<InventorySlot> slotsWithItem = inventorySlots.Where(IsSlotWithSO).ToList();

        if(start != null && itemSlotDictionary[start].GetItemSO() == itemSO)
            slotsWithItem.Prepend(start);

        if(slotsWithItem.Count == 0) return;
        int amountLeftToRemove = amount;


        foreach(var slot in slotsWithItem) {
            InventoryItem item = itemSlotDictionary[slot];
            int itemAmount = item.GetAmount();

            if (itemAmount > amountLeftToRemove) {
                item.SetAmount(itemAmount-amountLeftToRemove);
                slot.UpdateSlot();
                break;
            } else {
                itemSlotDictionary[slot] = null;
                amountLeftToRemove -= itemAmount;
            }

            slot.UpdateSlot();
        }

        slotsWithItem = inventorySlots.Where(slot => itemSlotDictionary[slot] != null && itemSlotDictionary[slot].GetItemSO() == itemSO).ToList();
        if (slotsWithItem.Count == 0) {
            OnAnyItemRemoved?.Invoke(itemSO);
        }
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