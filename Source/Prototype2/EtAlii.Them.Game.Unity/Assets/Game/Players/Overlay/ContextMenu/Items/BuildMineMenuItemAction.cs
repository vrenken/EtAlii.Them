namespace Game.Players
{
    using Game.Buildings;
    using Game.World;using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildMineMenuItemAction")]
    public class BuildMineMenuItemAction : MenuItemAction
    {
        public BuildingProp minePropPrefab;

        public override bool IsValid(HexTile tile, out int priority)
        {
            priority = 100;
            return tile.type switch
            {
                TileType.Ground => true,
                _ => false
            };
        }

        public override void Invoke(HexTile tile, HexGrid grid)
        {
            var existingProps = tile.props.GetComponentsInChildren<BuildingProp>();
            foreach (var existingProp in existingProps)
            {
                Destroy(existingProp.gameObject); 
            }

            Instantiate(minePropPrefab, tile.props.transform);
        }
    }
}