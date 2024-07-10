using System;
using UnityEngine;

public class UseSystem : MonoBehaviour
{
    private UsableInventoryItemSO currentUsable;
    private Action currentModeAction;
    private bool isModeUsable;

    private void Start() {
        InventorySlot.OnAnyItemUsed += InventorySlot_OnAnyItemUsed;
    }

    private void InventorySlot_OnAnyItemUsed(UsableInventoryItemSO itemUsableSO){
        if(currentUsable == itemUsableSO) return;

        currentUsable = itemUsableSO;
        isModeUsable = itemUsableSO.CreateUsableInstance() is ItemModeUsable;
        if(isModeUsable)
            currentModeAction = (itemUsableSO.CreateUsableInstance() as ItemModeUsable).ModeEnabled; 

    }

    private void Update() {
        currentModeAction?.Invoke();
    }
}
