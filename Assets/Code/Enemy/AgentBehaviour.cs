using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour {
    // The controller for this agent.
    public AgentController _controller;

    private void Update() {
        // Get current agent data for the agent.
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        float velocity = _controller.agentVelocity;

        // Get current controller data for the agent.
        Vector3 separation = Vector3.zero;
        Vector3 alignment = _controller.transform.forward;
        Vector3 cohesion = _controller.transform.position;

        Collider[] agents = Physics.OverlapSphere(position,
            _controller.distanceBetweenAgents, _controller.agentLayer);

        foreach (var agent in agents) {
            // If the agent is this agent, it already has the current controller data.
            if (agent.gameObject == this.gameObject) {
                continue;
            }
            
            // Update the other agents in the flock.
            separation += CalculateSeparationVector(agent.transform);
            alignment += agent.transform.forward;
            cohesion += agent.transform.position;
        }

        float average = 1.0f / agents.Length;

        alignment *= average;
        cohesion *= average;
        cohesion = (cohesion - position).normalized;

        Vector3 newDirection = separation + alignment + cohesion;

        Quaternion newRotation = Quaternion.FromToRotation(Vector3.forward, newDirection.normalized);

        // Only update the agent rotation if the rotation has changed.
        if (newRotation != rotation)
            transform.rotation = Quaternion.Slerp(newRotation, rotation, Mathf.Exp(-4.0f * Time.deltaTime));

        transform.position = position + transform.forward * (velocity * Time.deltaTime);
    }

    // Calculates the separation vector between an agent in the flock and the current agent.
    private Vector3 CalculateSeparationVector(Transform target) {
        Vector3 separationVector = transform.position - target.transform.position;

        // The length of the vector.
        float distance = separationVector.magnitude;

        float scaler = Mathf.Clamp01(1.0f - distance / _controller.distanceBetweenAgents);

        return separationVector * (scaler / distance);
    }
}
