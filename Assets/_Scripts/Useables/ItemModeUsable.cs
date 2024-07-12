using UnityEngine;

public abstract class ItemModeUsable : ItemUsable
{
    protected bool modeIsEnabled;

    protected override void OnClick(UsableInventoryItemSO so) {
        if(!corrospondingUsableItemSOs.Contains(so)) { 
            modeIsEnabled = false;
            return;
        };

        modeIsEnabled = true;
        DefineOnModeEnabled(so);
    }
    public bool IsModeEnabled() => modeIsEnabled;

    protected abstract void DefineOnModeEnabled(UsableInventoryItemSO so);
    public abstract void DefineDuringModeEnabled();
    public void DuringModeEnabled(){
        DefineDuringModeEnabled();
    }

    private void Update() {
        if(!modeIsEnabled) return;
        DuringModeEnabled();
    }
}
