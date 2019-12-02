using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public class Ship : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField] private ShipItem _shipItem;
        [SerializeField] private SpriteRenderer _shipSprite;
        [SerializeField] private Transform _tilesContainer;
        [SerializeField] private GameObject _tilePrefab;

#pragma warning restore 0649

        public ShipTile[,] ShipTilesGrid;
        public List<ShipTile> WorkingTiles { get; private set; } = new List<ShipTile>();

        private Rigidbody2D _rigidbody2D;
        private List<EquipmentItem> _equipmentItems = new List<EquipmentItem>();

        public ShipItem ShipItem => _shipItem;

        #region Ship parameters

        private float _durability = 0;
        private float _mass = 0;
        private float _minRange = Mathf.Infinity;
        private float _maxRange = 0;
        private float _thrustForce = 0;
        private float _enginesMaxVelocity = 0;
        private float _rotationForce = 0;
        private float _enginesMaxTurnSpeed = 0;

        private const float ENGINES_MAX_VALUES_MULTIPLIER = 10;

        #endregion

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

        /// <summary>
        /// Call this method when ship is instantiated and all equipment is assigned or when ship has lost some part.
        /// </summary>
        private void UpdateShipParameters()
        {
            _durability = 0;
            _mass = 0;
            _minRange = Mathf.Infinity;
            _maxRange = 0;
            _thrustForce = 0;
            _enginesMaxVelocity = 0;
            _rotationForce = 0;
            _enginesMaxTurnSpeed = 0;

            foreach (EquipmentItem equipmentItem in _equipmentItems)
            {
                _durability += equipmentItem.Durability;
                _mass += equipmentItem.Mass;

                if (equipmentItem.GetType() == typeof(EngineEquipment))
                {
                    EngineEquipment engineEquipment = (EngineEquipment)equipmentItem;
                    _thrustForce += engineEquipment.ThrustForce;
                    _rotationForce += engineEquipment.RotationForce;
                }
                else if (equipmentItem.GetType() == typeof(WeaponEquipmentItem))
                {
                    WeaponEquipmentItem weaponEquipmentItem = (WeaponEquipmentItem)equipmentItem;
                    if (weaponEquipmentItem.Range < _minRange)
                    {
                        _minRange = weaponEquipmentItem.Range;
                    }
                    if (weaponEquipmentItem.Range > _maxRange)
                    {
                        _maxRange = weaponEquipmentItem.Range;
                    }
                }
            }

            _enginesMaxVelocity = _thrustForce * ENGINES_MAX_VALUES_MULTIPLIER;
            _enginesMaxTurnSpeed = _rotationForce * ENGINES_MAX_VALUES_MULTIPLIER;
        }

        public float GetParameterValue(EntityParameters entityParameter)
        {
            switch (entityParameter)
            {
                case EntityParameters.Durability:
                    return _durability;
                case EntityParameters.Speed:
                    return _thrustForce;
                case EntityParameters.None:
                    Debug.LogError("Enum \"SearchParameter\": \"" + entityParameter + "\" is not correct!"); // TODO: check this output
                    return 0;
                default:
                    Debug.LogError("Enum \"SearchParameter\": \"" + entityParameter + "\" is not correct!"); // TODO: check this output
                    return 0;
            }
        }

        private void UpdateWorkingTiles()
        {
            WorkingTiles = null;
            WorkingTiles = new List<ShipTile>();

            for (int i = 0; i < ShipTilesGrid.GetLength(0); i++)
            {
                for (int j = 0; j < ShipTilesGrid.GetLength(1); j++)
                {
                    if (!ShipTilesGrid[i, j].IsEmptyTile)
                    {
                        WorkingTiles.Add(ShipTilesGrid[i, j]);
                    }
                }
            }
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
                    else
                    {
                        ShipTilesGrid[i, j].DeactivateTile();
                    }
                }
            }

            UpdateWorkingTiles();
            UpdateShipParameters();
        }

        #endregion



        #region Ship movement

        private void ShipControl()
        {
            float axisY = Input.GetAxis("Vertical");
            float axisX = Input.GetAxis("Horizontal");

            ThrustForward(axisY);
            ClampVelocity(_enginesMaxVelocity);

            RotateShip(axisX);
            ClampTurnSpeed(_enginesMaxTurnSpeed);
        }

        private void ThrustForward(float axisY)
        {
            _rigidbody2D.AddForce(transform.up * axisY * _thrustForce);
        }

        private void ClampVelocity(float enginesMaxVelocity)
        {
            float clampMoveX = Mathf.Clamp(_rigidbody2D.velocity.x, -enginesMaxVelocity, enginesMaxVelocity);
            float clampMoveY = Mathf.Clamp(_rigidbody2D.velocity.y, -enginesMaxVelocity, enginesMaxVelocity);
            _rigidbody2D.velocity = new Vector2(clampMoveX, clampMoveY);
        }

        private void RotateShip(float axisX)
        {
            _rigidbody2D.AddTorque(-axisX * _rotationForce);
        }

        private void ClampTurnSpeed(float enginesMaxTurnSpeed)
        {
            float clampRotateSpeed = Mathf.Clamp(_rigidbody2D.angularVelocity, -enginesMaxTurnSpeed, enginesMaxTurnSpeed);
            _rigidbody2D.angularVelocity = clampRotateSpeed;
        }

        #endregion
    }
}
