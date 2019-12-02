using UnityEngine;

namespace OverMars
{
    public class DoNotDestroyOnLoad : Singleton<DoNotDestroyOnLoad>
    {
#pragma warning disable 0649

        [SerializeField] private GameObject[] ObjectsNotToDestroyOnLoad;

#pragma warning restore 0649

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
