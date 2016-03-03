using UnityEngine;
using System.Collections;

//Script to manage weapons. See UML for more infos.

public class Weapon_container : MonoBehaviour {

//private Weapon[] weaponList = new Weapon[10]; //The magic of C# is so magic that we don't need to delete or free when we do a "new". 
   // private Weapon tempWeapon; //Variable which will be used for operations
	

    void start()
    {
        //for (var i=1,)
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
}
