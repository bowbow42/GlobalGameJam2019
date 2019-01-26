using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerController : MonoBehaviour {

    public Camera _cam;
    public NavMeshAgent _agent;

    public Component _destinations;

    private bool _moving = false;
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
        _agent.updateRotation = true;
    }

    // Update is called once per frame
    void Update () {

        //  send agent to new position
        if (!_moving && (Time.realtimeSinceStartup - _timeSinceLastDest) > 5)
        {
            Debug.Log(_timeSinceLastDest);
            Debug.Log(Time.realtimeSinceStartup);
            Debug.Log("new position please");

            // get new object
            int nextIndex = Random.Range(0, _destinations.transform.childCount);
            while (nextIndex == _lastItem)
            {
                nextIndex = Random.Range(0, _destinations.transform.childCount);
            }
            _lastItem = nextIndex;
            // start moving 
            _agent.SetDestination(_destinations.transform.Find("Child" + nextIndex).transform.Find("moveposition").transform.position);
            _moving = true;
            _agent.updateRotation = true;
        }

        // TODO if  obstacle detected
        //_agent.Stop();

        // check if agent has reached its destination
        if (!_agent.pathPending && _moving)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance + 1)
            {
                // Does not always trigger when stoppingdistance is not increased
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    _agent.updateRotation = false;
                    _timeSinceLastDest = Time.realtimeSinceStartup;
                    _moving = false;
                }
            }
        }

        if (!_moving)
        {
            Vector3 direction = (_destinations.transform.Find("Child" + _lastItem).transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
