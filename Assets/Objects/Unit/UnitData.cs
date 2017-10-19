using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitData : MonoBehaviour {
   bool selected_flag;
   public bool patrol_flag; 
   public List<Vector3> waypoints;
   NavMeshAgent agent;
   public int currentWaypoint;
   private Light l;

    // Use this for initialization
    void Start () {
        waypoints = new List<Vector3>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        selected_flag = false;
        currentWaypoint = 0;
        l = this.gameObject.GetComponent<Light>();
    }

    //called when object is clicked
    //void OnMouseDown()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
        if (selected_flag)
        {
            l.intensity = 5;
        }
        else
        {
            l.intensity = 0;
        }

        if(patrol_flag)
        {
            Patrol();
        }
        else
        {
            moveThroughWayPoints();
        }        
    }

    public void addWayPoint(Vector3 point)
    {
        waypoints.Add(point);
    }

    public List<Vector3> getWayPoints()
    {
        return waypoints;
    }

    private void Patrol()
    {
        /*if (waypoints.Count < 1)
            return;

        if (waypoints.Count > 0)
            agent.SetDestination(waypoints[currentWaypoint]);*/
        if (Vector3.Distance(waypoints[0], this.gameObject.transform.position) < 1.0f)
        {
            agent.SetDestination(waypoints[1]);
        }

        if (Vector3.Distance(waypoints[1], this.gameObject.transform.position) < 1.0f)
        {
            agent.SetDestination(waypoints[0]);
        }
    }

    private void moveThroughWayPoints()
    {
        if (waypoints.Count < 1)
            return;

        if (waypoints.Count > 0)
            agent.SetDestination(waypoints[currentWaypoint]);

        if (Vector3.Distance(waypoints[currentWaypoint], this.gameObject.transform.position) < 1.0f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
            {
                waypoints.Clear();
                currentWaypoint = 0;
            }

        }
    }

    public bool Selected
    {
        get
        {
            return selected_flag;
        }
        set
        {
            selected_flag = value;
        }
    }

    public bool Patrolling
    {
        get
        {
            return patrol_flag;
        }
        set
        {
            patrol_flag = value;
        }
    }
}
