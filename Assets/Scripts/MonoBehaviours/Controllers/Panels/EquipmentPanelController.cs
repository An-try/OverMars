using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class EquipmentPanelController : PanelCommon<EquipmentPanelController>
    {
        [SerializeField] private Transform _shipTilesContainerUI;
        [SerializeField] private GameObject _shipTileUIPrefab;

        private GridLayoutGroup _tilesContainerGrid;

        private protected override void Awake()
        {
            base.Awake();
            _tilesContainerGrid = _shipTilesContainerUI.GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            RebuildTiles();
        }

        #region Building ship tiles UI

        private void RebuildTiles()
        {
            DestroyTiles();

            ShipItem shipItem = PlayerController.Instance.Ship.ShipItem;
            string cleanTilesCode = shipItem.CleanTilesCode;
            _tilesContainerGrid.constraintCount = shipItem.Size.y;
            int tileIndex = 0;

            for (int i = 0; i < cleanTilesCode.Length; i++)
            {
                ShipTileUI shipTileUI = Instantiate(_shipTileUIPrefab, _shipTilesContainerUI).GetComponent<ShipTileUI>();

                int tileCode = int.Parse(cleanTilesCode[tileIndex].ToString());
                if (tileCode == 0)
                {
                    shipTileUI.DeactivateTile();
                }
                else
                {
                    shipTileUI.ActivateTile((TileTypes)tileCode);
                }

                tileIndex++;
            }
        }

        private void DestroyTiles()
        {
            for (; _shipTilesContainerUI.childCount > 0;)
            {
                Destroy(_shipTilesContainerUI.GetChild(0));
            }
        }

        #endregion
    }
}
