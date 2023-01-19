using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody targetRb;

    private float minSpeed = 12.0f;
    private float maxSpeed = 15.0f;
    private float maxTorque = 10.0f;
    private float xRange = 4.0f;
    private float spawnPosY = -2.0f;

    public ParticleSystem explosionParticle;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();

        // Give the target an upward motion by randomize the position, force, and torque
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpanwPos();

    }

    // Called when left clicking target
    private void OnMouseDown()
    {
        if (gameManager.isGameAcitve)
        {
            // Desroy the object and spawn/play a explosions particle
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            // Update the socre based on the value of the target
            gameManager.UpdateScore(pointValue);
        }
    }

    // Trigger is called when colliding to the sensor below the game
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        // the object is not bad, deduct a life point
        if (gameManager.isGameAcitve && !gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(-1);
        }
    }

    // Called at start method, returns a vector3 upward, used as force direciton
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Called at start method, returns a random float, used as torque
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Called at start method, returns a random vector3, used as a spawning position
    Vector3 RandomSpanwPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), spawnPosY);
    }
}
