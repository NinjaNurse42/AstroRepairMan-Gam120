using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
  
    [SerializeField] float maxOxygen = 100f;
    [SerializeField] float depletionRateOutside = 10f; // oxygen lost per second when not in zone
    [SerializeField] float regenRateInside = 15f; // oxygen restored per second inside zone
    [SerializeField] float suffocationDamagePerSecond = 20f; // optional, require
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
