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
    public int warehouses_num;
    public int water_num;
    public int new_added_turret_num;
    // public GameObject game_level;


    // Start is called before the first frame update
    void Start()
    {
        warehouses_num = 0;
        water_num = 0;
        new_added_turret_num = 0;
        start = GameObject.Find("Canvas/Panel/StartButton").GetComponent<Button>();
        easy = GameObject.Find("Canvas/Panel/EasyButton").GetComponent<Button>();
        medium = GameObject.Find("Canvas/Panel/MediumButton").GetComponent<Button>();
        hard = GameObject.Find("Canvas/Panel/HardButton").GetComponent<Button>();
        // game_level = GameObject.Find("GamesLevels");
        start.onClick.AddListener(startButtonClicked);
        easy.onClick.AddListener(easyButtonClicked);
        medium.onClick.AddListener(mediumButtonClicked);
        hard.onClick.AddListener(hardButtonClicked);
        // GameObject.DontDestroyOnLoad(game_level);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startButtonClicked()
    {
        Debug.Log("start button clicked");
        SceneManager.LoadScene("Assets/Scenes/SampleScene.unity", LoadSceneMode.Single);
        Scene gameScene = SceneManager.GetSceneByPath("Assets/Scenes/SampleScene.unity");
        Debug.Log("Scene name is: " + gameScene.name);
        
        // SceneManager.SetActiveScene(gameScene);
    }

    private void OnDisable() {
        PlayerPrefs.SetInt("warehouses_number", warehouses_num);
        PlayerPrefs.SetInt("water_number", water_num);
        PlayerPrefs.SetInt("turrets_number", new_added_turret_num);
    }

    void Restore()
    {
        easy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        medium.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        hard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    void easyButtonClicked()
    {
        Debug.Log("easy button clicked");
        warehouses_num = 2;
        water_num = 4;
        new_added_turret_num = 2;
        Restore();
        easy.transform.localScale *= 1.1f;
    }

    void mediumButtonClicked()
    {
        Debug.Log("medium button clicked");
        warehouses_num = 4;
        water_num = 6;
        new_added_turret_num = 4;
        Restore();
        medium.transform.localScale *= 1.1f;
    }

    void hardButtonClicked()
    {
        Debug.Log("hard button clicked");
        warehouses_num = 9;
        water_num = 8;
        new_added_turret_num = 9;
        Restore();
        hard.transform.localScale *= 1.1f;
    }

}
