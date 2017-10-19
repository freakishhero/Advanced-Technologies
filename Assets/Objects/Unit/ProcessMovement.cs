using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProcessMovement : MonoBehaviour {

    NavMeshAgent agent;
    UnitData data;
    public Vector3 destination;
    
	// Use this for initialization
	void Start () {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        data = this.gameObject.GetComponent<UnitData>();
        destination = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public Vector3 Destination
    {
        get
        {
            return destination;
        }
        set
        {
            destination = value;
        }

    }
}
