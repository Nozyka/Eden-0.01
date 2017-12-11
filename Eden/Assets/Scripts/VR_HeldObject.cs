using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VR_HeldObject : MonoBehaviour {

    [HideInInspector]
    public VR_Controller parent;
}
