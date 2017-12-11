using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip_Manager : MonoBehaviour {

    public Rigidbody rb_Body;

    public Pull left;
    public Pull right;

    public VR_Hand handLeft;
    public VR_Hand handRight;


    // Update is called once per frame
    void FixedUpdate()
    {
        var lDevice = SteamVR_Controller.Input((int)left.controller.index);
        var rDevice = SteamVR_Controller.Input((int)right.controller.index);

        bool isGripped = left.bCanGrab || right.bCanGrab;

        if (isGripped)
        { 
            if (handLeft.heldObject == null)
            {
                if (left.bCanGrab && lDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
                {
                    rb_Body.isKinematic = true;
                    rb_Body.transform.position += (left.prevPos - left.transform.localPosition);
                }
                else if (left.bCanGrab && lDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    rb_Body.isKinematic = false;
                    rb_Body.velocity += (left.prevPos - left.transform.localPosition) / Time.deltaTime / 1.5f;
                }
            }

            if (handRight.heldObject == null)
            {
                if (right.bCanGrab && rDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
                {
                    rb_Body.isKinematic = true;
                    rb_Body.transform.position += (right.prevPos - right.transform.localPosition);
                }
                else if (right.bCanGrab && rDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    rb_Body.isKinematic = false;
                    rb_Body.velocity += (right.prevPos - right.transform.localPosition) / Time.deltaTime / 1.5f;
                }
            }
                

            else
            {
                rb_Body.isKinematic = false;
            }
        }
        left.prevPos = left.transform.localPosition;
        right.prevPos = right.transform.localPosition;
    }
}
