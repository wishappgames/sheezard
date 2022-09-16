using UnityEngine;

public class WeaponBuff : MonoBehaviour
{
    public WeaponType WeaponType { get { return weaponType; } }
    public int Power { get { return power; } }

    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private int power;
}

public enum WeaponType { Bow }
