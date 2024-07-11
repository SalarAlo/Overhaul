using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "")]
public class SeedBagItemSO : UsableInventoryItemSO
{
    public List<GameObject> stages;
    public int baseGrowthValue;
    public int basePesticideProtection;
    [Range(0, 100)]
    public int basePesticideSpawnRate;
    public int baseWaterDemand;
    public int baseSunDemand;
    public int baseQuantity;
}
