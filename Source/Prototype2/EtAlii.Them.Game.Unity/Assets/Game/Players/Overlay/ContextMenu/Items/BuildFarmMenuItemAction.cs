namespace Game.Players
{
    using Game.Buildings;
    using Game.World;using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildFarmMenuItemAction")]
    public class BuildFarmMenuItemAction : MenuItemAction
    {
        public BuildingProp farmPropPrefab;

        public override bool IsValid(HexTile tile, HexGrid grid, out int priority, out object preparations)
        {
            preparations = null;
            priority = 100;
            return tile.type switch
            {
                TileType.Ground => true,
                _ => false
            };
        }

        public override void Invoke(HexTile tile, HexGrid grid, object preparations)
        {
            var existingProps = tile.props.GetComponentsInChildren<BuildingProp>();
            foreach (var existingProp in existingProps)
            {
                Destroy(existingProp.gameObject); 
            }

            Instantiate(farmPropPrefab, tile.props.transform);
        }
    }
}