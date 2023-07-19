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
            //if(the next charcter = <) search for the right arrow ">" if we have both of them, then type the arrow itself and the content in one go. after that go letter by letter.
            charIndex = Mathf.FloorToInt(theText);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            if (textToType[charIndex] == '<')
            {
                Debug.Log(textToType[charIndex]);
                for (int i = charIndex; i < textToType.Length; i++)
                {
                    if (textToType[i] == '>')
                    {
                        charIndex = i;
                    }
                }
            }
            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }

        textLabel.text = textToType;
    }
}
