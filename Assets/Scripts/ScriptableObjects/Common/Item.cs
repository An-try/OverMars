using UnityEngine;

namespace OverMars
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        public string Name => _name;
        public string Description => _description;

        public virtual bool IsEquipment => false;
    }
}
