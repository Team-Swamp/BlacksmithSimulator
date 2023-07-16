using System.Collections;
using TMPro;
using UnityEngine;

public sealed class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typeWriterSpeed;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        var theText = 0f;
        var charIndex = 0;

        while (charIndex < textToType.Length)
        {
            theText += Time.deltaTime * typeWriterSpeed;
            charIndex = Mathf.FloorToInt(theText);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }

        textLabel.text = textToType;
    }
}
