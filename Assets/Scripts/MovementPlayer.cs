using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour {

    #region PROPERTIES
    //Set speed
    public float movementSpeed;

    //Set player controls
    public int playerNumer = 0;
    #endregion

    #region FUNCTIONS
    // Use this for initialization
    void Start()
    {

    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (playerNumer == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
            {
                transform.position += (transform.forward * Time.deltaTime * movementSpeed * 2.5f);
            }
            else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (transform.forward * Time.deltaTime * movementSpeed);
            }
            else if (Input.GetKey("s"))
            {
                transform.position -= (transform.forward * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * movementSpeed *15.0f, Space.World);
            }
            else if (Input.GetKey("d") && !Input.GetKey("a"))
            {
                transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * movementSpeed * 15.0f, Space.World);
            }
        }
        else if(playerNumer == 1)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("i"))
            {
                transform.position += (transform.forward * Time.deltaTime * movementSpeed * 2.5f);
            }
            else if (Input.GetKey("i") && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (transform.forward * Time.deltaTime * movementSpeed);
            }
            else if (Input.GetKey("k"))
            {
                transform.position -= (transform.forward * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey("j") && !Input.GetKey("l"))
            {
                transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * movementSpeed * 15.0f, Space.World);
            }
            else if (Input.GetKey("l") && !Input.GetKey("j"))
            {
                transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * movementSpeed * 15.0f, Space.World);
            }
        }
    }
    #endregion
}

