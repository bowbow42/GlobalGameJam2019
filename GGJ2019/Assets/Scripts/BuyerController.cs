using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerController : MonoBehaviour {

    public Camera _cam;
    public NavMeshAgent _agent;

    public Component _destinations;

    private bool _moving = true;
    private int _lastItem = -1;
    private float _timeSinceLastDest = 0;

    private void Start()
    {
        // initial path
        int nextIndex = Random.Range(0, _destinations.transform.childCount);
        while (nextIndex == _lastItem)
        {
            nextIndex = Random.Range(0, _destinations.transform.childCount);
        }
        _lastItem = nextIndex;
        // start moving 
        _agent.SetDestination(_destinations.transform.Find("Child" + _lastItem).transform.Find("moveposition").transform.position);
        _moving = true;
    }

    // Update is called once per frame
    void Update () {

        // send agent to new position
        if (!_moving && (Time.realtimeSinceStartup - _timeSinceLastDest) > 5)
        {
            // get new object
            int nextIndex = Random.Range(0, _destinations.transform.childCount);
            while (nextIndex == _lastItem)
            {
                nextIndex = Random.Range(0, _destinations.transform.childCount);
            }
            _lastItem = nextIndex;
            // start moving 
            _agent.SetDestination(_destinations.transform.Find("Child" + _lastItem).transform.Find("moveposition").transform.position);
            _moving = true;
        }

        // TODO if  obstacle detected
        //_agent.Stop();

        // check if agent has reached its destination
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance + 1)
            {
                // Does not always trigger when stoppingdistance is not increased
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    //Debug.Log("Destination Reached");
                    //Quaternion to = new Quaternion(0, 45, 0, 1);
                    //transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.deltaTime * 5f); // rotate to object

                    Vector3 direction = (_destinations.transform.Find("Child" + _lastItem).transform.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    Debug.Log(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                    // rotate to object
                    //Vector3 targetDir = _destinations.transform.Find("Child" + _lastItem).transform.position - transform.position;
                    //float step = 5f * Time.deltaTime;
                    //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                    //Debug.DrawRay(transform.position, newDir, Color.red);
                    //Debug.Log(newDir);
                    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newDir), Time.deltaTime * 5f);

                    _timeSinceLastDest = Time.realtimeSinceStartup;
                    _moving = false;
                }
            }
        }
    }
}
