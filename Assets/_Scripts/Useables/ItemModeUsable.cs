using UnityEngine;

public abstract class ItemModeUsable : ItemUsable
{
    protected bool modeIsEnabled;

    public override void OnClick() {
        modeIsEnabled = true;
        DefineOnClickUsable();
    }
    public bool IsModeEnabled() => modeIsEnabled;

    protected abstract void DefineOnClickUsable();
    public abstract void DefineModeEnabled();
    public void ModeEnabled(){
        DefineModeEnabled();
    }
}
