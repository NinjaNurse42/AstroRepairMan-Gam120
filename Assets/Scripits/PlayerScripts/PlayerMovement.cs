using TMPro;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float MoveForce = 1f;
    public float RotateForce = 10f;
    private Rigidbody2D RB;

    void Start() => RB = GetComponent<Rigidbody2D>();

    void Update()
    {
       
        if (Time.timeScale == 0f)
            return;

        // Movement forward/back
        if (Input.GetKey(KeyCode.W))
            RB.AddForce(transform.up * MoveForce);

        if (Input.GetKey(KeyCode.S))
            RB.AddForce(-transform.up * MoveForce / 2f);

        // Rotation
        if (Input.GetKey(KeyCode.A))
            RB.AddTorque(RotateForce);

        if (Input.GetKey(KeyCode.D))
            RB.AddTorque(-RotateForce);
    }
}