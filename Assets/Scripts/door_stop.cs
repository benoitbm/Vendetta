using UnityEngine;
using System.Collections;

public class door_stop : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.CompareTag("Door"))
            {
                print("This is a door.");
                Rigidbody[] others = other.gameObject.transform.parent.GetComponentsInChildren<Rigidbody>();
                print("You have " + others.Length + " rigidbody");
                foreach (Rigidbody rb in others)
                    rb.velocity = Vector3.zero;
            }
            else
                print("It's not a door.");
        }
    }
}