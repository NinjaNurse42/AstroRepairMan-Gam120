using Unity.VisualScripting;
using UnityEngine;

public class DialougeManager : MonoBehaviour
{
    
    public static DialougeManager Instance { get; private set; }

    [SerializeField] private TextboxUI textboxUI;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // optional: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (textboxUI == null)
            Debug.LogWarning("DialogueManager: TextboxUI reference not set.", this);
    }

    public void ShowDialogue(DialougeObject dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("DialogueManager.ShowDialogue: null dialogue provided.", this);
            return;
        }

        if (textboxUI == null)
        {
            Debug.LogWarning("DialogueManager.ShowDialogue: textboxUI not assigned.", this);
            return;
        }

        textboxUI.ShowDialouge(dialogue);
    }

    public static void Show(DialougeObject dialogue)
    {
        if (Instance == null)
        {
            Debug.LogWarning("DialogueManager.Show: No DialogueManager instance found.", null);
            return;
        }

        Instance.ShowDialogue(dialogue);
    }



}
