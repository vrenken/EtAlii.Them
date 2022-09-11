namespace Game.Players
{
    using Game.World;
    using System.Collections;
    using UnityEngine;

    public class HexTileSelector : MonoBehaviour
    {
        public ThirdPersonController character;
        
        public HexGrid hexGrid;

        public HexTile hexTile;

        public LayerMask selectionMask;

        /// <summary>
        /// The update interval (in seconds).
        /// </summary>
        public float interval = 0.1f;
        
        private IEnumerator _updateCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            if (Application.isPlaying)
            {
                _updateCoroutine = UpdateCoroutine();
                StartCoroutine(_updateCoroutine);
            }
        }

        private void OnDestroy()
        {
            if (_updateCoroutine != null)
            {
                StopCoroutine(_updateCoroutine);
                _updateCoroutine = null;
            }
        }

        private IEnumerator UpdateCoroutine()
        {
            while (_updateCoroutine != null)
            {
                yield return new WaitForSecondsRealtime(interval);
                UpdateHexTile();
                if (hexTile != null)
                {
                    hexTile.EnableHighlight();
                }
            }
        }

        private void UpdateHexTile()
        {
            if (hexGrid == null || character == null) 
            {
                return;
            }

            if (FindTarget(out var tileGameObject))
            {
                var selectedHexTile = tileGameObject.GetComponent<HexTile>();
                if (selectedHexTile != null)
                {
                    if (hexTile != null)
                    {
                        hexTile.DisableHighlight();

                    }
                    hexTile = selectedHexTile;  
                }
            }
        }
        
        
        private bool FindTarget(out GameObject result)
        {
            var ray = new Ray(character.transform.position + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out var hit, selectionMask))
            {
                result = hit.collider.gameObject;
                return true;
            }

            result = null;
            return false;
        }
    }
}
