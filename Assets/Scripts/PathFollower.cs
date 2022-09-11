using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private WaveParametersSo waveParameters;
    private List<Transform> waypoints;

    private int currentWaypointIndex = 1;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        waveParameters = enemySpawner.GetCurrentWave();
        waypoints = waveParameters.GetWaypoints();

        transform.position = waypoints[0].position;
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            Vector3 destination = waypoints[currentWaypointIndex].position;
            destination = new Vector3(destination.x, destination.y, 0);

            transform.position = Vector2.MoveTowards(
                transform.position,
                destination,
                waveParameters.speed * Time.deltaTime);

            if (transform.position == destination)
            {
                Debug.Log("next waypoint");
                currentWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
