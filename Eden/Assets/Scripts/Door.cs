using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Vector3 Rotation;
    private bool bHandleOpen = false;
    public Player_Input goController;

    public Player_Input goControllerLeft;
    public Player_Input goControllerRight;
    public bool bCanTurn = false;
    public bool bCanGrab = false;

    // Use this for initialization
    void Start () {

        Rotation = transform.eulerAngles;
	}

    void OpenDoor()
    {
        Debug.Log("La porte s'ouvre");
    }

    void LockHandle()
    {
        if (transform.eulerAngles.x < 95 && transform.eulerAngles.x > 85)
        {
            bHandleOpen = true;
            Rotation.x = 90;
            transform.eulerAngles = Rotation;
            OpenDoor();
        }
    }

    void RotateHandle()
    {
        if (bCanTurn)
        {
            if (!bHandleOpen)
            {
                Rotation.x = -goController.iRotationZ;
                transform.eulerAngles = Rotation;
            }
        }
    }

    public void Turn(int i)
    {
        if (i == 1)
            bCanTurn = true;
        else if (i == 0)
            bCanTurn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Controller (left)" || other.name == "Controller (right)")
        {
            goController = other.gameObject.GetComponent<Player_Input>();
            bCanGrab = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Controller (left)" || other.name == "Controller (right)")
        {
            goController = null;
            bCanGrab = false;
        }
    }

    // Update is called once per frame
    void Update () {
        
        RotateHandle();
        LockHandle();
    }

    private static Door instance;
    public static Door Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("Handle").GetComponent<Door>();
            }

            return instance;
        }
    }
}
