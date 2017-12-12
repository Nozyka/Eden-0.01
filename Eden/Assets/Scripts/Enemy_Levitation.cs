using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Levitation : MonoBehaviour {

    ////////// PRIVATE //////////
    private Vector3 posPlayer;
    private Vector3 posEnemyAgent;
    private Vector3 posEnemy;
    private float speed = 5;
    private bool bUP = true;
    private bool bDOWN = false;
    private bool bTouched = false;
    private float nTimer = 1;
    private float fLevitate = 0.005f;

    ////////// PULIC //////////
    public GameObject goPlayer;
    public GameObject goEnemyAgent;
    public bool bAttacked = false;

    

	// Use this for initialization
	void Start () {
        //transform.position =  transform.position + new Vector3(0, posPlayer.y, 0);  
	}
	
	// Update is called once per frame
	void Update () {
        
        posEnemy = transform.position;
        posEnemyAgent = goEnemyAgent.transform.position;
        posPlayer = goPlayer.transform.position;
        

        if (!bAttacked)
            Levitate();
    }

    public void RushPlayer()
    {
        if (transform.position.y < posPlayer.y && !bTouched)
        {
            posEnemy += new Vector3(0, 3f,0) * Time.deltaTime;
            posEnemy = new Vector3(posEnemyAgent.x, posEnemy.y, posEnemyAgent.z);
            transform.position = posEnemy;
        }
        else if (transform.position.y > posPlayer.y && !bTouched)
        {
            posEnemy -=  new Vector3(0, 3f, 0) * Time.deltaTime;
            posEnemy = new Vector3(posEnemyAgent.x, posEnemy.y, posEnemyAgent.z);
            transform.position = posEnemy;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ca touche");
        if (other.gameObject == goPlayer)
        {
            posEnemy = new Vector3(posEnemyAgent.x, posPlayer.y, posEnemyAgent.z);
            transform.position = posEnemy;
            bTouched = true;
            Debug.Log("ca touche");
        }
        if (other.gameObject != goPlayer)
        {
            Debug.Log("Je vais jouer une anim");
        }
    }

    public void Levitate()
    {
        if (bUP)
        {
            nTimer -= Time.deltaTime;
            if (nTimer <= 0)
            {
                nTimer = 1;
                bUP = false;
                bDOWN = true;
            }
        }
        else if (bDOWN)
        {
            nTimer -= Time.deltaTime;
            if (nTimer <= 0)
            {
                nTimer = 1;
                bUP = true;
                bDOWN = false;
            }
        }

        if (bUP)
            transform.Translate(0, fLevitate, 0);
        else if (bDOWN)
            transform.Translate(0, -fLevitate, 0);
    }

    private static Enemy_Levitation instance;
    public static Enemy_Levitation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("Enemy").GetComponent<Enemy_Levitation>();
            }

            return instance;
        }
    }
}
