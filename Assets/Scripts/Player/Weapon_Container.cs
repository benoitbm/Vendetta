using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script to manage weapons. See UML for more infos.

public class Weapon_Container : MonoBehaviour {

    //private Weapon[] weaponList = new Weapon[10]; //The magic of C# is so magic that we don't need to delete or free when we do a "new". 
    private Weapon currentWeapon = null; //Variable which will be used for operations

    public Text ammoText;

    public bool autoReload = true;

    int unlockedWeapon;

    void Start()
    {
        
        //Getting all children with weapons
        Transform[] children = GetComponentsInChildren<Transform>();

        //Searching the gun in the hierarchy
        foreach (Transform child in children)
        {
            if (child.CompareTag("Weapon"))
                currentWeapon = child.gameObject.GetComponent<Weapon>();
        }

        if (currentWeapon != null)
        {
            currentWeapon.unlockWeapon(true, 30);
        }
    }

    void Update()
    {
        updateAmmoText();
    }

    /// <summary>
    /// get_Ammo() is a function to get the remaining rounds in the weapon's clip
    /// </summary>
    /// <returns>Returns an int</returns>
    public int get_Ammo(int WID)
    {
        //return weaponList[WID].remainAmmoClip;
        return 0;
    }

    public void updateAmmoText()
    {
        if (currentWeapon != null)
            ammoText.text = currentWeapon.getAmmoUI();
    }

    public void addAmmo(int ammo)
    {
        if (currentWeapon != null)
            currentWeapon.addAmmo(ammo);
    }

    public int getClipSize()
    { return currentWeapon.getclipSize(); }
}