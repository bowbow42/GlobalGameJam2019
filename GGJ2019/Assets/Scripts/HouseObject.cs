using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseObject: MonoBehaviour {
    const float _progressBarMaxWidth = 0.95f;
    const float _progressBarHeight = 0.7f;
    const float _progressBarDepth = 1.0f;

    public float _repairedPercent = 0.0f;
    public float _repairProgressPerSecond = 1.0f;
    public float _visibilityPercent = 0.0f;
    public float _visibilityPerSecond = 1.0f;


    private GameObject _brokenModel, _fixedModel;
    
    public GameObject _repairBarPrefab;
    private GameObject _repairBarInstance;
    private Slider _repairBarSlider;

    //public GameObject _viewCone;
    public GameObject _viewPlayer;

    private Bounds colliderBounds;

    bool isRepaired() {
        return (_repairedPercent >= 100.0f);
    }

    bool wasSeen() {
        return (_visibilityPercent >= 100.0f);
    }

    // Start is called before the first frame update
    void Start() {
        _repairedPercent = 0;
        colliderBounds = gameObject.GetComponent<Collider>().bounds;
        //viewCone = GameObject.FindGameObjectWithTag("viewCone");
        //viewPlayer = GameObject.FindGameObjectWithTag("viewPlayer");

        _repairBarInstance = Instantiate(_repairBarPrefab, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;
        Debug.Log(_repairBarInstance);
        _repairBarSlider = _repairBarInstance.GetComponentInChildren<Slider>();
        Debug.Log(_repairBarSlider);
        
        foreach(Transform child in transform) {
            if(child.gameObject.tag == "broken") _brokenModel = child.gameObject;
            if(child.gameObject.tag == "fixed") _fixedModel = child.gameObject;
        }
        if(!_brokenModel || !_fixedModel) {
            Debug.LogError("Missing Model or Tag in Model...");
        }
    }

    // Update is called once per frame
    void Update() {
        if(!isRepaired()) {
            if(colliderBounds.Intersects(_viewPlayer.GetComponent<Collider>().bounds)) {
                _repairedPercent += (_repairProgressPerSecond * Time.deltaTime);
                Debug.Log(_repairedPercent);
            }
        }
        _repairBarSlider.value = Mathf.FloorToInt(_repairedPercent);

        /*
        if(!wasSeen()) {
            if(colliderBounds.Intersects(_viewCone.GetComponent<Collider>().bounds)) {
                _visibilityPercent += (_visibilityPerSecond * Time.deltaTime);
            }
        }
        */
    }
}
