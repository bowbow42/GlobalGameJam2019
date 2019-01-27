using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseObject: MonoBehaviour {
    const float _progressBarMaxWidth = 0.95f;
    const float _progressBarHeight = 0.7f;
    const float _progressBarDepth = 1.0f;

    public float _repairedPercent = 0.0f;
    public float _repairProgressPerSecond = 0.1f;

    private GameObject _brokenModel, _fixedModel;
    
    public GameObject _repairBarPrefab;
    private GameObject _repairBarInstance;
    private Slider _repairBarSlider;

    public GameObject _viewCone;
    public GameObject _viewPlayer;
    public GameObject _buyer;
    public bool _wasJudged;

    private Bounds colliderBounds;

    bool isRepaired() {
        return (_repairedPercent >= 1.0f);
    }

    // Start is called before the first frame update
    void Start() {
        _wasJudged = false;
        _repairedPercent = 0;
        colliderBounds = gameObject.GetComponent<Collider>().bounds;

        _repairBarInstance = Instantiate(_repairBarPrefab, new Vector3(transform.position.x, transform.position.y+5, transform.position.z), transform.rotation) as GameObject;
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

                if (_brokenModel || _fixedModel)
                {
                    if (_repairedPercent > 0.8)
                    {
                        _brokenModel.active = false;
                        _fixedModel.active = true;
                    }
                }
            }
        }
        _repairBarSlider.value = Mathf.Min(1, _repairedPercent);
        
        // check if buyer sees object
        if(colliderBounds.Intersects(_viewCone.GetComponent<Collider>().bounds)) {
            if(!_wasJudged) {
                if(_repairedPercent > 0.8) {
                    Debug.Log("Nice Object");
                    _buyer.GetComponent<BuyerSatisfaction>().increase(0.1f);
                    _wasJudged = true;
                } else {
                    Debug.Log("This One is destroyed");
                    _buyer.GetComponent<BuyerSatisfaction>().decrease(0.1f);
                    _wasJudged = true;
                }
            }
        }
    }
}
