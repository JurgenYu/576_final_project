using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Turret : MonoBehaviour
{
    private float shooting_delay; 
    private GameObject projectile_template;
    private Vector3 direction_from_turret_to_claire;
    private Vector3 shooting_direction;
	private Vector3 future_diravtion;
    private Vector3 projectile_starting_pos;
	private Vector3 claire_volecity;
	private Vector3 future_claire_pos;
	private Vector3 last_future_target;
    private float projectile_velocity;
    private bool claire_is_accessible;
	private float t;
	public static bool isAppleHitClaire;


    // Start is called before the first frame update
    void Start()
    {
        projectile_template = (GameObject)Resources.Load("Apple/Prefab/Apple", typeof(GameObject));  // projectile prefab
        if (projectile_template == null)
            Debug.LogError("Error: could not find the apple prefab in the project! Did you delete/move the prefab from your project?");
        shooting_delay = 0.5f;  
        projectile_velocity = 5.0f;
        direction_from_turret_to_claire = new Vector3(0.0f, 0.0f, 0.0f);
        projectile_starting_pos = new Vector3(0.0f, 0.0f, 0.0f);
		//future_direction = new Vector3(0.0f, 0.0f, 0.0f);
		claire_volecity = new Vector3();
        claire_is_accessible = false;
        StartCoroutine("Spawn");
		t = 0.0f;
		isAppleHitClaire = false;
		
		
    }

    // Update is called once per frame
    void Update()
    {
        GameObject CarObj = GameObject.Find("CarObj");
        if (CarObj == null)
            Debug.LogError("Error: could not find the game character 'Claire' in the scene. Did you delete the model Claire from your scene?");
        Vector3 claire_centroid = CarObj.GetComponent<CapsuleCollider>().bounds.center;
        Vector3 turret_centroid = GetComponent<Collider>().bounds.center;
        direction_from_turret_to_claire = claire_centroid - turret_centroid;
        direction_from_turret_to_claire.Normalize();
		

        RaycastHit hit;
        if (Physics.Raycast( turret_centroid, direction_from_turret_to_claire, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == CarObj)
            {
                ////////////////////////////////////////////////
                // WRITE CODE HERE:
                // implement deflection shooting
				//future_direction = claire_centroid;
				isAppleHitClaire = true;
				for(int i=0; i<10; i++){
					float old_t = t;
					t = (claire_centroid + t * claire_volecity - projectile_starting_pos).magnitude / projectile_velocity;
					
					//Debug.Log("Display t" + t);
					if(t - old_t < 0.003) break;
					
				}
				var future_claire_pos = claire_centroid + CarObj.transform.forward * CarObj.GetComponent<Car>().movementSpeed * t;
                Debug.Log(CarObj.GetComponent<Car>().moveDirection);
                // Debug.Log(future_claire_pos);
				shooting_direction = (future_claire_pos - turret_centroid).normalized;
				//target_position + t * target_velocity; 
		
				
				//shooting_direction = direction_from_turret_to_claire;
                 // this is a very simple heuristic for shooting, replace it
                ////////////////////////////////////////////////

                float angle_to_rotate_turret = Mathf.Rad2Deg * Mathf.Atan2(shooting_direction.x, shooting_direction.z);
                transform.eulerAngles = new Vector3(0.0f, angle_to_rotate_turret, 0.0f);
                Vector3 current_turret_direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 1.1f, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
                projectile_starting_pos = transform.position + 1.1f * current_turret_direction;  // estimated position of the turret's front of the cannon
                claire_is_accessible = true;
            }
            else
                claire_is_accessible = false;
				
			//isAppleHitClaire = false;	
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {            
            if (claire_is_accessible)
            {
                GameObject new_object = Instantiate(projectile_template, projectile_starting_pos, Quaternion.identity);
                new_object.GetComponent<Apple>().direction = shooting_direction;
                new_object.GetComponent<Apple>().velocity = projectile_velocity;
                new_object.GetComponent<Apple>().birth_time = Time.time;
                new_object.GetComponent<Apple>().birth_turret = transform.gameObject;
            }
            yield return new WaitForSeconds(shooting_delay); // next shot will be shot after this delay
        }
    }
}
