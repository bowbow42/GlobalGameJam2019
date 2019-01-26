using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    // TODO use this to modify movement outside
    public bool inSpace = false;

    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        // move up
        if ( Input.GetKey( KeyCode.W ) || Input.GetKey(KeyCode.UpArrow)) {
            dir += Vector3.forward * moveSpeed;
        }
        // move down
        if ( Input.GetKey( KeyCode.S ) || Input.GetKey( KeyCode.DownArrow ) ) {
            dir += Vector3.back * moveSpeed;
        }
        // move left
        if ( Input.GetKey( KeyCode.A ) || Input.GetKey( KeyCode.LeftArrow ) ) {
            dir += Vector3.left * moveSpeed;
        }
        // move right
        if ( Input.GetKey( KeyCode.D ) || Input.GetKey( KeyCode.RightArrow ) ) {
            dir += Vector3.right * moveSpeed;
        }
        transform.position += dir * Time.deltaTime;

        if ( dir.sqrMagnitude > 0.1f ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( dir, Vector3.up ), Time.deltaTime * 10f );
        }
    }
}
