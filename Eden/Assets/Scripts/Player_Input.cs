using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour {

    ///PRIVATE///
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float fForceMax = 2;
    private Vector3 camRigVelocity;
    private float fVelocity = 0.025f;
    private float fFastVelocity = 0.5f;
    private bool bSpeedMax;

    ///PUBLIC///
    public GameObject CamRig;
    public Rigidbody rb_CamRig;
    public int iRotationZ;
    public Player_Input Other;

    // Use this for initialization
    void Start () {

        trackedObject = GetComponent<SteamVR_TrackedObject>();
        rb_CamRig = CamRig.GetComponent<Rigidbody>();
        camRigVelocity = rb_CamRig.velocity;
     }

    void Break ()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            /////////// FREINAGE X ///////////

            if (camRigVelocity.x > fFastVelocity)
            {
                camRigVelocity.x -= fFastVelocity / 2;
            }
            else if (camRigVelocity.x >= fVelocity && camRigVelocity.x < fFastVelocity)
            {
                camRigVelocity.x -= fVelocity;
            }
            else if (camRigVelocity.x < -fFastVelocity)
            {
                camRigVelocity.x += fFastVelocity / 2;
            }
            else if (camRigVelocity.x <= -fVelocity && camRigVelocity.x > fFastVelocity)
            {
                camRigVelocity.x += fVelocity;
            }
            else if (camRigVelocity.x > -fVelocity && camRigVelocity.x < fVelocity)
            {
                camRigVelocity.x = 0;
            }

            /////////// FREINAGE Y ///////////

            if (camRigVelocity.y > fFastVelocity)
            {
                camRigVelocity.y -= fFastVelocity / 2;
            }
            else if (camRigVelocity.y >= fVelocity && camRigVelocity.y < fFastVelocity)
            {
                camRigVelocity.y -= fVelocity;
            }
            else if (camRigVelocity.y < -fFastVelocity)
            {
                camRigVelocity.y += fFastVelocity / 2;
            }
            else if (camRigVelocity.y <= -fVelocity && camRigVelocity.y > fFastVelocity)
            {
                camRigVelocity.y += fVelocity;
            }
            else if (camRigVelocity.y > -fVelocity && camRigVelocity.y < fVelocity)
            {
                camRigVelocity.y = 0;
            }

            /////////// FREINAGE Z ///////////

            if (camRigVelocity.z > fFastVelocity)
            {
                camRigVelocity.z -= fFastVelocity / 2;
            }
            else if (camRigVelocity.z >= fVelocity && camRigVelocity.z < fFastVelocity)
            {
                camRigVelocity.z -= fVelocity;
            }
            else if (camRigVelocity.z < -fFastVelocity)
            {
                camRigVelocity.z += fFastVelocity / 2;
            }
            else if (camRigVelocity.z <= -fVelocity && camRigVelocity.z > fFastVelocity)
            {
                camRigVelocity.z += fVelocity;
            }
            else if (camRigVelocity.z > -fVelocity && camRigVelocity.z < fVelocity)
            {
                camRigVelocity.z = 0;
            }
            rb_CamRig.velocity = camRigVelocity;
        }
    }

    void CheckSpeed()
    {
        if (camRigVelocity.x > fForceMax || camRigVelocity.y > fForceMax || camRigVelocity.z > fForceMax ||
            camRigVelocity.x < -fForceMax || camRigVelocity.y < -fForceMax || camRigVelocity.z < -fForceMax)
            bSpeedMax = false;
        else
            bSpeedMax = true;
    }

    void Thruster()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Grip) && bSpeedMax)
        {
            rb_CamRig.AddForce(transform.forward / 50, ForceMode.Impulse);
            //AkSoundEngine.PostEvent("Thruster", gameObject);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && bSpeedMax)
        {
            AkSoundEngine.PostEvent("Thruster", gameObject);
        }
    }

    void Grab()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (Door.Instance.bCanGrab)
            {
                Door.Instance.Turn(1);
            }
        }
        else if (!device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && !Other.device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            Door.Instance.Turn(0);
    }

    // Update is called once per frame
    void Update () {

        iRotationZ = (int)transform.localEulerAngles.z;

        device = SteamVR_Controller.Input((int)trackedObject.index);
        camRigVelocity = rb_CamRig.velocity;
        CheckSpeed();
        Thruster();
        Break();
        Grab();
    }
}
