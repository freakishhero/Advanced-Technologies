using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafingPillar : MonoBehaviour {

    float movementSpeed;
    int movementModifier;

    [SerializeField]
    bool blocked;
    [SerializeField]
    private bool moveOnYAxis;
    [SerializeField]
    private bool moveOnXAxis;

    // Use this for initialization
    void Start () {
        movementSpeed = 5.0f;
        movementModifier = 1;
        blocked = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (blocked)
            movementSpeed = 0;
        else
            movementSpeed = 5.0f;

        if(moveOnXAxis && moveOnYAxis)
        {
            moveOnYAxis = false;
            moveOnXAxis = false;
            Debug.Log("Object cannot move on both axis. Turning X and Y to false.");
        }

        if(moveOnXAxis)
            transform.Translate(Vector2.right * movementSpeed * movementModifier * Time.deltaTime);

        if (moveOnYAxis)
            transform.Translate(new Vector3(0, 0, 1) * movementSpeed * movementModifier * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        switch (target.tag)
        {
            case "Wall":
                movementModifier *= -1;
                break;

        }
            
    }

    public bool Blocked
    {
        get
        {
            return blocked;
        }
        set
        {
            blocked = value;
        }
    }
}
