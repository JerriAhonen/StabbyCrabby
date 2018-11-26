using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Tooltip("Reads current timescale.")] public float timeScale = 1f;
    public GUIStyle style;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.PageUp))
                ChangeScale(0.1f);
            else if (Input.GetKeyDown(KeyCode.PageDown))
                ChangeScale(-0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.PageUp))
            ChangeScale(0.05f);
        else if (Input.GetKeyDown(KeyCode.PageDown))
            ChangeScale(-0.05f);
        else if (Input.GetKeyDown(KeyCode.Home))
            SetTimescale(1f);
        else if (Input.GetKeyDown(KeyCode.End))
            SetTimescale(0f);

        timeScale = Time.timeScale;
    }

#if UNITY_EDITOR
    void OnGUI()
    {
        UnityEditor.EditorGUILayout.LabelField("Page up", "+0.05 Timescale", style);
        UnityEditor.EditorGUILayout.LabelField("Page down", "-0.05 Timescale", style);
        UnityEditor.EditorGUILayout.LabelField("+Shift", "+0.1 / -0.1", style);
        UnityEditor.EditorGUILayout.LabelField("Home", "Set timescale to 1", style);
        UnityEditor.EditorGUILayout.LabelField("End", "Set timescale to 0", style);
        UnityEditor.EditorGUILayout.LabelField("TIMESCALE", timeScale.ToString(), style);

    }
#endif

    private void ChangeScale(float amount)
    {
        float newValue = Time.timeScale + amount;
        Time.timeScale = Mathf.Clamp(newValue, 0f, 1f);
    }

    private void SetTimescale(float amount)
    {
        Time.timeScale = amount;
    }
}