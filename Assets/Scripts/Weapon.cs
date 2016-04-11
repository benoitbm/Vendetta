using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon class is used for one weapon, and to check his ammo and stuff.
/// </summary>
public class Weapon : MonoBehaviour
{
    private bool unlocked = false; //By default, every weapon will be locked (except gun and unarmed)
    public ushort weaponType; //To know which sprite to use. (Unarmed, melee weapon, one hand weapon or rifle weapon)
    // 0 = Bare hands
    // 1 = One hand melee
    // 2 = Two hands melee
    // 3 = One hand firearm
    // 4 = Two hands firearm

    public int clipSize; //Will be the number of rounds we have in a ammo clip
    public int maxAmmo; //Max ammo without ammo in clip

    public bool clipReload = true; //If true : It will reload all the ammo once (like a gun), if false, it will be a shell system (like shotguns)

    private int currentAmmo; //Remain ammo outside ammo clip
    private int remainAmmoClip; //Remaining ammo in the weapon's clip

    private bool isReloading = false;

    public float reloadCooldown = 1.0f; //Cooldown in seconds after the reload has been done before using weapon again.

    public bool isEnemy = false; //If it's a enemy, he has INFINITE AMMO ! (For the moment)

    public GameObject weaponObject; //The gameobject reference in this (tag used)

    /// <summary>
    /// This function is used to lock or unlock a weapon.
    /// </summary>
    /// <param name="_unlocked">Boolean to chose if you unlock or lock a weapon.</param>
    public void unlockWeapon(bool _unlocked)
    {
        unlocked = _unlocked;
        if (! unlocked)
        {
            currentAmmo = 0;
            remainAmmoClip = 0;
        }
        else
        {
            currentAmmo = clipSize * 2;
            remainAmmoClip = clipSize;
        }
    }

    /// <summary>
    /// This function is used to lock or unlock a weapon. You can chose the ammo he gots
    /// </summary>
    /// <param name="_unlocked">Boolean to chose if you unlock or lock a weapon.</param>
    /// <param name="_currentAmmo">The total ammo you give when you unlock the weapon.</param>
    public void unlockWeapon(bool _unlocked, int _currentAmmo)
    {
        unlocked = _unlocked;
        if (!unlocked)
        {
            currentAmmo = 0;
            remainAmmoClip = 0;
        }
        else
        {
            currentAmmo = _currentAmmo-clipSize;

            if (currentAmmo > maxAmmo) //To avoid some programming errors (if you put too much ammo)
                currentAmmo = maxAmmo;

            if (currentAmmo < 0) //In case you unlock a weapon, but it doesn't have enough ammo for a full clip.
            {
                currentAmmo = 0;
                remainAmmoClip = _currentAmmo;
            }
            else
                remainAmmoClip = clipSize;
        }

    }

    /// <summary>
    /// This function is used for the display of ammo. It will return the remaining ammo in clip with remaining ammo.
    /// </summary>
    /// <returns>It returns a string with the data needed.</returns>
    public string getAmmoUI()
    {
        return remainAmmoClip.ToString() + "/" + currentAmmo.ToString();
    }

    /// <summary>
    /// Function called every time you shot (removing one bullet in the clip)
    /// </summary>
    /// <returns>It will return if the clip if you can shoot or not.</returns>
    public bool shot()
    {
        if (!isReloading)
        {
            if (remainAmmoClip-- >= 1)
                return true;
            else
            {
                remainAmmoClip = 0;
                return false;
            }
        }
        else
            return false;
    }

    /// <summary>
    /// Function called every time you shot, with n bullets removed from the clip.
    /// </summary>
    /// <param name="ammoShot">Number of bullets you want to remove.</param>
    /// <returns>It will return if the clip if you can shoot or not</returns>
    public bool shot(int ammoShot)
    {
        if (!isReloading)
        {
            remainAmmoClip -= ammoShot;
            if (remainAmmoClip >= 0)
                return true;
            else
            {
                remainAmmoClip += ammoShot; //If you can't shoot because you don't have enough ammo (like 2 bullets at once), it will put back the ammo.
                return false;
            }
        }
        else
            return false;

    }

    public void reload()
    {
        if (remainAmmoClip < clipSize) //If you are full size, you don't need to reload.
        {
            int ammoToAdd = clipSize - remainAmmoClip;

            if (currentAmmo >= ammoToAdd)
            {
                isReloading = true;
                currentAmmo -= ammoToAdd;
                remainAmmoClip += ammoToAdd;
            }
            else if (currentAmmo > 0 && currentAmmo < ammoToAdd)
            {
                isReloading = true;
                remainAmmoClip += currentAmmo;
                currentAmmo = 0;
            }
            
            if (isReloading)
                StartCoroutine(rldCooldown());

        }
    }

    public void addAmmo(int ammo)
    {
        currentAmmo += ammo;
        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;
    }

    IEnumerator rldCooldown()
    {
        yield return new WaitForSeconds(reloadCooldown);
        isReloading = false;
    }

    public bool clipIsEmpty()
    { return (remainAmmoClip == 0); }

    public bool WisReloading()
    { return isReloading; }

    public bool canReload()
    { return (currentAmmo > 0 && remainAmmoClip < clipSize) ; }

    public int getclipSize()
    { return clipSize; }
}
