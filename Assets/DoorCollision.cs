using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Unit")
        {
            Debug.Log("Collision with unit");
            UnitData data = collision.gameObject.GetComponent<UnitData>();
            if (data.getClass() == UnitData.Class.Thief)
            {
                Destroy(gameObject);
                Debug.Log("Collision with thief DESTROY");
            }
        }

    }
}
