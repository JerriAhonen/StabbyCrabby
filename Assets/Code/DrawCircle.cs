using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public float radius;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}