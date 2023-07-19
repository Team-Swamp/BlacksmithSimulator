using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private TMP_Text keywordText;
    [SerializeField] private DialogueObject startDialogue;
    [SerializeField] private WeaponPartsDesirables weaponDesirables;
    [SerializeField] private string keyWordColor;

    private int _dialogueIndex;
    private KeyWithText _randomDialogue;
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
            ? StepThroughDialogue(startDialogue) 
            : StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        _dialogueIndex = 1;
        Debug.Log(dialogueObject);
        var randomNumber = Random.Range(0, dialogueObject.TextWithKey.Count);
        _randomDialogue = dialogueObject.TextWithKey[randomNumber];
        //_randomDialogue = dialogueObject.TextWithKey[randomNumber].Dialogue[0];
        
        var dialogueCombinedWithColor = _randomDialogue.Dialogue[0] + keyWordColor + weaponDesirables.ToString() + "</color>" + _randomDialogue.Dialogue[1];

        yield return _typeWriterEffect.Run(dialogueCombinedWithColor, textlabel);
        yield return new WaitUntil(() => SetNextDialogue(dialogueObject.TextWithKey[randomNumber]));

        CloseDialogueBox();
    }
    
     private bool SetNextDialogue(KeyWithText keyWithText)
     {
         //if (_dialogueIndex >= keyWithText.Dialogue.Length) return false;
         if (!Input.GetKeyDown(KeyCode.Space)) return false;
         
         //_randomDialogue = keyWithText.Dialogue[_dialogueIndex];
         //_dialogueIndex++;
         return true;
     }
    
    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textlabel.text = string.Empty;
    }
}
