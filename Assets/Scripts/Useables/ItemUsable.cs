public abstract class ItemUsable
{
    protected abstract void DefineOnClick();
    public void OnClick() {
        DefineOnClick();
    }
}
