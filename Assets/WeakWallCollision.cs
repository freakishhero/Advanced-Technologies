using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeakWallCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Unit")
        {
            Debug.Log("Collision with UNIT");
            UnitData data = collision.gameObject.GetComponent<UnitData>();
            if (data.getClass() == UnitData.Class.Builder)
            {
                Debug.Log("Collision WITH BUILDER - DESTROY");
                GetComponent<NavMeshObstacle>().carving = false;
                Destroy(gameObject);
            }
        }

    }
}
