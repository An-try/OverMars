﻿using UnityEngine;

namespace OverMars
{
    public abstract class Item : ScriptableObject
    {
#pragma warning disable 0649

        [Header("Item")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _sprite;

#pragma warning restore 0649

        public string Name => _name;
        public string Description => _description;
        public int Price => _price;
        public Sprite Sprite => _sprite;

        public virtual bool IsEquipment => false;

        public virtual string GetInfo()
        {
            return "Name: " + _name + "\n" +
                   "Description: " + _description + "\n" +
                   "Price: " + _price + "\n";
        }
    }
}
