using System;
using UnityEngine;

public class UseSystem : MonoBehaviour
{
    private UsableInventoryItemSO currentUsable;
    private Action currentModeAction;
    private ItemUsable usable;
    private bool isModeUsable;

    private void Start() {
        InventorySlot.OnAnyItemUsed += InventorySlot_OnAnyItemUsed;
    }

    private void InventorySlot_OnAnyItemUsed(UsableInventoryItemSO itemUsableSO){
        if(currentUsable == itemUsableSO) return;
        Debug.Log("USED");

        currentUsable = itemUsableSO;
        usable = itemUsableSO.CreateUsableInstance();
        isModeUsable = usable is ItemModeUsable;

        if(isModeUsable) {
            currentModeAction = (usable as ItemModeUsable).ModeEnabled; 
        }
    }

    private void Update() {
        currentModeAction?.Invoke();
    }
}
