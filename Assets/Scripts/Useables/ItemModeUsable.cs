public abstract class ItemModeUsable : ItemUsable
{
    protected bool modeIsEnabled;

    protected override void DefineOnClick()
    {
        modeIsEnabled = true;
        DefineOnClickUsable();
    }

    protected abstract void DefineOnClickUsable();
    public abstract void ModeEnabled();
}
