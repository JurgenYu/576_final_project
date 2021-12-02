using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Parcel
{
    public string name;

    public int Id;

    public float CountDown;

    public Parcel(string name, int Id, float CountDown)
    {
        this.name = name;
        this.Id = Id;
        this.CountDown = CountDown;
    }
}

class ParcelsTimeComparator : IComparer<Parcel>
{
    public int Compare(Parcel a, Parcel b)
    {
        if (a.CountDown == b.CountDown) {
            return a.Id - b.Id;
        } else {
            return (int)( a.CountDown - b.CountDown);
        }
    }
}

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    private SortedList<Parcel, int> ParcelInTrunk;

    private int ItemNumber;

    public Text CargoList;

    void Start()
    {
        ItemNumber = 0;
        ParcelInTrunk = new SortedList<Parcel, int>(new ParcelsTimeComparator());
        ParcelInTrunk.Add(new Parcel("test object1", ++ItemNumber,  200.0f), ItemNumber);
        ParcelInTrunk.Add(new Parcel("test object2", ++ItemNumber,  200.0f), ItemNumber);
        Debug.Log(0);

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
}
