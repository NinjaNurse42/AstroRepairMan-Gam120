using TMPro;
using UnityEngine;

public class TextboxUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        textLabel.text = "Hello!\n This is my second Line.";
    }
}
