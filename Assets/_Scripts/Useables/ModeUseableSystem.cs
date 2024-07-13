using System;
using System.Collections.Generic;
using UnityEngine;

public class ModeUseableSystem : Singleton<ModeUseableSystem>
{
    [SerializeField] private List<ModeSoAssociation> modeSoAssociations;
    [SerializeField] private ItemModeUsable currentUsable;
    public Action<UsableInventoryItemSO> OnNewModeSelected;
    private UsableInventoryItemSO usableInventoryItemSO;

    private void Start() {
        InventorySlot.OnAnySlotClicked += InventorySlot_OnAnySlotClicked;
        InventorySystem.Instance.OnAnyItemRemoved += InventorySystem_OnAnyItemRemoved;
    }

    private void InventorySystem_OnAnyItemRemoved(InventoryItemSO sO) {
        if (usableInventoryItemSO == sO as UsableInventoryItemSO) {
            SetModeToNull();
        }
    }

    private void SetModeToNull() {
        currentUsable = null;
        OnNewModeSelected?.Invoke(null);
    }

    private void InventorySlot_OnAnySlotClicked(InventorySlot itemSoUsed) {
        InventoryItemSO inventoryItemSO = InventorySystem.Instance.GetItemOnSlot(itemSoUsed)?.GetItemSO();
        if(inventoryItemSO == null) SetModeToNull();
        else TryToChangeMode(inventoryItemSO);
    }

    private bool TryToChangeMode(InventoryItemSO itemSoUsed) {
        ItemModeUsable modeUsable = null;
        usableInventoryItemSO = null;

        foreach(ModeSoAssociation association in modeSoAssociations) {
            if(association.so.Contains(itemSoUsed as UsableInventoryItemSO)) {
                modeUsable = association.modeUsable;
                usableInventoryItemSO = association.so.Find(so => so == itemSoUsed);
            }
        }

        if(modeUsable == null) { 
            Debug.LogError("No item found with this association");
            return false;
        }

        currentUsable = modeUsable;
        OnNewModeSelected?.Invoke(usableInventoryItemSO);
        return true;
    }

    public ItemModeUsable GetCurrentlySelectedModeUsable() => currentUsable;
}

[Serializable]
public class ModeSoAssociation {
    public ItemModeUsable modeUsable;
    public List<UsableInventoryItemSO> so;
}
