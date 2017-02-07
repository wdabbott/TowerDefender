using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Path path;
    public int speed;
    private int waypointReached = -1;
    private int currentTarget = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentTarget < path.Waypoints.Count)
	    {
	        var thisVector = new Vector2(transform.position.x, transform.position.y);
	        var targetVector = new Vector2(path.Waypoints[currentTarget].transform.position.x,
	            path.Waypoints[currentTarget].transform.position.y);

	        transform.position =
	            Vector2.MoveTowards(thisVector, targetVector, speed*Time.deltaTime);

	        var distance = Vector2.Distance(thisVector, targetVector);


	        if (distance < 0.05)
	        {
	            waypointReached = currentTarget;
	            currentTarget++;
	        }
	    }
	    else
	    {
	        Destroy(this.gameObject);
	    }
    }
}
