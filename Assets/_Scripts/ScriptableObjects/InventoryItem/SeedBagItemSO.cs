using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/new Seed Plant")]
public class SeedBagItemSO : UsableInventoryItemSO
{
    public List<GameObject> stages;
    public InventoryItemSO outcomePlant;
    [Range(0, 20)] public int baseGrowthValue;
    [Range(0, 20)] public int basePesticideProtection;
    [Range(0, 100)] public int basePesticideSpawnRate;
    [Range(0, 100)] public int seedDropRate;
    [Range(1, 20)] public int baseWaterDemand;
    [Range(1, 20)] public int baseSunDemand;
    [Range(1, 20)] public int baseQuantity;
    public int randomSeedDropMax;

}
