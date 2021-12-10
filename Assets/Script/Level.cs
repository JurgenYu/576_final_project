using System.Collections;
using System.Collections.Generic;
using Buildings;
using Parcels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;



public class Level : MonoBehaviour
{
    // public List<Vector3> warehouses_position = new List<Vector3>() { 
    //     new Vector3(100.0f, 0.0f, 100.0f), new Vector3(100.0f, 0.0f, 400.0f), new Vector3(100.0f, 0.0f,700.0f),
    //     new Vector3(400.0f, 0.0f, 100.0f), new Vector3(400.0f, 0.0f, 400.0f), new Vector3(400.0f, 0.0f, 700.0f),
    //     new Vector3(700.0f, 0.0f, 100.0f), new Vector3(700.0f, 0.0f, 400.0f), new Vector3(700.0f, 0.0f, 700.0f)
    // };
    
    public List<Vector3> warehouses_position;

    // public List<Vector3> vector3s = new List<Vector3>() { new Vector2(1, 0), new Vector3(2, 9), new Vector3(5, 7,10) };
    public List<Vector3> waters_position;
    public List<Vector3> turrets_position;
    public List<Vector3> actual_warehouses_position_in_world;
    // Start is called before the first frame update
    public SortedList<Parcel, int> GlobalParcelsList;

    public GameObject turret_prefab;
    public GameObject house_prefab;
    public GameObject water_prefab;

    public Text CargoList;
    public Terrain terrain;
    public StartSceneKeyControl game_level;
    public Text ScoreText;
    public Text Prompt;
    private int NextUpdate;

    private int ItemNumber;

    private int TotalNumberDelivered;
    public int Score;


    void Start()
    {
        Score = 0;
        ItemNumber = 0;
        GlobalParcelsList = Parcel.GetParcelsList();
        game_level = GameObject.Find("GamesLevels")? GameObject.Find("GamesLevels").GetComponent<StartSceneKeyControl>():null;
        warehouses_position = new List<Vector3>() { 
        new Vector3(100.0f, 0.0f, 100.0f), new Vector3(100.0f + 100.0f, 0.0f, 400.0f), new Vector3(100.0f + 150.0f, 0.0f ,700.0f + 150.0f),
        new Vector3(400.0f, 0.0f, 100.0f + 200.0f), new Vector3(400.0f + 150.0f, 0.0f, 400.0f + 50.0f), new Vector3(400.0f, 0.0f, 700.0f + 100.0f),
        new Vector3(700.0f + 50.0f, 0.0f, 100.0f + 150.0f), new Vector3(700.0f + 150.0f, 0.0f, 400.0f + 150.0f), new Vector3(700.0f, 0.0f, 700.0f)
    };
        Debug.Log("Length of walehouses is: " + warehouses_position.Count);

        // fixed turret position
        turrets_position = new List<Vector3>() {
            new Vector3(300.0f, 0.0f, 820.0f), new Vector3(650.0f, 0.0f, 700.0f),
            new Vector3(900.0f, 0.0f, 450.0f), new Vector3(600.0f, 0.0f, 150.0f),
            new Vector3(600.0f, 0.0f, 200.0f)
        };

        drawWareHouses(game_level? game_level.warehouses_num:5);
        drawWaters(game_level? game_level.water_num:5);
        drawTurrets(game_level? game_level.new_added_turret_num:5);
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
        ScoreText.text = "Score: " + Score;
        if (Time.time > NextUpdate) {
            Prompt.text = "";
        }

    }

    public void AddParcel(Parcel NewParcel)
    {
        Prompt.text = "New Deliveries Picked Up!";
        GlobalParcelsList.Add(NewParcel, ++ItemNumber);
        NextUpdate = Mathf.FloorToInt(Time.time + 5);
    }

    public void Delivered() {
        TotalNumberDelivered += GlobalParcelsList.Count;
        Prompt.text = "Delivered " + GlobalParcelsList.Count + " Packages";
        NextUpdate = Mathf.FloorToInt(Time.time + 5);
        foreach (Parcel p in GlobalParcelsList.Keys) {
            Score += p.Score;
        }
        Debug.Log(Score);
        GlobalParcelsList.Clear();
    }

    // void drawWareHouses(int totalWareHouses)
    // {
    //     Debug.Log(totalWareHouses);
    //     for (int i = 0; i < totalWareHouses; i++)
    //     {

    //         // record the x position and z position of the warehouse 
    //         float whx = 0;
    //         float whz = 0;
    //         bool is_valid = false;
    //         Vector3 sample_wh_position = new Vector3(0.0f, 0.0f, 0.0f);
    //         while (!is_valid) // try until a valid position is sampled
    //         {
    //             whx = Random.Range(80, 400);
    //             whz = Random.Range(80, 400);
    //             // Vector3 sample_wh_position = new Vector3(whx, 0.0f, whz);
    //             sample_wh_position.x = whx;
    //             sample_wh_position.z = whz;
    //             float wh_height = Terrain.activeTerrain.SampleHeight(sample_wh_position);
    //             sample_wh_position.y = wh_height;

    //             // check if this position is already in the list
    //             // if not in the list, check if two warehouses are too close to each other
    //             if (!warehouses_position.Contains(sample_wh_position))
    //             {
    //                 int itr = 0;
    //                 for (itr = 0; itr < warehouses_position.Count; itr++)
    //                 {
    //                     float distance = Vector3.Distance(sample_wh_position, warehouses_position[itr]);
    //                     if (distance < 30.0f)
    //                     {
    //                         break;
    //                     }
    //                 }

    //                 if (itr == (warehouses_position.Count - 1))
    //                 {
    //                     warehouses_position.Add(sample_wh_position);
    //                     is_valid = true;
    //                 }
    //                 is_valid = true;
    //             }
    //             //Debug.Log("Strap in loop");
    //         }
    //         //Debug.Log("get out the loop");
    //         GameObject house = Instantiate(house_prefab, sample_wh_position, Quaternion.identity);
    //         house.name = "HOUSE" + i.ToString();
    //         house.AddComponent<BoxCollider>();
    //         house.AddComponent<House>();
    //         house.AddComponent<CapsuleCollider>();
    //         house.GetComponent<BoxCollider>().size = new Vector3(5.0f, 5.0f, 5.0f);
    //         house.GetComponent<CapsuleCollider>().radius = 10.0f;
    //         house.GetComponent<CapsuleCollider>().isTrigger = true;
    //         house.AddComponent<ParticleSystem>();
    //         var ps = house.GetComponent<ParticleSystem>();
    //         var ex = ps.externalForces;
    //         var main = ps.main;
    //         main.gravityModifier = 0.0f;
    //         main.gravityModifierMultiplier = 0.0f;
    //         ex.enabled = false;
    //         main.startColor = new Color(0.0f, 1.0f, 0.0f, 0.7f);
    //         ps.Play();
    //         // house.AddComponent<House>();


    //         for (int x = 0; x < terrain.terrainData.size.x; x++)
    //         {

    //         }
    //     }
    // }

     void drawWareHouses(int totalWareHouses)
    {
        Debug.Log("Total warehouses number is :" + totalWareHouses);
        bool[] isVisited = new bool[warehouses_position.Count];
        int rand_index = 0;
        for (int i = 0; i < totalWareHouses; i++)
        {
            rand_index = Random.Range(0, warehouses_position.Count);
            while (isVisited[rand_index]) {
                isVisited[rand_index]  = true;
                rand_index = Random.Range(0, warehouses_position.Count);
            }
            actual_warehouses_position_in_world.Add(warehouses_position[rand_index]);
            GameObject house = Instantiate(house_prefab, warehouses_position[rand_index], Quaternion.identity);
            house.name = "HOUSE" + i.ToString();
            house.AddComponent<BoxCollider>();
            house.AddComponent<House>();
            house.AddComponent<CapsuleCollider>();
            house.GetComponent<BoxCollider>().size = new Vector3(5.0f, 5.0f, 5.0f);
            house.GetComponent<CapsuleCollider>().radius = 10.0f;
            house.GetComponent<CapsuleCollider>().isTrigger = true;
            house.AddComponent<ParticleSystem>();
            house.AddComponent<NavMeshObstacle>();
            house.GetComponent<NavMeshObstacle>().carving = true;
            var ps = house.GetComponent<ParticleSystem>();
            var ex = ps.externalForces;
            var main = ps.main;
            main.gravityModifier = 0.0f;
            main.gravityModifierMultiplier = 0.0f;
            ex.enabled = false;
            main.startColor = new Color(0.0f, 1.0f, 0.0f, 0.7f);
            ps.Play();
        }
    }


    void drawWaters(int totalWaters)
    {
        Debug.Log("Total water number is :" + totalWaters);
        bool[] isVisited = new bool[warehouses_position.Count];
        int rand_index = 0;

        for (int i = 0; i < totalWaters; i++)
        {
            // record the x position and z position of the warehouse 
            rand_index = Random.Range(0, warehouses_position.Count);
            while (isVisited[rand_index]) {
                isVisited[rand_index]  = true;
                rand_index = Random.Range(0, warehouses_position.Count);
            }
            GameObject water = Instantiate(water_prefab, warehouses_position[rand_index] + new Vector3(30.0f, 0.0f, 0.0f), Quaternion.identity);
            water.name = "WATER" + i.ToString();
            water.AddComponent<BoxCollider>();
            // house.AddComponent<House>();
            water.GetComponent<BoxCollider>().isTrigger = true;
            water.GetComponent<BoxCollider>().size = new Vector3(3.0f, 3.0f, 3.0f);
            water.AddComponent<ParticleSystem>();
            water.AddComponent<NavMeshObstacle>();
            water.GetComponent<NavMeshObstacle>().carving = true;
            // house.AddComponent<House>();
            var ps = water.GetComponent<ParticleSystem>();
            var ex = ps.externalForces;
            var main = ps.main;
            main.gravityModifier = 0.0f;
            main.gravityModifierMultiplier = 0.0f;
            ex.enabled = false;
            main.startColor = new Color(0.0f, 1.0f, 0.0f, 0.7f);
            ps.Play();
        }
    }

    void drawTurrets(int totalTurrets) {
        Debug.Log("Total turret number is :" + totalTurrets);
        bool[] isVisited = new bool[warehouses_position.Count];
        int rand_index = 0;
        

        // draw fixed position turrets
        for (int i = 0; i < turrets_position.Count; i++) {
            GameObject turret = Instantiate(turret_prefab, turrets_position[i], Quaternion.identity);
            turret.name = "TURRET" + i.ToString();
            turret.AddComponent<NavMeshObstacle>();
            turret.GetComponent<NavMeshObstacle>().carving = true;
        }

        int turret_id = turrets_position.Count;
        // randomly draw turrets near warehouses
        // record the x position and z position of the warehouse 
        Vector3[] positions = new Vector3[]{new Vector3(8.0f, 0.0f, 8.0f), new Vector3(-8.0f, 0.0f, 8.0f), new Vector3(-8.0f, 0.0f, -8.0f), new Vector3(8.0f, 0.0f, -8.0f)};
        for (int i = 0; i < actual_warehouses_position_in_world.Count; i++)
        {
            bool[] isCreated = new bool[4];
            int num_turret_near_warehouses = Random.Range(0, 4);
            for (int j = 0; j < num_turret_near_warehouses; j++) {
                rand_index = Random.Range(0, 4);
                while(isCreated[rand_index]) {
                    rand_index = Random.Range(0, 4);
                    isCreated[rand_index] = true;
                }
                GameObject turret = Instantiate(turret_prefab, warehouses_position[i] + positions[rand_index], Quaternion.identity);
                turret.name = "TURRET" + (turret_id + j).ToString();
            }
            turret_id += num_turret_near_warehouses;
        }
        
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


