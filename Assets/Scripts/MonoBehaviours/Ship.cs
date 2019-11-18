using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private SpriteRenderer _shipSprite;
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
            _shipSprite.sprite = _shipItem.Sprite;

            RebuildTiles();
            FillShipWithEquipment();
        }

        #region Building ship tiles

        private void RebuildTiles()
        {
            DestroyTiles();
            _tilesContainer.localPosition = Vector3.zero;

            int shipWidth = _shipItem.Width;
            int shipHeight = _shipItem.Height;
            string cleanTilesCode = _shipItem.CleanTilesCode;
            Vector2 starterPoint = new Vector2(_tilesContainer.localPosition.x - shipWidth / 2 - 0.5f, _tilesContainer.localPosition.y + shipHeight / 2 + 0.5f);

            ShipTilesGrid = new ShipTile[shipHeight, shipWidth];
            int tileIndex = shipWidth * shipHeight - 1;

            for (int i = shipHeight - 1; i >= 0; i--)
            {
                for (int j = shipWidth - 1; j >= 0; j--)
                {
                    Vector3 newTilePosition = new Vector3(starterPoint.x + j, starterPoint.y - i, 0);

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

            if (shipWidth % 2 == 0)
            {
                _tilesContainer.localPosition += new Vector3(0.5f, 0, 0);
            }
            if (shipHeight % 2 == 0)
            {
                _tilesContainer.localPosition -= new Vector3(0, 0.5f, 0);
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
