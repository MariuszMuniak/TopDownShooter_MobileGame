using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    [System.Serializable]
    public class WeaponComponents
    {
        [SerializeField] private GameObject acog;

        public GameObject Acog
        {
            get { return acog; }
            set { acog = value; }
        }

        [SerializeField] private GameObject flashlight;

        public GameObject Flashlight
        {
            get { return flashlight; }
            set { flashlight = value; }
        }

        [SerializeField] private GameObject laser;

        public GameObject Laser
        {
            get { return laser; }
            set { laser = value; }
        }

        [SerializeField] private GameObject reddot;

        public GameObject Reddot
        {
            get { return reddot; }
            set { reddot = value; }
        }

        [SerializeField] private GameObject silencer;

        public GameObject Silencer
        {
            get { return silencer; }
            set { silencer = value; }
        }

        [SerializeField] private Transform barrel;

        public Transform Barrel
        {
            get { return barrel; }
            set { barrel = value; }
        }
    }
}
