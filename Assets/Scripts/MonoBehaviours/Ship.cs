using UnityEngine;

namespace OverMars
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private Transform _tilesContainer;
        [SerializeField] private GameObject _tilePrefab;

        public ShipItem ShipItem => _shipItem;

        public ShipTile[,] ShipTilesGrid;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InitialiseShip();
            }
        }

        private void InitialiseShip()
        {
            RebuildTiles();
            //FillShipWithEquipment();
        }

        #region Building ship tiles

        private void RebuildTiles()
        {
            DestroyTiles();

            Vector2Int size = _shipItem.Size;
            string cleanTilesCode = _shipItem.CleanTilesCode;
            Vector2 starterPoint = new Vector2(_tilesContainer.localPosition.x - size.x / 2 - 0.5f, _tilesContainer.localPosition.y + size.y / 2 + 0.5f);

            ShipTilesGrid = new ShipTile[size.x, size.y];
            int tileIndex = size.x * size.y - 1;

            for (int i = size.x - 1; i >= 0; i--)
            {
                for (int j = size.y - 1; j >= 0; j--)
                {
                    Vector3 newTilePosition = new Vector3(starterPoint.x + j + 1, starterPoint.y - i + 1, 0);

                    ShipTile shipTile = Instantiate(_tilePrefab, _tilesContainer).GetComponent<ShipTile>();
                    shipTile.transform.localPosition = newTilePosition;

                    int tileCode = int.Parse(cleanTilesCode[tileIndex].ToString());
                    if (tileCode == 0)
                    {
                        shipTile.DeactivateTile();
                    }
                    else
                    {
                        shipTile.ActivateTile((TileTypes)tileCode);
                    }

                    ShipTilesGrid[i, j] = shipTile;
                    tileIndex--;
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

        #region Filling ship with equipment

        private void FillShipWithEquipment()
        {
            for (int i = 0; i < ShipTilesGrid.GetLength(0); i++)
            {
                for (int j = 0; j < ShipTilesGrid.GetLength(1); j++)
                {
                    EquipmentItem equipmentItem = EquipmentPanelController.EquipmentTilesGrid[i, j].EquipmentItem;
                    if (equipmentItem)
                    {
                        ShipTilesGrid[i, j].SetItem(equipmentItem);
                    }
                }
            }
        }

        #endregion
    }
}
