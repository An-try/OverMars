﻿using UnityEngine;

namespace OverMars
{
    public class DoNotDestroyOnLoad : Singleton<DoNotDestroyOnLoad>
    {
        [SerializeField] private GameObject[] ObjectsNotToDestroyOnLoad;

        public GameObject[] Objects => ObjectsNotToDestroyOnLoad;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < ObjectsNotToDestroyOnLoad.Length; i++)
            {
                DontDestroyOnLoad(ObjectsNotToDestroyOnLoad[i]);
            }
        }
    }
}
