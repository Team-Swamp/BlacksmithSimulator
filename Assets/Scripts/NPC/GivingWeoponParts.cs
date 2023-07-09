using System;
using System.Collections;
using UnityEngine;

public class GivingWeoponParts : MonoBehaviour
{
    [SerializeField] private int weaponPartToGive;
    [SerializeField] private DialogueObject positiveResponds;
    [SerializeField] private DialogueObject negativeResponds;

    public WeaponScore score;

    private Inventory _inventory;
    private DialogueUI dialogueUI;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        dialogueUI = FindObjectOfType<DialogueUI>();
    }
    
    public void SelectItems()
    {
        switch (score)
        {
            case WeaponScore.Squalid:
            case WeaponScore.Common:
                dialogueUI.ShowDialogue(negativeResponds);
                StartCoroutine(StartWaling());
                break;
            case WeaponScore.Uncommon:
            case WeaponScore.Rare:
            case WeaponScore.Epic:
            case WeaponScore.Legendary:
                dialogueUI.ShowDialogue(positiveResponds);
                _inventory.ActivatePart(weaponPartToGive);
                StartCoroutine(StartWaling());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Debug.Log(score);
    }

    public void SetScore(int targetScore) => score = (WeaponScore)targetScore;
    
    private void StartWalking() => GetComponent<HeroWalking>().SetToWalkingBackState();

    private IEnumerator StartWaling()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<HeroWalking>().SetToWalkingBackState();
    }
}
