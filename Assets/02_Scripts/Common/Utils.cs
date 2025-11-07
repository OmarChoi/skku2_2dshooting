using UnityEngine;

public static class Utils
{
    public static int GetRandomIndexByWeight(int totalWeight, int[] weights)
    {
        float randomValue = UnityEngine.Random.value;
        float totalValue = 0.0f;
        int type = 0;
        for (int i = 0; i < weights.Length; ++i)
        {
            totalValue += (float)weights[i] / totalWeight;
            if (randomValue < totalValue)
            {
                type = i;
                break;
            }
        }
        return type;
    }
}
