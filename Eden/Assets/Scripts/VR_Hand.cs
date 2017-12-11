using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Hand : MonoBehaviour {

    public GameObject heldObject;
    VR_Controller controller;

    Rigidbody simulator;

	// Use this for initialization
	void Start () {

        simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        simulator.transform.parent = transform.parent;

        controller = GetComponent<VR_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (heldObject)
        {
            simulator.velocity = (transform.position - simulator.position) * 50f;
            if(controller.Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().velocity = simulator.velocity;
                heldObject.GetComponent<VR_HeldObject>().parent = null;
                heldObject.layer = 0;
                heldObject = null;
            }
        }
        else
        {
            if (controller.Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);

                foreach (Collider col in cols)
                {
                    if ( heldObject == null && col.GetComponent<VR_HeldObject>() && col.GetComponent<VR_HeldObject>().parent == null)
                    {
                        heldObject = col.gameObject;
                        heldObject.transform.parent = transform;
                        heldObject.transform.localPosition = Vector3.zero;
                        heldObject.transform.localRotation = Quaternion.identity;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        heldObject.GetComponent<VR_HeldObject>().parent = controller;
                        heldObject.layer = 12;
                    }
                }
            }
        }
	}
}
