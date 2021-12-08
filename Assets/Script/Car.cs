using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Buildings;
using System.Linq;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update、private Animator animation_controller;
    private CharacterController controller;
    //private Animator animation_controller;
    public GameObject car;
    public Vector3 moveDirection;
    // public float turnSpeed = 10.0f;
    public float turnSpeed = 50.0f;
    public float walking_velocity;
    public float movementSpeed;
    public float rotationSpeed = 10.0f;
    public Canvas gameCanvas;
    public Terrain terrain;
    public float terrain_length;
    public float terrain_width;
    internal float player_health = 3.0f;
    public Slider slider;
    public Text score_text;             // text UI element showing the score
    public Text timer;
    public float timeValue;
    public int packageNumber;
    public bool hasTimeUsed;
    public Gradient gradient;
    public Image health_bar_fill;
    //public float gravity = 20.0f;	
    void Start()
    {

        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 2.0f;
        movementSpeed = 0.0f;
        gameCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        player_health = 2.0f;
        hasTimeUsed = false;
        packageNumber = 0;
        timeValue = 60.0f;
        if (gameCanvas == null)
        {
            //Debug.Log("Doesn't exist");
        }
        gameCanvas.enabled = false;
        terrain = Terrain.activeTerrain;
        Vector3 terrainSize = terrain.terrainData.size;
        //Debug.Log("Terrain size is: " + terrainSize);
        terrain_length = terrainSize.x;
        terrain_width = terrainSize.z;
        slider.value = 1.0f;
        health_bar_fill.color = gradient.Evaluate(1.0f);
    }

    // void OnTriggerEnter(Collider col) {
    //     Debug.Log(col.gameObject.name);
    // }

    // Update is called once per frame
    void Update()
    {
        //if(transform.position.y <0){
        //transform.position.y = 0;
        //}
        if (transform.position.y > 0.0f)
        {
            //Vector3 lower_character = moveDirection * movementSpeed * Time.deltaTime;
            //lower_character.y = -100f; // hack to force her down
            //controller.Move(lower_character);
            float x = transform.position.x;
            float z = transform.position.z;
            transform.position = new Vector3(x, 1.5f, z);
        }
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;


        }
        else
        {
            timeValue = 0;
            hasTimeUsed = true;

        }
        DisplayTime(timeValue);
        //if picked up, package number ++;
        DisplayScore(packageNumber);
        slider.value = player_health / 2;

        health_bar_fill.color = gradient.Evaluate(slider.value);
        float zdirection = -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float xdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        moveDirection = new Vector3(xdirection, 0.0f, zdirection);


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (movementSpeed < 0)
            {
                movementSpeed = Mathf.Clamp(movementSpeed + 0.1f, -50.0f, 0);
            }
            else
            {
                movementSpeed = Mathf.Clamp(movementSpeed + 0.05f, 0, 50.0f);
            }


        }

        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

            if (movementSpeed > 0)
            {
                movementSpeed = Mathf.Clamp(movementSpeed - 0.1f, 0, 50.0f);
            }
            else
            {
                movementSpeed = Mathf.Clamp(movementSpeed - 0.025f, -15.0f, 0);
            }

        }
        else
        {
            if (movementSpeed > 0)
            {
                movementSpeed = Mathf.Clamp(movementSpeed - 0.025f, 0, movementSpeed);
            }
            else
            {
                movementSpeed = Mathf.Clamp(movementSpeed + 0.025f, movementSpeed, 0);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {

            transform.Rotate(0, Time.deltaTime * (-turnSpeed), 0);


        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {

            transform.Rotate(0, Time.deltaTime * turnSpeed, 0);


        }

        transform.position += moveDirection * Time.deltaTime * movementSpeed;

    }



    void DisplayTime(float time)
    {
        if (time < 0)
        {
            time = 0;
        }
        float finalTime = Mathf.Floor(time);
        timer.text = "Time Remaining: " + finalTime + "s";
    }
    void DisplayScore(int score)
    {
        score_text.text = "Package Picked:" + score;
    }

}



