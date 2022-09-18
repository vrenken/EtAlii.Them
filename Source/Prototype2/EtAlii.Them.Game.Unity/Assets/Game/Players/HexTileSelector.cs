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

                if (hexGrid == null || character == null) 
                {
                    yield break;
                }

                if (FindTarget(out var selectedHexTile))
                {
                    if (selectedHexTile != null)
                    {
                        if (hexTile != null && hexTile != selectedHexTile)
                        {
                            hexTile.DisableHighlight();
                        }
                        hexTile = selectedHexTile;  
                        
                        contextMenu.Prepare(hexTile, out var canBeUsed);
                        if (canBeUsed)
                        {
                            hexTile.EnableHighlight();
                        }
                    }
                }
                else
                {
                    if (hexTile != null)
                    {
                        hexTile.DisableHighlight();
                        hexTile = null;
                    }
                }
            }
        }

        
        private bool FindTarget(out HexTile result)
        {
            
            var characterPosition = character.transform.position + Vector3.up / 2f;
            var ray = new Ray(characterPosition, Vector3.down);
            if (Physics.Raycast(ray, out var hit, selectionMask))
            {
                var potentialTile = hit.collider.gameObject;
                result = potentialTile.GetComponent<HexTile>() ?? potentialTile.GetComponentInParent<HexTile>(); 
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
                            potentialTile = hit.collider.gameObject;
                            result = potentialTile.GetComponent<HexTile>() ?? potentialTile.GetComponentInParent<HexTile>(); 
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
