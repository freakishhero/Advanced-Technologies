using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitData : MonoBehaviour {
    
    //flags
    bool selected_flag;
    bool patrol_flag;
    bool waypoint_marker_flag;

    public enum Class
    {
        Thief = 0,
        Builder = 1,
        Defender = 2
    }
    [SerializeField]
    Class unit_class;

    //waypoints
    public List<Vector3> waypoints;
    public int currentWaypoint;

    //selection light
    private Light l;

    //waypoint markers
    private GameObject waypoint;
    private Light light;

    //Reference to the navmesh agent of the
    //object this script is attached to.
    NavMeshAgent agent;

    Vector3 spawnLocation;

    // Use this for initialization
    void Start () {
        waypoints = new List<Vector3>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.velocity = Vector3.zero;
        selected_flag = false;
        currentWaypoint = 0;
        l = this.gameObject.GetComponent<Light>();
        spawnLocation = transform.position;
        InitialiseClass();
    }

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
        if (waypoints.Count < 1)
            return;

        if (waypoints.Count > 0)
            agent.SetDestination(waypoints[currentWaypoint]);

        if (Vector3.Distance(waypoints[0], this.gameObject.transform.position) < 2.5f)
        {
            agent.SetDestination(waypoints[1]);
        }

        if (Vector3.Distance(waypoints[1], this.gameObject.transform.position) < 2.5f)
        {
            agent.SetDestination(waypoints[0]);
        }

        Debug.DrawLine(waypoints[0], waypoints[1]);
    }

    private void moveThroughWayPoints()
    {
        if (!waypoint_marker_flag)
        {
            waypoint = new GameObject();
            waypoint.name = name + " waypoint marker";
            waypoint_marker_flag = true;
        }

        if (waypoints.Count < 1)
            return;
  
        if (waypoints.Count > 0)
        {
            agent.isStopped = false;
            agent.acceleration = 40;
            agent.SetDestination(waypoints[currentWaypoint]);
            createPathMarkerLight();
        }

        if (Vector3.Distance(waypoints[currentWaypoint], this.gameObject.transform.position) < 2.5f)
        {
            Destroy(waypoint);
            waypoint_marker_flag = false;
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
            {
                Destroy(light);
                waypoints.Clear();
                currentWaypoint = 0;
                agent.velocity = Vector3.zero;
                agent.acceleration = 0;
                agent.isStopped = true;
            }

        }
    }

    void createPathMarkerLight()
    {
        Vector3 lightloc = new Vector3(waypoints[currentWaypoint].x, waypoints[currentWaypoint].y + 4, waypoints[currentWaypoint].z);
        waypoint.transform.position = lightloc;

        if (!waypoint.GetComponent<Light>())
        {
            light = waypoint.AddComponent<Light>();
            light.color = Color.green;
            light.range = 5;
            light.intensity = 500;
        }
        if (waypoint.GetComponent<Light>() && selected_flag == false)
        {
            light.intensity = 0;
        }
        if (waypoint.GetComponent<Light>() && selected_flag == true)
        {
            light.intensity = 500;
        }
    }

    void InitialiseClass()
    {
        switch (unit_class)
        {
            case Class.Defender:
                l.color = Color.red;
                break;
            case Class.Thief:
                l.color = Color.magenta;
                break;
            case Class.Builder:
                l.color = Color.yellow;
                break;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject target = collision.gameObject;
        switch (target.tag)
        {
            case "Hazard":
                StrafingPillar data = target.GetComponent<StrafingPillar>();
                if (unit_class != Class.Defender)
                {
                    Destroy(light);
                    transform.position = spawnLocation;
                    waypoints.Clear();
                    agent.isStopped = true;
                    Debug.Log("Hit hazard - resetting location of" + name);
                }
                
                if(unit_class == Class.Defender)
                {
                    data.Blocked = true;
                }
                break;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject target = collision.gameObject;
        switch (target.tag)
        {
            case "Hazard":
                StrafingPillar data = target.GetComponent<StrafingPillar>();
                if (unit_class == Class.Defender)
                {
                  data.Blocked = false;
                }
                break;
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

    public Class getClass()
    {
        return unit_class;
    }
}
