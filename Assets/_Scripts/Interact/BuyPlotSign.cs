using UnityEngine;

public class BuyPlotSign : Interactable
{
    private Plot relativePlot;

    public void AssignPlot(Plot plot) => relativePlot = plot;

    public override void Interact() {
        TileManager.Instance.UnlockPlot(relativePlot);
    }

    public override void OnHover(){

    }
}
