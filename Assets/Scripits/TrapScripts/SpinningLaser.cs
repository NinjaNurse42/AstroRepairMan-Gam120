using UnityEngine;

public class SpinningLaser : MonoBehaviour
{
    public float rotationspeed;

    private void Start()
    {
        
    }


    private void FixedUpdate()
    {
        this.transform.Rotate (new Vector3 (0,0, rotationspeed));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //kill player!
    }
}
