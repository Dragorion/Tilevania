using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPathing : MonoBehaviour {

    BatWaveConfig batWaveConfig;
    List<Transform> batWaypoints;
    int waypointIndex = 0;
	// Use this for initialization
	void Start () {
        batWaypoints = batWaveConfig.GetWaypoints();
        transform.position = batWaypoints[waypointIndex].transform.position;
		
	}
	
    public void SetWaveConfig(BatWaveConfig batWaveConfig) 
    {
        this.batWaveConfig = batWaveConfig;  //setting local variable from this function 
    }


	// Update is called once per frame
	void Update ()
    {
        Move();

    }

    private void Move()
    {
        if (waypointIndex <= batWaypoints.Count - 1)
        {
            var targetPosition = batWaypoints[waypointIndex].transform.position;
            var moveThisFrame = batWaveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
