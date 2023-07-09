using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponPartButtons;
    [SerializeField] private bool[] activeWeaponPartButtons;

    private void Awake()
    {
        activeWeaponPartButtons = new bool[weaponPartButtons.Length];
    }

    public void ActivatePart(int targetPart)
    {
        if(CheckAllButtonsActive()) return;
        
        if (activeWeaponPartButtons[targetPart])
        {
            int randomNumber;
            do
            {
                randomNumber = Random.Range(0, weaponPartButtons.Length);
            } while (activeWeaponPartButtons[randomNumber]);

            activeWeaponPartButtons[randomNumber] = true;
            weaponPartButtons[randomNumber].SetActive(true);
        }
        else
        {
            activeWeaponPartButtons[targetPart] = true;
            weaponPartButtons[targetPart].SetActive(true);
        }
    }
    
    private bool CheckAllButtonsActive()
    {
        return activeWeaponPartButtons.All(t => t);
    }
}
