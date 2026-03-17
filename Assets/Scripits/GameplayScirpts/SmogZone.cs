using UnityEngine;

public class SmogTrap : MonoBehaviour
{
    [SerializeField] float smogDrainMultiplier = 3f;

    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerOxygen oxygen = collision.GetComponent<PlayerOxygen>();

        if (oxygen != null)
        {
            // simulate faster oxygen loss by repeatedly lowering oxygen
            float drain = Time.deltaTime * smogDrainMultiplier;

            float newLevel = oxygen.GetOxygenLevel() - drain;

            // clamp so it doesn't go negative
            if (newLevel < 0f)
                newLevel = 0f;

            // directly set oxygen using reflection of existing reset logic
            typeof(PlayerOxygen)
                .GetField("oxygenLevel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(oxygen, newLevel);
        }
    }
}