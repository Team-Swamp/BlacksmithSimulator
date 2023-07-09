using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GradingSystem : MonoBehaviour
{
    [SerializeField] private Diserars currentHeroDesiers;
    [SerializeField] private GameObject weapon;
    [SerializeField] private WeaponScore weaponScore;
    private int _score;

    public void GetScore()
    {
        if (_score == 0)
        {
            weaponScore = WeaponScore.Squalid;
        }
        else if (_score == 1)
        {
            weaponScore = WeaponScore.Common;
        }
        else if (_score == 2)
        {
            weaponScore = WeaponScore.Uncommon;
        }
        else if (_score == 3)
        {
            weaponScore = WeaponScore.Rare;
        }
        else if (_score == 4)
        {
            weaponScore = WeaponScore.Epic;
        }
        else if (_score > 4)
        {
            weaponScore = WeaponScore.Legendary;
        }

        var givingWeeponParts = currentHeroDesiers.gameObject.GetComponent<GivingWeoponParts>();
        
        givingWeeponParts.score = weaponScore;
        givingWeeponParts.SelectItems();
    }
    
    public void SetDeposititWeapon()
    {
        weapon = FindObjectOfType<WeaponDeposit>().GetDepositedWeapon();

        _score = 0;
        
        var data = weapon.GetComponentsInChildren<WeaponPartData>();
        foreach (var a in data)
        {
            foreach (var desirable in a.WeaponPartsDesirables)
            {
                foreach (var keyword in currentHeroDesiers.GetHeroData().TextWithKey)
                {
                    if (desirable == keyword.Keyword) _score++;
                }
            }
        }
    }

    public void SetHero(Diserars target)
    {
        // Debug.Log(target.gameObject.name);
        currentHeroDesiers = target;
    } 
}
