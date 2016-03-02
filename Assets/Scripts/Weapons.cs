using UnityEngine;
using System.Collections;

//Script to manage weapons. See UML for more infos.

/// <summary>
/// Structure of a weapon, with all his stats
/// </summary>
public struct Weapon
{
    private bool unlocked; //By default, every weapon will be locked (except gun and unarmed)
    private bool meleeWeapon;

    public int clipSize; //Will be the number of rounds we have in a ammo clip
    public int maxAmmo; //Max ammo without ammo in clip

    public int currentAmmo;
    public int remainAmmoClip; //Remaining ammo in the weapon's clip

    public GameObject weaponObject; //The gameobject reference in this (tag used)
}

public class Weapons : MonoBehaviour {

    private Weapon[] weaponList = new Weapon[10]; //The magic of C# is so magic that we don't need to delete or free when we do a "new". 
    private Weapon tempWeapon; //Variable which will be used for operations
	

    void start()
    {
        weaponList[0] = new Weapon();
    }

    /// <summary>
    /// get_Ammo() is a function to get the remaining rounds in the weapon's clip
    /// </summary>
    /// <returns>Returns an int</returns>
    public int get_Ammo(int WID)
    {
        return weaponList[WID].remainAmmoClip;
    }
}
