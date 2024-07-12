using UnityEngine;

public static class OverhaulFormulas 
{
    const int AVERAGE_BASE_VALUE = 10;
    public static int GetStageDuration(int growthRate, int stage) {
        const int baseDuration = 60;

        double modifier = (float)AVERAGE_BASE_VALUE / growthRate;
        int stageDuration = (int)(baseDuration * modifier) * (stage+1);

        return stageDuration;
    }

}
