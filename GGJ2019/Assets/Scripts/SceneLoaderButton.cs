using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderButton : MonoBehaviour
{
    public string sceneToLoad;
    private Button sceneLoaderButton;


    // Start is called before the first frame update
    void Start()
    {
        sceneLoaderButton = GetComponent<Button>();
        sceneLoaderButton.onClick.AddListener( LoadScene );
    }

    void LoadScene () {
        SceneManager.LoadScene( sceneToLoad );
    }

    void OnDestroy()
    {
        sceneLoaderButton.onClick.RemoveListener( LoadScene );
    }
}
