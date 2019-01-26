using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseObject: MonoBehaviour {
    const float progressBarMaxWidth = 0.95f;
    const float progressBarHeight = 0.7f;
    const float progressBarDepth = 1.0f;

    public float repairedPercent = 0.0f;
    public float repairProgressPerSecond = 1.0f;
    public float visibilityPercent = 0.0f;
    public float visibilityPerSecond = 1.0f;


    private GameObject brokenModel, fixedModel;
    private Slider repairBarSlider;

    public static GameObject viewCone;
    public static GameObject viewPlayer;

    private Bounds colliderBounds;

    bool isRepaired() {
        return (repairedPercent >= 100.0f);
    }

    bool wasSeen() {
        return (visibilityPercent >= 100.0f);
    }

    // Start is called before the first frame update
    void Start() {
        repairedPercent = 0;
        colliderBounds = gameObject.GetComponent<Collider>().bounds;
        viewCone = GameObject.FindGameObjectWithTag("viewCone");
        viewPlayer = GameObject.FindGameObjectWithTag("viewPlayer");

        foreach(Transform child in transform) {
            if(child.gameObject.tag == "broken") brokenModel = child.gameObject;
            if(child.gameObject.tag == "fixed") fixedModel = child.gameObject;
        }
        if(!brokenModel || !fixedModel) {
            Debug.LogError("Missing Model or Tag in Model...");
        }
    }

    // Update is called once per frame
    void Update() {
        if(!isRepaired()) {
            //if(colliderBounds.Intersects(viewPlayer.GetComponent<Collider>().bounds)) {
            repairedPercent += (repairProgressPerSecond * Time.deltaTime);
            //}
        }
        repairBarSlider.value = Mathf.FloorToInt(repairedPercent);

        if(!wasSeen()) {
            if(colliderBounds.Intersects(viewCone.GetComponent<Collider>().bounds)) {
                visibilityPercent += (visibilityPerSecond * Time.deltaTime);
            }
        }
    }
}
