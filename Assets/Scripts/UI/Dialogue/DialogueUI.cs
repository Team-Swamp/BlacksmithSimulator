using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private TMP_Text keywordText;
    [SerializeField] private DialogueObject testDialogue;

    private int _dialogueIndex;
    private string _randomDialogue;
    private TypeWriterEffect _typeWriterEffect;

    private void Start()
    {
        _typeWriterEffect = GetComponent<TypeWriterEffect>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject? dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(dialogueObject == null
            ? StepThroughDialogue(testDialogue)
            : StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        _dialogueIndex = 1;
        
        var randomNumber = Random.Range(0, dialogueObject.TextWithKey.Count);
        _randomDialogue = dialogueObject.TextWithKey[randomNumber].Dialogue[0];
        
        for (int i = 0; i < dialogueObject.TextWithKey[randomNumber].Dialogue.Length; i++)
        {
            yield return _typeWriterEffect.Run(_randomDialogue, textlabel);
            yield return new WaitUntil(() => SetNextDialogue(dialogueObject.TextWithKey[randomNumber]));
        }
            
        CloseDialogueBox();
    }

     private bool SetNextDialogue(KeyWithText keyWithText)
     {
         if (_dialogueIndex >= keyWithText.Dialogue.Length) return false;
         if (!Input.GetKeyDown(KeyCode.Space)) return false;
         
         _randomDialogue = keyWithText.Dialogue[_dialogueIndex];
         _dialogueIndex++;
         return true;
     }
    
    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textlabel.text = string.Empty;
    }
}
