using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneKeyControl : MonoBehaviour
{
    public Button start;
    public Button easy;
    public Button medium;
    public Button hard;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("Canvas/Panel/StartButton").GetComponent<Button>();
        easy = GameObject.Find("Canvas/Panel/EasyButton").GetComponent<Button>();
        medium = GameObject.Find("Canvas/Panel/MediumButton").GetComponent<Button>();
        hard = GameObject.Find("Canvas/Panel/HardButton").GetComponent<Button>();
        start.onClick.AddListener(startButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startButtonClicked() {
        Debug.Log("start button clicked");
        SceneManager.LoadScene("Assets/Scenes/SampleScene.unity", LoadSceneMode.Single);
        Scene gameScene = SceneManager.GetSceneByPath("Assets/Scenes/SampleScene.unity");
        Debug.Log("Scene name is: " + gameScene.name);
        // SceneManager.SetActiveScene(gameScene);
    }
}
