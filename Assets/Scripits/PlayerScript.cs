using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float MoveForce = 1f;
    public float RotateForce = 10f;
    private Rigidbody2D RB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
