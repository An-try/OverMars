using UnityEngine;

namespace OverMars
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private Transform _tilesContainer;

        //private List<EquiupmentItem> _equiupmentItems;

        private void Start()
        {
            InitialiseShip();
        }

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

            Vector2Int starterPoint = new Vector2Int((int)_tilesContainer.localPosition.x - size.x / 2, (int)_tilesContainer.localPosition.y + size.y / 2);

            int tileIndex = 0;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector3 newTilePosition = new Vector3(starterPoint.x + j + 1, starterPoint.y - i + 1, 0);
                    ShipTileBattle shipTileBattle = Instantiate(_shipItem.TilePrefab, newTilePosition, Quaternion.identity, _tilesContainer).GetComponent<ShipTileBattle>();

                    int tileCode = int.Parse(_shipItem.CleanTilesCode[tileIndex].ToString());
                    if (tileCode == 0)
                    {
                        shipTileBattle.DeactivateTile();
                    }
                    else
                    {
                        shipTileBattle.ActivateTile((TileTypes)tileCode);
                    }

                    tileIndex++;
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

        #endregion
    }
}
