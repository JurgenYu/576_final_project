using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public List<Vector3> warehouses_position;
    public GameObject house_prefab;
	//public float gravity = 20.0f;	
    void Start()
    {
        //animation_controller = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 2.0f;
        // movementSpeed = 5.0f;
        movementSpeed = 0.0f;
        //controller = GetComponent<CharacterController>();
        // car = GameObject.Find("Car_4_Blue");
        // Instantiate(car, new Vector3(6.0f, 1.1f, 3.0f), Quaternion.identity);
        gameCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        // house_prefab = GameObject.Find("Assets/warehouse/house.fbx");

        // if (house_prefab == null) {
        //     Debug.Log("Prefab not exists");
        // }

        if (gameCanvas == null) {
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

    // Update is called once per frame
    void Update()
    {
    //     float zdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
    //     float xdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float xdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        moveDirection = new Vector3(xdirection, 0.0f, zdirection);
		//character_controller.Move(movement_direction * velocity * Time.deltaTime);
		// Debug.Log("Moving direction is: "+ moveDirection);

		if(Input.GetKey(KeyCode.UpArrow)) {
             // transform.position += transform.forward * Time.deltaTime * movementSpeed;
             // moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
             // transform.position += moveDirection * Time.deltaTime * movementSpeed;
            //  if (movementSpeed < 5.0f) {
            //      movementSpeed += 0.1f;
            //  }
             movementSpeed = 50.0f;
			 //transform.position += moveDirection * Time.deltaTime * movementSpeed;
             // Debug.Log("Up pressed");
             // Debug.Log("Up pressed, moving direction is: "+ moveDirection);
			 
         }else if(Input.GetKeyUp(KeyCode.UpArrow)){
			 movementSpeed = 0.0f;
		 }
         else if(Input.GetKey(KeyCode.DownArrow)) {
             // transform.position -= transform.forward * Time.deltaTime * movementSpeed;
             //  moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
             movementSpeed = -5.0f;
             //transform.position += moveDirection * Time.deltaTime * movementSpeed;
         }else if(Input.GetKeyUp(KeyCode.DownArrow)){
			movementSpeed = 0.0f;
		 }
		 if(Input.GetKey(KeyCode.LeftArrow)) {
             // transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
             // transform.Rotate(Vector3.up * (-turnSpeed) * Time.deltaTime, Space.World);
             transform.Rotate(0, Time.deltaTime * (-turnSpeed), 0);
             // transform.rotation.eulerAngles.y = new Vector3(0.0f, Time.deltaTime*turnSpeed, 0.0f);
             // Debug.Log("Left pressed, moving direction is: "+ moveDirection);

         }
         else if(Input.GetKey(KeyCode.RightArrow)) {
              // transform.Rotate(0, Time.deltaTime * (-turnSpeed), 0);
              // transform.Rotate(Vector3.up * (turnSpeed) * Time.deltaTime, Space.World);
              transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
              // transform.rotation.eulerAngles.y = new Vector3(0.0f, Time.deltaTime*turnSpeed, 0.0f);
              // Debug.Log("Right pressed, moving direction is: "+ moveDirection);

         }else{}

        //controller.Move(moveDirection * movementSpeed * Time.deltaTime);
		transform.position += moveDirection * Time.deltaTime * movementSpeed;
        // Debug.Log("My position is: "+  transform.position);
	}

    void drawWareHouses(int totalWareHouses) {
        for (int i = 0; i < totalWareHouses; i++) {

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
                if (!warehouses_position.Contains(sample_wh_position)) {
                    int itr = 0;
                    for (itr = 0; itr < warehouses_position.Count; itr++) {
                        float distance = Vector3.Distance(sample_wh_position, warehouses_position[itr]);
                        if (distance < 30.0f) {
                            break;
                        }
                    }

                    if (itr == (warehouses_position.Count - 1)) {
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


            for (int x = 0; x < terrain_length; x++) {

            }
        }
    }
}



