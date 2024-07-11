using UnityEngine;

public abstract class ItemModeUsable : ItemUsable
{
    protected bool modeIsEnabled;

    public override void OnClick() {
        modeIsEnabled = true;
        DefineOnModeEnabled();
    }
    public bool IsModeEnabled() => modeIsEnabled;

    protected abstract void DefineOnModeEnabled();
    public abstract void DefineDuringModeEnabled();
    public void ModeEnabled(){
        DefineDuringModeEnabled();
    }
}
