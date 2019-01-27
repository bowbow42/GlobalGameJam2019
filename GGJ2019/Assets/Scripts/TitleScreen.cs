using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    private Button playButton, helpButton, exitButton;

    private AssetBundle _myLoadedAssetBundle;

    // Start is called before the first frame update
    void Start() {
        playButton = GameObject.FindGameObjectWithTag("playButton").GetComponent<Button>();
        playButton.onClick.AddListener(loadGame);
        helpButton = GameObject.FindGameObjectWithTag("helpButton").GetComponent<Button>();
        helpButton.onClick.AddListener(showHelp);
        exitButton = GameObject.FindGameObjectWithTag("exitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(exitGame);

        //_myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
    }

    void loadGame() {
        SceneManager.LoadScene("HouseLevel_01");
        //SceneManager.LoadScene(_myLoadedAssetBundle.LoadAsset("HouseLevel_01"), LoadSceneMode.Single);
    }

    void showHelp() {

    }

    void exitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    // Update is called once per frame
    void Update() {

    }
}
