using UnityEngine;
using System.Collections;

//Script to move the bullet
public class bullet_move : MonoBehaviour {
    public float speed = 30.0f;
    public float lifeSpan = 3f;
    public Rigidbody2D bulletself;

    void Start()
    {
        //Rotation is already done during the creation of the bullet.
        //We just have to add the velocity locally, adapt it to global world (TransformDirection) and let the shells hit the ground.

        bulletself.velocity = transform.TransformDirection(Vector3.right * speed);

        Rigidbody2D.DestroyObject(gameObject, lifeSpan); //Self destroyed after x (float) seconds
    }

    void LateUpdate()
    {
        if (bulletself != null)
        {
            bulletself.velocity = transform.TransformDirection(Vector3.right * speed * Time.fixedDeltaTime); //Changing the speed according to FPS 
        }
    }
}
