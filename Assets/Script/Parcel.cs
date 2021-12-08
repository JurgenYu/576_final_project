using Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcels
{
    public class Parcel
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

    public class ParcelsTimeComparator : IComparer<Parcel>
    {
        public int Compare(Parcel a, Parcel b)
        {
            if (a.CountDown == b.CountDown)
            {
                return a.Id - b.Id;
            }
            else
            {
                return (int)(a.CountDown - b.CountDown);
            }
        }
    }
}
