using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MinimapSwitcher : MonoBehaviour
{
    public GameObject Map0;
    public GameObject Map1;
    public GameObject Cam0;
    public GameObject Cam1;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        Map0 = GameObject.Find("MiniParent0");
        Map1 = GameObject.Find("MiniParent1");
        Cam0 = GameObject.Find("MiniCam0");
        Cam1 = GameObject.Find("MiniCam1");
        //gets the object in the engine and translates it to code
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Map0)
        {
            Cam0.SetActive(true);
            Cam1.SetActive(false);
        }
        else if (Map1)
        {
            Cam1.SetActive(false);
            Cam0.SetActive(true);
        }

        //finds the map the player is colliding with and displays it
    }
}
