using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemModeUsable : ItemUsable
{
    protected bool modeIsEnabled;
    protected override void Start() {
        ModeUseableSystem.Instance.OnNewModeSelected += ModeUseableSystem_OnNewModeSelected;
    }

    private void ModeUseableSystem_OnNewModeSelected(UsableInventoryItemSO So){
        ItemModeUsable currentMode = ModeUseableSystem.Instance.GetCurrentlySelectedModeUsable();

        if(currentMode == null || currentMode.GetType() != GetType()) {
            modeIsEnabled = false;
            return;
        }

        OnModeEnabled(So);
        modeIsEnabled = true;
    }

    protected override void OnClick(UsableInventoryItemSO so) {
    }

    protected abstract void OnModeEnabled(UsableInventoryItemSO so);
    public abstract void DuringModeEnabled();

    private void Update() {
        if(!modeIsEnabled) return;
        DuringModeEnabled();
    }
}
