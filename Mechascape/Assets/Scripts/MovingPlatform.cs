using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;  // Speed of the platform
    public int startingPoint;  // Which point the platform will start at
    public Transform[] points;  // List of points the platform will move to and from
    
    private int i; // Array Index
    
    void Start()
    {
        transform.position = points[startingPoint].position; // Sets starting position of the platform to whatever point "startingPoint" is set to
    }

    void Update()
    {
        // Checks distance between the platform and the point it is currently traveling to
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length) // Check if the platform was on the last point after the index increased
            {
                i = 0; // Resets Index
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       collision.transform.SetParent(null);
    }
}
