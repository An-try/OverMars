using UnityEngine;

namespace OverMars
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private Transform _tilesContainer;

        //private List<EquiupmentItem> equiupmentItems;

        private void InitialiseShip()
        {
            RebuildTiles();
        }

        #region Building ship tiles

        private void RebuildTiles()
        {
            DestroyTiles();

            Vector2Int size = _shipItem.Size;
            string cleanTilesCode = _shipItem.CleanTilesCode;

            Vector2Int starterPoint = new Vector2Int((int)_tilesContainer.localPosition.x + size.x / 2, (int)_tilesContainer.localPosition.y - size.y / 2);

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector3 newTilePosition = new Vector3(starterPoint.x + j, starterPoint.y + i, 0);
                    Instantiate(_shipItem.TilePrefab, newTilePosition, Quaternion.identity, _tilesContainer);
                }
            }
        }

        private void DestroyTiles()
        {
            for (;_tilesContainer.childCount > 0;)
            {
                Destroy(_tilesContainer.GetChild(0));
            }
        }

        //private void DetermineOffsetX

        #endregion
    }
}
