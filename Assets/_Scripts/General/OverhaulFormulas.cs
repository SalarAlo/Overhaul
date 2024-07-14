using Unity.VisualScripting;
using UnityEngine;

public static class OverhaulFormulas 
{
    private const int AVERAGE_BASE_VALUE = 10;
    private const int RANDOM_GROW_OFFSET_MAX = 20; 
    private const bool SPEED_GROW = true;
    public static int GetStageDuration(int growthRate, int stage) {
        const int baseDuration = 30;

        double modifier = (float)AVERAGE_BASE_VALUE / growthRate;
        int stageDuration = (int)(baseDuration * modifier) * (stage+1);
        stageDuration += UnityEngine.Random.Range(0, RANDOM_GROW_OFFSET_MAX);

        return SPEED_GROW ? 1 : stageDuration;
    }

    public static int GetQuantity(SeedBagItemSO seed) {
        double quantity = ((seed.baseQuantity * 1.5) / (float)AVERAGE_BASE_VALUE + 2) - Random.Range(-1, 2);
        return (int)quantity;
    }

}
