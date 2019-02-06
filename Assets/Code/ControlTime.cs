using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTime : MonoBehaviour {
    
    /// <summary>
    /// Slow time (using Time.timeScale)
    /// </summary>
    /// <param name="duration">Duration of the slow-mo in seconds</param>
    /// <param name="speed">Time speed, 1 = normal time</param>
    /// <returns></returns>
    public IEnumerator SlowTime(float duration, float speed)
    {
        Debug.Log("Slow Time to " + speed + " for " + duration + " seconds.");

        float startSpeed = Time.timeScale;
        Time.timeScale = speed;

        float endSlowTime = Time.realtimeSinceStartup + duration;
        while (Time.realtimeSinceStartup < endSlowTime)
        {
            yield return 0;
        }

        Time.timeScale = startSpeed;
    }

    /// <summary>
    /// Stop time for a set amount of time
    /// </summary>
    /// <param name="duration">Duration of the time stop</param>
    /// <returns></returns>
    public IEnumerator StopTime(float duration)
    {
        Debug.Log("Stop Time for " + duration + " seconds.");

        float startSpeed = Time.timeScale;
        Time.timeScale = 0.0f;

        float endSlowTime = Time.realtimeSinceStartup + duration;
        while (Time.realtimeSinceStartup < endSlowTime)
        {
            yield return 0;
        }

        Time.timeScale = startSpeed;
    }

    /// <summary>
    /// First stop time and then slow time
    /// </summary>
    /// <param name="stopDuration">Duration of stop time</param>
    /// <param name="slowDuration">Duration of slow time</param>
    /// <param name="slowSpeed">Time speed during slow time</param>
    /// <returns></returns>
    public IEnumerator StopAndSlowTime(float stopDuration, float slowDuration, float slowSpeed)
    {
        float startSpeed = Time.timeScale;
        Time.timeScale = 0.0f;

        float endSlowTime = Time.realtimeSinceStartup + stopDuration;
        while (Time.realtimeSinceStartup < endSlowTime)
        {
            yield return 0;
        }

        Time.timeScale = slowSpeed;

        endSlowTime = Time.realtimeSinceStartup + slowDuration;
        while (Time.realtimeSinceStartup < endSlowTime)
        {
            yield return 0;
        }
        
        Time.timeScale = startSpeed;
    }

}
