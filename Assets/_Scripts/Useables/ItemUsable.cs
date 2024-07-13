using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUsable : MonoBehaviour
{
    protected virtual void Start() {
        InventorySlot.OnAnyItemUsed += InventorySlot_OnAnyItemUsed;
    }

    private void InventorySlot_OnAnyItemUsed(UsableInventoryItemSO usableInvItemSO){
        OnClick(usableInvItemSO);
    }

    protected abstract void OnClick(UsableInventoryItemSO itemSO);
}
