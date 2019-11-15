using UnityEngine;

namespace OverMars
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;

        public string Name => _name;
        public string Description => _description;
        public Sprite Sprite => _sprite;

        public virtual bool IsEquipment => false;
    }
}
