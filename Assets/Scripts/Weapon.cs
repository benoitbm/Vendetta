using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon class is used for one weapon, and to check his ammo and stuff.
/// </summary>
public class Weapon : MonoBehaviour
{
    private bool unlocked; //By default, every weapon will be locked (except gun and unarmed)
    private ushort weaponType; //To know which sprite to use. (Unarmed, melee weapon, one hand weapon or rifle weapon)
    // 0 = Bare hands
    // 1 = One hand melee
    // 2 = Two hands melee
    // 3 = One hand firearm
    // 4 = Two hands firearm

    public int clipSize; //Will be the number of rounds we have in a ammo clip
    public int maxAmmo; //Max ammo without ammo in clip

    private int currentAmmo;
    private int remainAmmoClip; //Remaining ammo in the weapon's clip

    public GameObject weaponObject; //The gameobject reference in this (tag used)


}
