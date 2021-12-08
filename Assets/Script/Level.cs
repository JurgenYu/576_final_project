using System.Collections;
using System.Collections.Generic;
using Buildings;
using Parcels;
using UnityEngine;
using UnityEngine.UI;



public class Level : MonoBehaviour
{
    public List<Vector3> warehouses_position;
    // Start is called before the first frame update
    private SortedList<Parcel, int> ParcelInTrunk;

    private int ItemNumber;

    public GameObject house_prefab;

    public Text CargoList;
    public Terrain terrain;


    void Start()
    {
        ItemNumber = 0;
        ParcelInTrunk = new SortedList<Parcel, int>(new ParcelsTimeComparator());
        ParcelInTrunk.Add(new Parcel("test object1", ++ItemNumber, 200.0f), ItemNumber);
        ParcelInTrunk.Add(new Parcel("test object2", ++ItemNumber, 200.0f), ItemNumber);
        drawWareHouses(3);
    }

    // Update is called once per frame
    void Update()
    {
        CargoList.text = "";
        foreach (Parcel p in this.ParcelInTrunk.Keys)
        {
            string ParcelInfo = "Name: " + p.name + "\n" + "TimeLeft: " + p.CountDown + "\n";
            CargoList.text += ParcelInfo;
            p.CountDown -= Time.deltaTime;
        }

    }

    void drawWareHouses(int totalWareHouses)
    {
        Debug.Log(111);
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


            for (int x = 0; x < terrain.terrainData.size.x; x++)
            {

            }
        }
    }
}


