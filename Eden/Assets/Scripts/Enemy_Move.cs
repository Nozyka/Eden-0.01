using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Move : MonoBehaviour
{

    ////////// PRIVATE //////////
    private bool bAttack = false;
    private NavMeshAgent NMA;
    private Vector3 result;

    ////////// PULIC //////////
    public Transform Destination;

    // Use this for initialization
    void Start()
    {

        NMA = this.GetComponent<NavMeshAgent>();

        if (NMA == null)
            Debug.Log("No Nav Mesh Agent");

    }

    private void GoToPlayer()
    {
        if (Destination != null)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(Destination.position, out hit, 100.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                //return true;
            }
            NMA.destination = result;
        }
        Enemy_Levitation.Instance.RushPlayer();
        Enemy_Levitation.Instance.bAttacked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (MicrophoneStuff.Instance.fLevel > 0.1f || Input.GetKey(KeyCode.Space))
            bAttack = true;

        if (bAttack)
            GoToPlayer();
    }
}
