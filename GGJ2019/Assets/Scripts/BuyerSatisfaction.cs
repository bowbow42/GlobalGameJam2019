using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuyerSatisfaction: MonoBehaviour {

    public Slider _buyerSatisfactionBar;
    
    public void increase(float percent) {
        if(_buyerSatisfactionBar.value < 1.0f)
            _buyerSatisfactionBar.value += percent;
    }
    public void decrease(float percent) {
        if(_buyerSatisfactionBar.value > 0.0f)
            _buyerSatisfactionBar.value -= percent;
    }

    // Start is called before the first frame update
    void Start() {
        _buyerSatisfactionBar.value = 0.5f;
    }

    // Update is called once per frame
    void Update() {
        if ( _buyerSatisfactionBar.value > 0.95f ) {
            SceneManager.LoadScene( "WinScene" );
        }else if ( _buyerSatisfactionBar.value < 0.05f ) {
            SceneManager.LoadScene( "LooseScene" );
        }
    }
}
