using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour {
    
    public SteamVR_TrackedObject controller;

    [HideInInspector]
    public Vector3 prevPos;

    [HideInInspector]
    public bool bCanGrab;

	// Use this for initialization
	void Start () {

        prevPos = controller.transform.localPosition;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grip")
            bCanGrab = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grip")
            bCanGrab = false;
    }
}
