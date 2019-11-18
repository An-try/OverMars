using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private Transform _tilesContainer;
        [SerializeField] private GameObject _tilePrefab;

        private Rigidbody2D _rigidbody2D;

        private List<EquipmentItem> _equipmentItems = new List<EquipmentItem>();

        public ShipItem ShipItem => _shipItem;

        public ShipTile[,] ShipTilesGrid;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ShipControl();

            if (Input.GetKeyDown(KeyCode.E))
            {
                InitialiseShip();
            }
        }

        private void InitialiseShip()
        {
            RebuildTiles();
            FillShipWithEquipment();
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
                        shipTile.ActivateTile();
                    }

                    ShipTilesGrid[i, j] = shipTile;
                    tileIndex--;
                }
            }
        }

        private void DestroyTiles()
        {
            foreach (Transform tile in _tilesContainer)
            {
                Destroy(tile.gameObject);
            }
        }

        #endregion



        #region Filling ship with equipment

        private void FillShipWithEquipment()
        {
            _equipmentItems = null;
            _equipmentItems = new List<EquipmentItem>();

            for (int i = 0; i < ShipTilesGrid.GetLength(0); i++)
            {
                for (int j = 0; j < ShipTilesGrid.GetLength(1); j++)
                {
                    EquipmentItem equipmentItem = EquipmentPanelController.EquipmentTilesGrid[i, j].EquipmentItem;
                    if (equipmentItem)
                    {
                        ShipTilesGrid[i, j].SetItem(equipmentItem);
                        _equipmentItems.Add(equipmentItem);
                    }
                }
            }
        }

        #endregion



        #region Ship movement

        private void ShipControl()
        {
            float axisY = Input.GetAxis("Vertical");
            float axisX = Input.GetAxis("Horizontal");

            float thrustForce = 0;
            float rotationForce = 0;

            foreach (EquipmentItem equipmentItem in _equipmentItems)
            {
                if (equipmentItem.GetType() == typeof(EngineEquipment))
                {
                    EngineEquipment engineEquipment = (EngineEquipment)equipmentItem;
                    thrustForce += engineEquipment.ThrustForce;
                    rotationForce += engineEquipment.RotationForce;
                }
            }

            float enginesMaxVelocity = thrustForce * 10;
            float enginesMaxTurnSpeed = rotationForce * 10;

            ThrustForward(axisY * thrustForce);
            ClampVelocity(enginesMaxVelocity);

            RotateShip(axisX * rotationForce);
            ClampTurnSpeed(enginesMaxTurnSpeed);
        }

        private void ThrustForward(float forwardForce)
        {
            _rigidbody2D.AddForce(transform.up * forwardForce);
        }

        private void ClampVelocity(float enginesMaxVelocity)
        {
            float clampMoveX = Mathf.Clamp(_rigidbody2D.velocity.x, -enginesMaxVelocity, enginesMaxVelocity);
            float clampMoveY = Mathf.Clamp(_rigidbody2D.velocity.y, -enginesMaxVelocity, enginesMaxVelocity);
            _rigidbody2D.velocity = new Vector2(clampMoveX, clampMoveY);
        }

        private void RotateShip(float rotationForce)
        {
            _rigidbody2D.AddTorque(-rotationForce);
        }

        private void ClampTurnSpeed(float enginesMaxTurnSpeed)
        {
            float clampRotateSpeed = Mathf.Clamp(_rigidbody2D.angularVelocity, -enginesMaxTurnSpeed, enginesMaxTurnSpeed);
            _rigidbody2D.angularVelocity = clampRotateSpeed;
        }

        #endregion
    }
}
