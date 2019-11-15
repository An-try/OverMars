using UnityEngine;

namespace OverMars
{
    [CreateAssetMenu(fileName = "New ship", menuName = "Over Mars/Ship")]
    public class ShipItem : Item
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private string _tilesCode;

        public GameObject TilePrefab => _tilePrefab;
        public Vector2Int Size => IsTilesCodeProper() ? _size : Vector2Int.zero;
        public string TilesCode => IsTilesCodeProper() ? _tilesCode : "";
        public string CleanTilesCode => _tilesCode.Replace(" ", "");

        private bool IsTilesCodeProper()
        {
            if (_size.x * _size.y != CleanTilesCode.Length)
            {
                Debug.LogError("Tiles code length must be equals to size.height * size.width");
                return false;
            }
            return true;
        }
    }
}
