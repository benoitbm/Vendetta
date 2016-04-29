using UnityEngine;
using System.Collections;

public class Car_Hitbox : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            gameObject.transform.GetComponentInParent<Car_Handler>().leave();
    }
}
