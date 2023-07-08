using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public List<KeyWithText> textWithKey = new List<KeyWithText>();

    public List<KeyWithText> TextWithKey => textWithKey;
}

[Serializable]
public struct KeyWithText
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private DialogueKeyWords keyword;
    
    public string[] Dialogue => dialogue; //prevents overwrite
    public string Keyword => keyword.ToString();
}
