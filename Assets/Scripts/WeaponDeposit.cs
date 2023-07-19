using System.Collections.Generic;
using UnityEngine;

public sealed class WeaponDeposit : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private List<GameObject> WeaponDepositLocations;
    [SerializeField] private List<GameObject> DepositedWeapons;

    [SerializeField] private int nextWeaponposition = 0;
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
            _currentWeapon = WeaponDepositLocations[nextWeaponposition];
        }

        SetNextPosition();


        var _weapon = Instantiate(finalWeapon, _currentWeapon.transform.position, _currentWeapon.transform.rotation);
        
        if(DepositedWeapons.Count == 3) Destroy(DepositedWeapons[nextWeaponposition]);
        DepositedWeapons[nextWeaponposition] = _weapon;
        _depositedWeapon = _weapon;

    }

    public void SetNextPosition()
    {
        nextWeaponposition++;


        if (nextWeaponposition == 3) nextWeaponposition = 0;
    }

    public void ClearWeapon(int numb)
    {
        print(numb);
        if(DepositedWeapons[numb].gameObject != null) Destroy(DepositedWeapons[numb]);
    }
    
    /// <summary>
    /// Used to put the created weapons in the background?
    /// </summary>
    /// <returns>The last deposited weapon</returns>
    public GameObject GetDepositedWeapon() => _depositedWeapon;
}
