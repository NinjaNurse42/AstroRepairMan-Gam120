using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MinimapSwitcher : MonoBehaviour
{
    public GameObject Map0;
    public GameObject Map1;
    public GameObject Map2;
    public GameObject Map3;
    public GameObject Map4;
    public GameObject Map5;
    public GameObject Map6;
    public GameObject Map7;
    public GameObject Map8;
    public GameObject Map9;
    public GameObject Cam0;
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;
    public GameObject Cam4;
    public GameObject Cam5;
    public GameObject Cam6;
    public GameObject Cam7;
    public GameObject Cam8;
    public GameObject Cam9;
    public GameObject player;
    BoxCollider2D box0;
    int boxNum;
    [SerializeField] private AudioClip MiniSwitcherClip;

    private void Start()
    {
        player = GameObject.Find("Player");
        Map0 = GameObject.Find("MiniParent0");
        Map1 = GameObject.Find("MiniParent1");
        Map2 = GameObject.Find("MiniParent2");
        Map3 = GameObject.Find("MiniParent3");
        Map4 = GameObject.Find("MiniParent4");
        Map5 = GameObject.Find("MiniParent5");
        Map6 = GameObject.Find("MiniParent6");
        Map7 = GameObject.Find("MiniParent7");
        Map8 = GameObject.Find("MiniParent8");
        Map9 = GameObject.Find("MiniParent9");
        Cam0 = GameObject.Find("MiniCam0");
        Cam1 = GameObject.Find("MiniCam1");
        Cam2 = GameObject.Find("MiniCam2");
        Cam3 = GameObject.Find("MiniCam3");
        Cam4 = GameObject.Find("MiniCam4");
        Cam5 = GameObject.Find("MiniCam5");
        Cam6 = GameObject.Find("MiniCam6");
        Cam7 = GameObject.Find("MiniCam7");
        Cam8 = GameObject.Find("MiniCam8");
        Cam9 = GameObject.Find("MiniCam9");
        box0 = Map0.GetComponent<BoxCollider2D>();

        //gets the object in the engine and translates it to code
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Zone1"))
        {
            boxNum = 0;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone2"))
        {
            boxNum = 1;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone3"))
        {
            boxNum = 2;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone4"))
        {
            boxNum = 3;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone5"))
        {
            boxNum = 4;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone6"))
        {
            boxNum = 5;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone7"))
        {
            boxNum = 6;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone8"))
        {
            boxNum = 7;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone9"))
        {
            boxNum = 8;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }
        else if (collision.CompareTag("Zone10"))
        {
            boxNum = 9;
            SFXManager.Instance.ScrapPickup(MiniSwitcherClip, transform, 1.0f);
        }

        switch (boxNum)
        {
            case 0:
                Cam0.SetActive(true);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 1:
                Cam0.SetActive(false);
                Cam1.SetActive(true);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 2:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(true);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break ;
            case 3:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(true);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 4:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(true);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 5:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(true);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 6:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(true);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 7:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(true);
                Cam8.SetActive(false);
                Cam9.SetActive(false);
                break;
            case 8:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(true);
                Cam9.SetActive(false);
                break;
            case 9:
                Cam0.SetActive(false);
                Cam1.SetActive(false);
                Cam2.SetActive(false);
                Cam3.SetActive(false);
                Cam4.SetActive(false);
                Cam5.SetActive(false);
                Cam6.SetActive(false);
                Cam7.SetActive(false);
                Cam8.SetActive(false);
                Cam9.SetActive(true);
                break;
        }
        //finds the map the player is colliding with and displays it
    }


}
