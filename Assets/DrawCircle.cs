using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public float radius;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}