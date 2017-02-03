using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Sprite sprite;
    public List<GameObject> Waypoints;

    void Start()
    {
        foreach (var waypoint in Waypoints)
        {
            waypoint.GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }

    public void AddWaypoint()
    {
        var newPoint = new GameObject("point" + Waypoints.Count);
        newPoint.transform.parent = transform;
        newPoint.transform.position = new Vector3(Waypoints.Count, 0);
        newPoint.AddComponent<SpriteRenderer>().sprite = sprite;
        Waypoints.Add(newPoint);
    }

    public void RemoveWaypoint(int index)
    {
        var item = Waypoints[index];
        Waypoints.Remove(Waypoints[index]);
        DestroyImmediate(item);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < Waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
        }
    }
}
