using UnityEngine;

namespace OverMars
{
    public class ShipTile : MonoBehaviour
    {
        [SerializeField] private Transform _spriteContainer;
        
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;

        private Sprite _emptyTileSprite;
        private Vector2Int _emptyTileSize;

        private void Awake()
        {
            _spriteRenderer = _spriteContainer.GetComponent<SpriteRenderer>();
            _boxCollider2D = _spriteContainer.GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _emptyTileSprite = _spriteRenderer.sprite;
            _emptyTileSize = new Vector2Int((int)_emptyTileSprite.rect.size.x, (int)_emptyTileSprite.rect.size.y);
        }

        public void SetItem(EquipmentItem equipmentItem)
        {
            Destroy(_boxCollider2D);

            _spriteRenderer.sprite = equipmentItem.Sprite;
            Vector2Int size = new Vector2Int((int)equipmentItem.Sprite.rect.size.x, (int)equipmentItem.Sprite.rect.size.y);

            _spriteContainer.localPosition = new Vector3(_spriteContainer.localPosition.x * (size.x / _emptyTileSize.x),
                                                         _spriteContainer.localPosition.y * (size.y / _emptyTileSize.y),
                                                         _spriteContainer.localPosition.z);

            _boxCollider2D = _spriteContainer.gameObject.AddComponent<BoxCollider2D>();
        }

        public void DeleteItem()
        {
            _spriteRenderer.sprite = _emptyTileSprite;
            _spriteContainer.localPosition = Vector3.zero;
        }

        public void ActivateTile(TileTypes tileType)
        {
            switch (tileType)
            {
                case TileTypes.Basic:
                    _spriteRenderer.color = Color.white;
                    break;
                case TileTypes.Engine:
                    _spriteRenderer.color = Color.blue;
                    break;
                default:
                    _spriteRenderer.color = Color.white;
                    break;
            }

            _spriteRenderer.enabled = true;
            _boxCollider2D.enabled = true;
        }

        public void DeactivateTile()
        {
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
        }
    }
}
