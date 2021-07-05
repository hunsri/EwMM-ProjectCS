using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class SceneStats : MonoBehaviour
    {
        private int _curedNPC = 0;
        private int _vaccinesShot = 0;
        private int _masksShot = 0;
        //the times someone got infected; spawned infections don't count
        private int _infectionEvents = 0;

        public void incrementCuredNPC()
        {
            _curedNPC++;
        }

        public void incrementVaccinesShot()
        {
            _vaccinesShot++;
        }

        public void incrementMasksShot()
        {
            _masksShot++;
        }

        public void incrementInfectionEvents()
        {
            _infectionEvents++;
        }
    }
}
