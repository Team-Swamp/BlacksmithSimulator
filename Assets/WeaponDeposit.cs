using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDeposit : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private List<GameObject> WeaponDepositLocations;
    [SerializeField] private List<GameObject> DepositedWeapons;

    [SerializeField] private int _nextWeaponposition = 0;
    private GameObject _currentWeapon;
    private GameObject _depositedWeapon;

    private void Start()
    {
        for (int i = 0; i < 3; i++) DepositedWeapons.Add(null);
    }

    public void GetFinalWeapon()
    {
        var finalWeapon = new GameObject();

        foreach (var weaponPart in itemManager.GetItems())
        {
            weaponPart.transform.SetParent(finalWeapon.transform);
        }

        if(WeaponDepositLocations != null)
        {
            _currentWeapon = WeaponDepositLocations[_nextWeaponposition];
        }

        SetNextPosition();


        var _weapon = Instantiate(finalWeapon, _currentWeapon.transform.position, _currentWeapon.transform.rotation);
        
        if(DepositedWeapons.Count == 3) Destroy(DepositedWeapons[_nextWeaponposition]);
        DepositedWeapons[_nextWeaponposition] = _weapon;
        _depositedWeapon = _weapon;

    }

    public void SetNextPosition()
    {
        _nextWeaponposition++;


        if (_nextWeaponposition == 3) 
        {
            _nextWeaponposition = 0;
            
        }
    }

    public void ClearWeapon(int numb)
    {
        print(numb);
        if(DepositedWeapons[numb].gameObject != null)
        {
            Destroy(DepositedWeapons[numb]);

        }
    }

    //TO GET THE LAST DEPOSITED WEAPON
    public GameObject GetDepositedWeapon()
    {
        return _depositedWeapon;
    }
}
