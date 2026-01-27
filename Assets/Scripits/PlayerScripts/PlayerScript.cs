using TMPro;
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
        if (Input.GetKey(KeyCode.W))
        {
            RB.AddForce(transform.up * MoveForce); //change to transform.up

        }
        if (Input.GetKey(KeyCode.S))
        {
            RB.AddForce(-transform.up * MoveForce / 2f);  //change to transform.down
        }

        if (Input.GetKey(KeyCode.A))
        {
            RB.AddTorque(RotateForce);
        }


        if (Input.GetKey(KeyCode.D))
        {
            RB.AddTorque(-RotateForce);
        }
    }
    

}
