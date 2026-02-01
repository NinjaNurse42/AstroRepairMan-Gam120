using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class TypewriterEffect : MonoBehaviour

{
    [SerializeField] private float typewriterSpeed = 50f;
    public Coroutine Run(string textToType, TMP_Text textlabel)
    {
        return StartCoroutine(routine: TypeText(textToType, textlabel));
    }
    private IEnumerator TypeText(string textToType, TMP_Text textlabel)
    {
        float t = 0;
        int CharIndex = 0;

        while (CharIndex < textToType.Length)
        {
            t += Time.deltaTime * typewriterSpeed;
            CharIndex = Mathf.FloorToInt(t);
            CharIndex = Mathf.Clamp(value: CharIndex, min: 0, max: textToType.Length);
            textlabel.text = textToType.Substring(startIndex: 0, length: CharIndex);
            yield return null;
        }
        textlabel.text = textToType;
    }
}
