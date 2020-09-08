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
        }

        [SerializeField] private GameObject flashlight;

        public GameObject Flashlight
        {
            get { return flashlight; }
        }

        [SerializeField] private GameObject laser;

        public GameObject Laser
        {
            get { return laser; }
        }

        [SerializeField] private GameObject reddot;

        public GameObject Reddot
        {
            get { return reddot; }
        }

        [SerializeField] private GameObject silencer;

        public GameObject Silencer
        {
            get { return silencer; }
        }

        [SerializeField] private Transform barrel;

        public Transform Barrel
        {
            get { return barrel; }
        }
    }
}
