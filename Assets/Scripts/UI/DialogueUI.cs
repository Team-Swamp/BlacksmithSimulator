using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private DialogueObject testDialogue;
    
    private TypeWriterEffect _typeWriterEffect;
    
    private void Start()
    {
        _typeWriterEffect = GetComponent<TypeWriterEffect>();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (var dialogue in dialogueObject.Dialogue)
        {
            yield return _typeWriterEffect.Run(dialogue, textlabel);
        }
    }
}
