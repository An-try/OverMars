using UnityEngine;

namespace OverMars
{
    public class ShipTileBattle : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
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
