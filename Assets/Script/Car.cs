using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Buildings;

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
	public GameObject scroll_bar;
	public Text score_text;             // text UI element showing the score
	public Text timer;
	public float timeValue;
	public int packageNumber;
    public List<Vector3> warehouses_position;
    public GameObject house_prefab;
	public bool hasTimeUsed;
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
            Debug.Log("Doesn't exist");
        }
        gameCanvas.enabled = false;
        terrain = Terrain.activeTerrain;
        Vector3 terrainSize = terrain.terrainData.size;
        Debug.Log("Terrain size is: " + terrainSize);
        terrain_length = terrainSize.x;
        terrain_width = terrainSize.z;
        drawWareHouses(3);
    }

    // void OnTriggerEnter(Collider col) {
    //     Debug.Log(col.gameObject.name);
    // }

    // Update is called once per frame
    void Update()
    {
		if(timeValue>0){
			timeValue -= Time.deltaTime;
			
			
		}else{
			timeValue = 0;
			hasTimeUsed = true;
			
		}
		DisplayTime(timeValue);
		//if picked up, package number ++;
		DisplayScore(packageNumber);
 
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

    void drawWareHouses(int totalWareHouses)
    {
        for (int i = 0; i < totalWareHouses; i++)
        {

            // record the x position and z position of the warehouse 
            float whx = 0;
            float whz = 0;
            bool is_valid = false;
            Vector3 sample_wh_position = new Vector3(0.0f, 0.0f, 0.0f);
            while (!is_valid) // try until a valid position is sampled
            {
                whx = Random.Range(80, 400);
                whz = Random.Range(80, 400);
                // Vector3 sample_wh_position = new Vector3(whx, 0.0f, whz);
                sample_wh_position.x = whx;
                sample_wh_position.z = whz;
                float wh_height = Terrain.activeTerrain.SampleHeight(sample_wh_position);
                sample_wh_position.y = wh_height;

                // check if this position is already in the list
                // if not in the list, check if two warehouses are too close to each other
                if (!warehouses_position.Contains(sample_wh_position))
                {
                    int itr = 0;
                    for (itr = 0; itr < warehouses_position.Count; itr++)
                    {
                        float distance = Vector3.Distance(sample_wh_position, warehouses_position[itr]);
                        if (distance < 30.0f)
                        {
                            break;
                        }
                    }

                    if (itr == (warehouses_position.Count - 1))
                    {
                        warehouses_position.Add(sample_wh_position);
                        is_valid = true;
                    }
                    is_valid = true;
                }
                Debug.Log("Strap in loop");
            }
            Debug.Log("get out the loop");
            GameObject house = Instantiate(house_prefab, sample_wh_position, Quaternion.identity);
            house.name = "HOUSE" + i.ToString();
            house.AddComponent<BoxCollider>();
            house.AddComponent<House>();
            house.GetComponent<BoxCollider>().isTrigger = true;
            house.GetComponent<BoxCollider>().size = new Vector3(3.0f, 3.0f, 3.0f);
            house.AddComponent<ParticleSystem>();
            var ps = house.GetComponent<ParticleSystem>();
            var ex = ps.externalForces;
            var main = ps.main;
            main.gravityModifier = 0.0f;
            main.gravityModifierMultiplier = 0.0f;
            ex.enabled = false;
            main.startColor = new Color(0.0f, 1.0f, 0.0f, 0.7f);
            ps.Play();
            // house.AddComponent<House>();
			scroll_bar.GetComponent<Scrollbar>().size = player_health;
			if (player_health < 0.5f)
			{
				ColorBlock cb = scroll_bar.GetComponent<Scrollbar>().colors;
				cb.disabledColor = new Color(1.0f, 0.0f, 0.0f);
				scroll_bar.GetComponent<Scrollbar>().colors = cb;
			}
			else
			{
				ColorBlock cb = scroll_bar.GetComponent<Scrollbar>().colors;
				cb.disabledColor = new Color(0.0f, 1.0f, 0.25f);
				scroll_bar.GetComponent<Scrollbar>().colors = cb;
			}

            for (int x = 0; x < terrain_length; x++)
            {

            }
        }
    }
	void DisplayTime(float time){
		if(time<0){
			time = 0;
		}
		float finalTime = Mathf.Floor(time);
		timer.text = "Time Remaining: " + finalTime + "s";
	}
	void DisplayScore(int score){
		score_text.text = "Package Picked:" + score;
	}
	
}



