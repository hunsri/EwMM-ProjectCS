using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace NPC
{
    public class NPCWaveManager : MonoBehaviour
    {
        [SerializeField]
        private int _maxWaves;

        private int _currentWave;

        [SerializeField]
        private int _waveDurationSeconds; 

        private int _elapsedWaveTime;

    }
}
