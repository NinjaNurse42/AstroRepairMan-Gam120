using UnityEngine;
using TMPro;

public class PartsTextUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        bat 
      
            text.text = "" + inventory.parts/2;
        
    }
}