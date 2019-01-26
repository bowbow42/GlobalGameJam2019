using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0f,360f)]
    public float camAngle = 80f;

    [Range(1f,50f)]
    public float camDistance = 5f;

    public List<Transform> trackTargets;

    void Start()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        foreach(PlayerMovement pm in players ) {
            trackTargets.Add( pm.transform );
        }

        transform.rotation = Quaternion.Euler( camAngle, 0f, 0f );
    }

    void Update()
    {
        Vector3 pos = Vector3.zero;
        // Calculate mean position
        foreach(Transform t in trackTargets ) {
            pos += t.position;
        }

        pos /= trackTargets.Count;
        transform.rotation = Quaternion.Euler( camAngle, 0f, 0f );
        transform.position = Vector3.Lerp(transform.position, pos - transform.forward * camDistance,Time.deltaTime * 5f);
    }
}
