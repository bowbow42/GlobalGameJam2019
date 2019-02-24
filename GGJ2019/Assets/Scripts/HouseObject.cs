using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseObject : MonoBehaviour
{
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

    bool isRepaired()
    {
        return (_repairedPercent >= 1.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _wasJudged = false;
        _repairedPercent = 0;
        colliderBounds = gameObject.GetComponent<Collider>().bounds;

        _repairBarInstance = Instantiate(_repairBarPrefab, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), transform.rotation) as GameObject;
        _repairBarSlider = _repairBarInstance.GetComponentInChildren<Slider>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "broken") _brokenModel = child.gameObject;
            if (child.gameObject.tag == "fixed") _fixedModel = child.gameObject;
        }

        if (!_brokenModel || !_fixedModel)
        {
            Debug.LogError("Missing Model or Tag in Model...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRepaired())
        {
            if (colliderBounds.Intersects(_viewPlayer.GetComponent<Collider>().bounds))
            {
                _repairedPercent += (_repairProgressPerSecond * Time.deltaTime);

                if (_brokenModel || _fixedModel)
                {
                    if (_repairedPercent > 0.8)
                    {
                        _brokenModel.SetActive(false);
                        _fixedModel.SetActive( true );
                    }
                }

                // start particle effect
                ParticleSystem[] pslist = GetComponentsInChildren<ParticleSystem>();

                int i = 0;
                foreach (ParticleSystem ps in pslist)
                {
                    i++;
                    ParticleSystem.EmissionModule em = ps.emission;
                    em.enabled = true;
                }
                //Debug.Log("Particle Running" + i);
            }
            else
            {
                // top particle effect
                ParticleSystem[] pslist = GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem ps in pslist)
                {
                    ParticleSystem.EmissionModule em = ps.emission;
                    em.enabled = false;
                }
            }
        }
        else
        {
            // top particle effect
            ParticleSystem[] pslist = GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem ps in pslist)
            {
                ParticleSystem.EmissionModule em = ps.emission;
                em.enabled = false;
            }
        }
        _repairBarSlider.value = Mathf.Min(1, _repairedPercent);

        // check if buyer sees object
        if (colliderBounds.Intersects(_viewCone.GetComponent<Collider>().bounds))
        {
            if (!_wasJudged)
            {
                if (_repairedPercent > 0.8)
                {
                    Debug.Log("Nice Object");
                    _buyer.GetComponent<BuyerSatisfaction>().increase(0.1f);
                }
                else
                {
                    Debug.Log("This One is destroyed");
                    _buyer.GetComponent<BuyerSatisfaction>().decrease(0.1f);
                }
                _wasJudged = true;
            }
        }
        else
        {
            _wasJudged = false;
        }
    }
}