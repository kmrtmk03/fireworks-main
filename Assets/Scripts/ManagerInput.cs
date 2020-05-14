using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyManager
{
    public class ManagerInput : MonoBehaviour
    {
        public static ManagerInput instance = null;

        [SerializeField]
        private SpawnFirework spawnFirework = default;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        public void OnMessage(string _data)
        {
            spawnFirework.Spawn(_data);
        }
    }
}