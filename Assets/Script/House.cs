using Unity;
using UnityEngine;
using System.Collections.Generic;
using Parcels;
namespace Buildings
{
    public class House : MonoBehaviour
    {

        Level level;
        private SortedList<Parcel, int> HouseParcelsList;

        private int NextUpdate;

        int ItemNumber;

        private void Start()
        {
            NextUpdate = 1;
            HouseParcelsList = Parcel.GetParcelsList();
            ItemNumber = 0;
            level = GameObject.Find("Level").GetComponent<Level>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.name == "CarObj")
            {
                foreach (Parcel p in this.HouseParcelsList.Keys)
                {
                    // Debug.Log(p);
                    level.AddParcel(new Parcel(p.Name, p.Score, p.CountDown));
                }
                HouseParcelsList = Parcel.GetParcelsList();
            }
        }

        private void Update()
        {
            if (Time.time > NextUpdate && HouseParcelsList.Count < 2)
            {
                NextUpdate = Mathf.FloorToInt(Time.time) + 1;
                if (Random.Range(0, 100) > 10)
                {
                    Parcel p = Parcel.GetRandomParcel(); ;
                    try
                    {
                        HouseParcelsList.Add(p, ++ItemNumber);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.ToString() + "\n" + p);
                    }
                }
            }

        }

    }
}

