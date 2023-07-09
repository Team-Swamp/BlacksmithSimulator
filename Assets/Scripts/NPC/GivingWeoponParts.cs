using System;
using UnityEngine;

public class GivingWeoponParts : MonoBehaviour
{
    [SerializeField] private int weaponPartToGive;

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
                StartWalking();
                break;
            case WeaponScore.Uncommon:
            case WeaponScore.Rare:
            case WeaponScore.Epic:
            case WeaponScore.Legendary:
                //todo: Positive dialog
                _inventory.ActivatePart(weaponPartToGive);
                StartWalking();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Debug.Log(score);
    }

    public void SetScore(int targetScore) => score = (WeaponScore)targetScore;
    
    private void StartWalking() => GetComponent<HeroWalking>().SetToWalkingBackState();
}
