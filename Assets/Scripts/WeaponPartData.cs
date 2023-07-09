using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPartData : MonoBehaviour
{
    [field: SerializeField] public WeaponPartsDesirables[] WeaponPartsDesirables { get; private set; }
}
