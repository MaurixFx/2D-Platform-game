using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        // First vector is the current waypoint
        // The idea is to compare the distance of the waypoint and the parent gameObject (example: moving platform)
        // if the distance is less than 1f means they are on the same position, so we have to switch to the next waypoint
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;

            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // We move the platform to the current waypoint position
        // speed 2f, means 2 game units per frame
        // We use Time.DeltaTime to make it universal, because there are different type of devices who can execute more or less frames per second
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
