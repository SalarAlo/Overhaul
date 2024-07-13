using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemUsable : MonoBehaviour
{

    [SerializeField] protected List<UsableInventoryItemSO> corrospondingUsableItemSOs;
    protected abstract void OnClick(UsableInventoryItemSO itemSO);
    protected virtual void Start() {
        InventorySlot.OnAnyItemUsed += InventorySlot_OnAnyItemUsed;
    }

    private void InventorySlot_OnAnyItemUsed(UsableInventoryItemSO sO){
        OnClick(sO);
    }
}
