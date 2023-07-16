using UnityEngine;

public sealed class WeaponPartData : MonoBehaviour
{
    [field: SerializeField] public WeaponPartsDesirables[] WeaponPartsDesirables { get; private set; }
}
