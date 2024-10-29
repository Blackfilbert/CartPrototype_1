using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCartMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    private float speed = 1f;

    private void Start() {
        lastWaypointSwitchTime = Time.time; 
    }

    private void Update() {
        MovementLogic();
    }

    private void MovementLogic() {
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if(gameObject.transform.position.Equals(endPosition)) {
            if(currentWaypoint < waypoints.Length - 2) {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }
            else {
                Destroy(gameObject);
            }
        }
    }

    private void RotateIntoMoveDirection() {
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = newEndPosition - newStartPosition;

        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(x, y) * 180 / Mathf.PI;

        GameObject playerCartBody = transform.Find("CartBody").gameObject;
        playerCartBody.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.back);
    }

    public float DistanceToGoal() {
        float distance = 0f;
        distance += Vector3.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);
        for(int i = currentWaypoint + 1; i < waypoints.Length - 1; i++) {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }
}
