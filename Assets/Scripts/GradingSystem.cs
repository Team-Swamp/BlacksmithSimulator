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

    private void Start()
    {
        SetDeposititWeapon(weapon);
        GetScore();
    }

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
        
        Debug.Log(_score);
    }
    
    public void SetDeposititWeapon(GameObject weapon)
    {
        var data = weapon.GetComponentsInChildren<WeaponPartData>();
        foreach (var a in data)
        {
            foreach (var desirable in a.WeaponPartsDesirables)
            {
                foreach (var keyword in currentHeroDesiers.GetHeroData().TextWithKey.Where(keyword => desirable == keyword.Keyword))
                {
                    _score++;
                }
            }
        }
    }

    public void SetCurrentHero(Diserars targetHero) => currentHeroDesiers = targetHero;
}
