using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodManager : MonoBehaviour {

    public GameObject CamRig;
    public GameObject CamGodMod;

    // Use this for initialization
    void Start () {
		
	}

    void GodMode()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CamGodMod.SetActive(true);
            CamRig.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {

        GodMode();

    }
}
