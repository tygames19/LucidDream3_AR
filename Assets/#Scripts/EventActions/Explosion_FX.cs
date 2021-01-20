using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Profiling;
using UnityEngine;

public class Explosion_FX : MonoBehaviour
{
    [SerializeField]
    GameObject deathParticle;

    [SerializeField]
    float rotationSpeed;

    //[SerializeField]
    //float cubeSize = 0.2f;

    //[SerializeField]
    //int cubesInRow = 5;

    //[SerializeField]
    //float explosionForce;

    //[SerializeField]
    //float explosionRadius;

    //[SerializeField]
    //float explosionUpward;

    //float cubesPivotDistance;
    //Vector3 cubesPivot;

    void Start()
    {
        //// calculate pivot distance
        //cubesPivotDistance = cubeSize * cubesInRow / 2;

        //// use this value to create pivot vector
        //cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    void Update()
    {
        // Rotate the cube at the random angle
        float randomAngle = Random.Range(0, 1);
        transform.Rotate(new Vector3(randomAngle, randomAngle, randomAngle) * (rotationSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Trig_floor"))
        {
            DeathParticleSpawn();
        }
    }

    public void DeathParticleSpawn()
    {
        GameObject colliderFloor = GameObject.FindWithTag("Trig_floor");
        Vector3 colliderPos = gameObject.transform.position + new Vector3 (0, colliderFloor.transform.position.y, 0);
        Instantiate(deathParticle, colliderPos, Quaternion.identity);
        Destroy(gameObject);
        //Debug.Log(transform.position);
    }

    //public void Explode()
    //{
    //    // loop 3 times to create 5x5x5 pieces in x,y,z coordinates
    //    for (int x = 0; x < cubesInRow; x++)
    //    {
    //        for (int y = 0; y < cubesInRow; y++)
    //        {
    //            for (int z = 0; z < cubesInRow; z++)
    //            {
    //                createPiece(x, y, z);
    //            }
    //        }
    //    }

    //    // get explosion position
    //    Vector3 explosionPos = transform.position;

    //    // get colliders in that position and radius
    //    Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

    //    // add explosion force to all colliders in that overlap sphere
    //    foreach (Collider hit in colliders)
    //    {
    //        //get rigidbody from the collider object
    //        Rigidbody rb = hit.GetComponent<Rigidbody>();

    //        if (rb != null)
    //        {
    //            // add explosion force to thjis body with given parameters
    //            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
    //        }
    //    }
    //}

    //void createPiece(int x, int y, int z)
    //{
    //    // create piece
    //    GameObject piece;
    //    piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //    // set piece position and scale
    //    piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) + cubesPivot;
    //    piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

    //    // add rigidBody and set mass
    //    piece.AddComponent<Rigidbody>();
    //    piece.GetComponent<Rigidbody>().mass = cubeSize;
    //}
}

