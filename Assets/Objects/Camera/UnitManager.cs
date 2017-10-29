using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitManager : MonoBehaviour {

    ProcessMovement movement;
    public GameObject[] units;
    private UnitData data;
    public GameObject location;
    public Vector3 target;

    // Use this for initialization
    void Start () {
        target = Vector3.zero;
        units = GameObject.FindGameObjectsWithTag("Unit");
        movement = this.gameObject.GetComponent<ProcessMovement>();
    }

    bool SelectUnit()
    {
        target = Input.mousePosition;
        target.z = 0.6f;
        Ray ray = Camera.main.ScreenPointToRay(target);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Unit")
            {
                Debug.DrawLine(ray.origin, hit.point);
                data = hit.collider.GetComponent<UnitData>();
                if (data.Selected)
                    data.Selected = false;
                else
                    data.Selected = true;

                return true;
            }
        }
        return false;
    }

    void createWayPoint(GameObject unit, Vector3 location)
    {
        data = unit.GetComponent<UnitData>();
        data.addWayPoint(location);
    }

    void MovementRequest()
    {
        target = Input.mousePosition;
        target.z = 0.6f;
        Ray ray = Camera.main.ScreenPointToRay(target);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);

            if (hit.collider.tag != "Unit")
            {

                

                location.transform.position = hit.point;

                if (hit.collider.tag == "Door" || hit.collider.tag == "Weak Wall")
                    location.transform.position = hit.collider.gameObject.transform.position;

                foreach (GameObject unit in units)
                {
                    data = unit.GetComponent<UnitData>();
                    if (Vector3.Distance(unit.transform.position, location.transform.position) > 2.0f)
                    {
                        if(Input.GetKey("left shift"))
                        {
                            if(data.Selected)
                            {
                                data.Patrolling = false;
                                createWayPoint(unit, location.transform.position);
                            }
                        }
                       /* else if(Input.GetKey("p"))
                        {
                            if (data.Selected)
                            {
                                data.getWayPoints().Clear();
                                data.Patrolling = true;
                                createWayPoint(unit, unit.transform.position);
                                createWayPoint(unit, location.transform.position);
                            }
                        }*/
                        else
                        {
                            if (data.Selected)
                            {
                                data.Patrolling = false;
                                data.getWayPoints().Clear();
                                createWayPoint(unit, location.transform.position);
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetKey("left ctrl") && Input.GetKey("a"))
        {
            foreach (GameObject unit in units)
            {
                data = unit.GetComponent<UnitData>();
                data.Selected = true;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if(SelectUnit())
            {
                //Unit is selected and added to the number of units
                //we currently have, if the raycast hits a unit
            }
            else
            {
                foreach (GameObject unit in units)
                {
                   data = unit.GetComponent<UnitData>();
                   data.Selected = false;
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            MovementRequest();
        }

	}
}
