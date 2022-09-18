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

        public float offsetMargin = 0.2f;

        public ContextMenu contextMenu;
        
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
                    contextMenu.Prepare(hexTile, out var canBeUsed);
                    if (canBeUsed)
                    {
                        hexTile.EnableHighlight();
                    }
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
                    if (hexTile != null && hexTile != selectedHexTile)
                    {
                        hexTile.DisableHighlight();

                    }
                    hexTile = selectedHexTile;  
                }
            }
        }
        
        
        private bool FindTarget(out HexTile result)
        {
            
            var characterPosition = character.transform.position + Vector3.up / 2f;
            var ray = new Ray(characterPosition, Vector3.down);
            if (Physics.Raycast(ray, out var hit, selectionMask))
            {
                result = hit.collider.gameObject.GetComponent<HexTile>();
                if (result != null)
                {
                    // We want to also look 
                    var direction = characterPosition - result.transform.position;
                    if (direction.magnitude > 1f - offsetMargin)
                    {
                        direction.y = 0;
                        ray = new Ray(characterPosition + character.transform.forward * offsetMargin, Vector3.down);
                        if (Physics.Raycast(ray, out hit, selectionMask))
                        {
                            result = hit.collider.gameObject.GetComponent<HexTile>();
                            return result != null;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            result = null;
            return false;
        }
    }
}
