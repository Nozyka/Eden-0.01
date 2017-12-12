using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode_Cam : MonoBehaviour
{
    private float fSpeed = 2.5f;

    public GameObject CamRig;

    // Use this for initialization
    void Start()
    {

    }

    void Move()
    {
        if (Input.GetKey(KeyCode.Z))
            transform.Translate(Vector3.forward * Time.deltaTime * fSpeed);
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.left * Time.deltaTime * fSpeed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * fSpeed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * fSpeed);
    }

    void ReturnToVR()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CamRig.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ReturnToVR();
    }
}