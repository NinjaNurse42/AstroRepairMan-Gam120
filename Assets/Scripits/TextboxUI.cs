using System.Collections;
using TMPro;
using UnityEngine;

public class TextboxUI : MonoBehaviour
{
    [SerializeField] private GameObject dialougebox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialougeObject testDialouge;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        ShowDialouge(testDialouge);
    }

    public void ShowDialouge(DialougeObject dialougeObject)
    {
        dialougebox.SetActive(true);
        StartCoroutine(routine: StepThroughDialouge(dialougeObject));
    }
    private IEnumerator StepThroughDialouge(DialougeObject dialougeObject)
    {
        foreach (string dialouge in dialougeObject.Dialouge)
        {
            yield return typewriterEffect.Run(dialouge, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialougebox.SetActive(false);
        textLabel.text = string.Empty;

    }
}

