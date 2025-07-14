using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private float Speed;

    private int CurrentWaypoint = 0;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == Waypoints[CurrentWaypoint].position)
            CurrentWaypoint = (CurrentWaypoint + 1) % Waypoints.Length;

        transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentWaypoint].position, Speed * Time.deltaTime);
    }
}
