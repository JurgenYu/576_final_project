using System.Collections;
using System.Collections.Generic;
using Buildings;
using Parcels;
using UnityEngine;
using UnityEngine.UI;



public class Level : MonoBehaviour
{
    public List<Vector3> warehouses_position;
    public List<Vector3> waters_position;
    // Start is called before the first frame update
    public SortedList<Parcel, int> GlobalParcelsList;


    public GameObject house_prefab;
    public GameObject water_prefab;

    public Text CargoList;
    public Terrain terrain;
    public StartSceneKeyControl game_level;

    private int ItemNumber;

    private int TotalNumberDelivered;
    private int Score;


    void Start()
    {
        Score = 0;
        ItemNumber = 0;
        GlobalParcelsList = Parcel.GetParcelsList();
        game_level = GameObject.Find("GamesLevels").GetComponent<StartSceneKeyControl>();
        drawWareHouses(game_level.warehouses_num);
        drawWaters(game_level.water_num);
    }

    // Update is called once per frame
    void Update()
    {
        CargoList.text = "";
        foreach (Parcel p in this.GlobalParcelsList.Keys)
        {
            string ParcelInfo = "Name: " + p.Name + "\n" + "Deliver In: " + (int)p.CountDown + "\n" + "Score: " + p.Score + "\n" + "\n";
            CargoList.text += ParcelInfo;
            p.CountDown -= Time.deltaTime;
        }

    }

    public void AddParcel(Parcel NewParcel)
    {
        GlobalParcelsList.Add(NewParcel, ++ItemNumber);
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
                //Debug.Log("Strap in loop");
            }
            //Debug.Log("get out the loop");
            GameObject house = Instantiate(house_prefab, sample_wh_position, Quaternion.identity);
            house.name = "HOUSE" + i.ToString();
            house.AddComponent<BoxCollider>();
            house.AddComponent<House>();
            house.AddComponent<CapsuleCollider>();
            house.GetComponent<BoxCollider>().size = new Vector3(5.0f, 5.0f, 5.0f);
            house.GetComponent<CapsuleCollider>().radius = 10.0f;
            house.GetComponent<CapsuleCollider>().isTrigger = true;
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


            for (int x = 0; x < terrain.terrainData.size.x; x++)
            {

            }
        }
    }

    void drawWaters(int totalWaters)
    {
        for (int i = 0; i < totalWaters; i++)
        {

            // record the x position and z position of the warehouse 
            float water_x = 0;
            float water_z = 0;
            bool is_valid = false;
            Vector3 sample_water_position = new Vector3(0.0f, 0.0f, 0.0f);
            while (!is_valid) // try until a valid position is sampled
            {
                water_x = Random.Range(80, 400);
                water_z = Random.Range(80, 400);
                // Vector3 sample_wh_position = new Vector3(whx, 0.0f, whz);
                sample_water_position.x = water_x;
                sample_water_position.z = water_z;
                float water_height = Terrain.activeTerrain.SampleHeight(sample_water_position);
                sample_water_position.y = water_height;

                // check if this position is already in the list
                // if not in the list, check if two warehouses are too close to each other
                if (!warehouses_position.Contains(sample_water_position) && !waters_position.Contains(sample_water_position))
                {
                    int itr = 0;
                    for (itr = 0; itr < warehouses_position.Count; itr++)
                    {
                        // make sure the water is not very close to warehouses
                        float distance = Vector3.Distance(sample_water_position, warehouses_position[itr]);
                        if (distance < 30.0f)
                        {
                            break;
                        }
                    }

                    // if the water position is valid after checking all warehouses positions 
                    if (itr == warehouses_position.Count)
                    {
                        waters_position.Add(sample_water_position);
                        is_valid = true;
                    }
                    is_valid = true;
                }
                //Debug.Log("Strap in loop");
            }
            //Debug.Log("get out the loop");
            GameObject water = Instantiate(water_prefab, sample_water_position, Quaternion.identity);
            water.name = "WATER" + i.ToString();
            water.AddComponent<BoxCollider>();
            // house.AddComponent<House>();
            water.GetComponent<BoxCollider>().isTrigger = true;
            water.GetComponent<BoxCollider>().size = new Vector3(3.0f, 3.0f, 3.0f);
            water.AddComponent<ParticleSystem>();
            // house.AddComponent<House>();
            var ps = water.GetComponent<ParticleSystem>();
            var ex = ps.externalForces;
            var main = ps.main;
            main.gravityModifier = 0.0f;
            main.gravityModifierMultiplier = 0.0f;
            ex.enabled = false;
            main.startColor = new Color(0.0f, 1.0f, 0.0f, 0.7f);
            ps.Play();

            for (int x = 0; x < terrain.terrainData.size.x; x++)
            {

            }
        }
    }

    public void Delivered() {
        TotalNumberDelivered += GlobalParcelsList.Count;
        foreach (Parcel p in GlobalParcelsList.Keys) {
            Score += p.Score;
        }
        Debug.Log(Score);
        GlobalParcelsList.Clear();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.name);
    //     if (other.GetComponent<Collider>().name == "CarObj" && water_prefab.GetComponent<Collider>().name.Contains("WATER"))
    //     {
    //         Debug.Log("Water Collide with Car");
    //     }
    // }

}


