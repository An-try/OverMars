using UnityEngine;
using UnityEngine.UI;

namespace OverMars
{
    public class ShipTileUI : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void ActivateTile(TileTypes tileType)
        {
            switch (tileType)
            {
                case TileTypes.Basic:
                    _image.color = Color.white;
                    break;
                case TileTypes.Engine:
                    _image.color = Color.blue;
                    break;
                default:
                    _image.color = Color.white;
                    break;
            }

            _image.enabled = true;
        }

        public void DeactivateTile()
        {
            _image.enabled = false;
        }
    }
}
