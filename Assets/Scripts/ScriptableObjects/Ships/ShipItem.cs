using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New ship", menuName = "Over Mars/Ship")]
    public class ShipItem : Item
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private string _tilesCode;

        public int Width => IsTilesCodeProper() ? _width : 0;
        public int Height => IsTilesCodeProper() ? _height : 0;
        public string TilesCode => IsTilesCodeProper() ? _tilesCode : "";
        public string CleanTilesCode => _tilesCode.Replace(" ", "");

        private bool IsTilesCodeProper()
        {
            if (_height * _width != CleanTilesCode.Length)
            {
                Debug.LogError("Tiles code length must be equals to size.height * size.width");
                return false;
            }
            return true;
        }
    }
}
