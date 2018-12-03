using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour {

    public int numberOfSpawns;
    public float spawnDistance;

    public float agentVelocity;
    public float agentRotationSpeed;
    public float distanceBetweenAgents;
    public LayerMask agentLayer;
    public GameObject agentToSpawn;

    private float time;
    public float moveTimer;

    public float maxDistance;
    private Vector3 targetLocation;

	private void Start () {
        Spawn(numberOfSpawns);
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            Spawn(numberOfSpawns);
        }

        time += Time.deltaTime;

        if (time > moveTimer) {
            targetLocation = CalculateRandomLocation(maxDistance);
            time = 0f;
        }

        Wander(targetLocation);
	}

    // Spawn the given number of objects to random locations withing spawning distance.
    private void Spawn(int numberToSpawn) {
        for (int index = 0; index < numberToSpawn; index++) {
            float x = Random.Range(-spawnDistance, spawnDistance);
            float y = Random.Range(-spawnDistance, spawnDistance);
            float z = Random.Range(-spawnDistance, spawnDistance);

            Vector3 randomLocation = new Vector3(x, y, z);

            Vector3 finalLocation = transform.position + randomLocation;

            GameObject agentClone = Instantiate(agentToSpawn, finalLocation, Quaternion.identity);

            agentClone.transform.rotation = Quaternion.Euler(randomLocation);
            agentClone.GetComponent<AgentBehaviour>()._controller = this;
        }
    }

    // Calculate a random location within the given distance.
    private Vector3 CalculateRandomLocation(float distance) {
        float x = Random.Range(-distance, distance);
        float z = Random.Range(-distance, distance);

        Vector3 location = new Vector3(transform.position.x + x, 1f, transform.position.z + z);

        return location;

    }

    // Move to the new target location.
    private void Wander(Vector3 target) {
        transform.position = Vector3.Slerp(transform.position, target, 3 * Time.deltaTime);
    }
}
