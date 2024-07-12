using System;
using UnityEngine;

public class UseSystem : Singleton<UseSystem>
{
    private Action currentModeAction;
    private UsableInventoryItemSO currentUsable;
    private ItemUsable usable;

    private void Start() {
        InventorySlot.OnAnyItemUsed += InventorySlot_OnAnyItemUsed;
    }

    private void InventorySlot_OnAnyItemUsed(UsableInventoryItemSO itemUsableSO){
        if(currentUsable == itemUsableSO) return;

        currentUsable = itemUsableSO;
        usable = itemUsableSO.CreateUsableInstance();

        if(usable is ItemModeUsable itemModeUsable) {
            currentModeAction = itemModeUsable.ModeEnabled; 
        }
    }

    public UsableInventoryItemSO GetUsableItemSO() {
        return currentUsable;
    }

    private void Update() {
        currentModeAction?.Invoke();
    }
}
