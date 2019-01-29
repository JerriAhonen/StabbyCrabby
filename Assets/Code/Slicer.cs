using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour {
    public int SliceID { get; private set; }
    Vector3 _prevPos;
    Vector3 _pos;

    [SerializeField]
    public Vector3 OriginVector { get; set; }

    [SerializeField]
    public Vector3 DirectionVector { get; set; }

    private void Update() {
        _prevPos = _pos;
        _pos = transform.position;
    }

    public Vector3 Origin {
        get {
            Vector3 localShifted = transform.InverseTransformPoint(transform.position) + OriginVector;
            return transform.TransformPoint(localShifted);
        }
    }

    public Vector3 BladeDirection { get { return transform.rotation * DirectionVector.normalized; } }
    public Vector3 MoveDirection { get { return (_pos - _prevPos).normalized; } }

    public void BeginNewSlice() {
        SliceID = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
    }
}
