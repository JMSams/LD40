using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingSloth.LD40
{
    public class PirateManager : MonoBehaviour
    {
        public PirateController piratePrefab;
        Pool<PirateController> piratePool;

        public PlayerController player;

        public float pirateAgression { get; protected set; }

        void Start()
        {
            piratePool = new Pool<PirateController>(piratePrefab, 10);
        }

        void Update()
        {

        }

        PirateController GetNewPirate()
        {
            PirateController temp = piratePool.GetObject();
            temp.manager = this;
            return temp;
        }
    }
}