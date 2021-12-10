using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    public Vector3 direction;
    public float velocity;
    public float birth_time;
    public GameObject birth_turret;
    public Text textDisplay;
    public int doNothing = 0;
    public GameObject car;
    //public int num_lives = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        car = GameObject.Find("CarObj");
        if (Time.time - birth_time > 10.0f)  // apples live for 10 sec
        {
            Destroy(transform.gameObject);
        }
        transform.position = transform.position + velocity * direction * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - birth_time > 0.5f)
        {
            if (other.GetComponent<Collider>().name == "CarObj" && Turret.isAppleHitClaire == true)
            {
                car.GetComponent<Car>().audio_source.PlayOneShot(car.GetComponent<Car>().carShotByAppleAudio, 1.0F);
                car.GetComponent<Car>().player_health = car.GetComponent<Car>().player_health - 0.1f;
                // Debug.Log("car has been shooted");
                // Debug.Log(car.GetComponent<Car>().player_health);
                Destroy(transform.gameObject);
                Turret.isAppleHitClaire = false;
            }
            else if (other.GetComponent<Collider>().name == "Apple" || other.GetComponent<Collider>().gameObject == birth_turret)
            {
                doNothing++;
            }
            else
            {
                //destroy the apple;
                Destroy(transform.gameObject);

            }
        }

    }
}
