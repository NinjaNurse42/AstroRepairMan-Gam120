using UnityEngine;
using TMPro;

public class VelocityTextUI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float speed = rb.linearVelocity.magnitude;
        text.text = "" + speed.ToString("F2");
    }
}