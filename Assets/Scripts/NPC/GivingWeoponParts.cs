using System;
using System.Collections;
using UnityEngine;

public class GivingWeoponParts : MonoBehaviour
{
    [SerializeField] private int weaponPartToGive;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip happySound;
    [SerializeField] private AudioClip sadSound;

    public WeaponScore score;

    private Inventory _inventory;

    private void Start() => _inventory = FindObjectOfType<Inventory>();

    public void SelectItems()
    {
        switch (score)
        {
            case WeaponScore.Squalid:
            case WeaponScore.Common:
                //todo: Negative dialog
                audioSource.clip = sadSound;
                StartCoroutine(StartWaling());
                break;
            case WeaponScore.Uncommon:
            case WeaponScore.Rare:
            case WeaponScore.Epic:
            case WeaponScore.Legendary:
                //todo: Positive dialog
                _inventory.ActivatePart(weaponPartToGive);
                audioSource.clip = happySound;
                StartCoroutine(StartWaling());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        audioSource.Play();
    }

    public void SetScore(int targetScore) => score = (WeaponScore)targetScore;

    private void StartWalking() => GetComponent<HeroWalking>().SetToWalkingBackState();

    private IEnumerator StartWaling()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<HeroWalking>().SetToWalkingBackState();
    }
}
