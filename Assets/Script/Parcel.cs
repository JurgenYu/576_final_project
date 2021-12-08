using Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcels
{
    public class Parcel
    {
        public static List<string> ParcelNames = new List<string>(){
            "Alpha-",
            "Bravo-",
            "Charlie-",
            "Delta-"
        };

        public string Name;

        public int Score;

        public float CountDown;


        public Parcel(string name, int Score, float CountDown)
        {
            this.Name = name;
            this.Score = Score;
            this.CountDown = CountDown;
        }

        public static SortedList<Parcel, int> GetParcelsList()
        {
            return new SortedList<Parcel, int>(new ParcelsComparator());
        }

        public static Parcel GetRandomParcel()
        {
            string Name = ParcelNames[Random.Range(0, ParcelNames.Count)];
            int Score = Random.Range(1, 10);
            int Time = Random.Range(5, 10) * 10;
            switch (Name)
            {
                case "Alpha-":
                    Score *= 10;
                    break;
                case "Bravo-":
                    Score *= 7;
                    break;
                case "Charlie-":
                    Score *= 5;
                    break;
                case "Delta-":
                    Score *= 2;
                    break;
                default: break;
            }
            Name+=Random.Range(1, 100).ToString();
            return new Parcel(Name, Score, Time);
        }

        override public string ToString() {
            return "Name: " + Name + " " + "Time: " + CountDown + " " + "Score: " + Score;
        }
    }

    public class ParcelsComparator : IComparer<Parcel>
    {
        public int Compare(Parcel a, Parcel b)
        {
            if (a.CountDown == b.CountDown)
            {
                return a.Score - b.Score;
            }
            else if (a.CountDown != b.CountDown)
            {
                return (int)(a.CountDown - b.CountDown);
            } else {
                return (int)(a.Name.CompareTo(b.Name));
            }
        }
    }
}
