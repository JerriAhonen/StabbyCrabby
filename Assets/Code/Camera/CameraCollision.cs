using UnityEngine;

public class CameraCollision : MonoBehaviour {

    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;

    private Vector3 _dollyDir;
    private float _distance;

    void Awake()
    {
        _dollyDir = transform.localPosition.normalized;  // Direction relative to the parent object
        _distance = transform.localPosition.magnitude;   // Relative distance to the parent object
    }
    
	void Update () {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(_dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            _distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }
        else
        {
            _distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, 
                                                _dollyDir * _distance, 
                                                Time.deltaTime * smooth);
	}
}
