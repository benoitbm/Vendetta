using UnityEngine;
using System.Collections;
using System; //For bool convertions

//Script for moving the caracter.
public class Moving : MonoBehaviour
{

    //Player variables
    public float walkSpeed = 10.0f; //Move speed of player

    // Update is called once per frame
    void FixedUpdate()
    {

        var cameraDirection = Camera.main.transform.TransformDirection(Vector3.forward);
        cameraDirection.z = 0.0f;

        float horizontalMove = Input.GetAxis("Horizontal"); //Using this also support controllers + ZQSD + arrows natively
        float verticalMove = Input.GetAxis("Vertical");

        //float horizontalLook = Input.GetAxis("RightH"); //Left stick of controller 
        //float verticalLook = Input.GetAxis("RightV"); //If not found, change name in Edit > Project Settings > Input

        // Moving relative to Camera, which won't rotate.
        //transform.position += (Camera.main.transform.right * walkSpeed * Time.deltaTime * horizontalMove);
        //transform.position += (Camera.main.transform.up * walkSpeed * Time.deltaTime * verticalMove);

        Vector3 Vec_Move = new Vector3(horizontalMove,verticalMove,0.0f);
        GetComponent<Rigidbody>().velocity = Vec_Move * walkSpeed;

        //Rotating following the mouse
        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        var direction = Input.mousePosition - objectPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        //Rotating with the right stick
        //TODO : Chose if we want to play either mouse + keyboard or gamepad
        //transform.localEulerAngles = new Vector3(horizontalLook, verticalMove;

    }
}
