using UnityEngine;

namespace OverMars
{
    public class ShipTile : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
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
