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

            _emptyTileSprite = _spriteRenderer.sprite;
            _emptyTileSize = new Vector2Int((int)_emptyTileSprite.rect.size.x, (int)_emptyTileSprite.rect.size.y);
        }

        public void SetItem(EquipmentItem equipmentItem)
        {
            Destroy(_boxCollider2D);

            _spriteRenderer.sprite = equipmentItem.Sprite;
            _spriteRenderer.sortingOrder = 1;
            Vector2Int size = new Vector2Int((int)equipmentItem.Sprite.rect.size.x, (int)equipmentItem.Sprite.rect.size.y);

            int positionMultiplierX = 1;
            int positionMultiplierY = 1;

            if (size.x != _emptyTileSize.x)
            {
                positionMultiplierX = size.x / _emptyTileSize.x;
            }
            if (size.y != _emptyTileSize.y)
            {
                positionMultiplierY = size.y / _emptyTileSize.y;
            }

            _spriteContainer.localPosition = new Vector3(_spriteContainer.localPosition.x * positionMultiplierX,
                                                         _spriteContainer.localPosition.y * positionMultiplierY,
                                                         _spriteContainer.localPosition.z);

            _boxCollider2D = _spriteContainer.gameObject.AddComponent<BoxCollider2D>();
        }

        public void DeleteItem()
        {
            _spriteRenderer.sprite = _emptyTileSprite;
            _spriteRenderer.sortingOrder = 0;
            _spriteContainer.localPosition = Vector3.zero;
        }

        public void ActivateTile()
        {
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
