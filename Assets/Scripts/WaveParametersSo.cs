using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Parameters", menuName = "Wave Parameters")]
public class WaveParametersSo : ScriptableObject
{
    public Transform pathPrefab;
    public float speed;
    public float timeBetweenEnemies;
    public List<EnemyParameterSO> enemies = new List<EnemyParameterSO>();

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }



}
