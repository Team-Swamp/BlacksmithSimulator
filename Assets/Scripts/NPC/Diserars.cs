using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diserars : MonoBehaviour
{
    [SerializeField, Tooltip("Disersar are in this scriptableObject")] private DialogueObject dialogue;

    public DialogueObject GetHeroData() => dialogue;
}
