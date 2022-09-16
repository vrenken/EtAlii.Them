namespace Game.Players
{
    using Game.Buildings;
    using Game.World;using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildWallMenuItemAction")]
    public class BuildWallMenuItemAction : MenuItemAction
    {
        public BuildingProp straightWallPropPrefab;
        public BuildingProp cornerWallPropPrefab;
        public BuildingProp gateWallPropPrefab;
        public BuildingProp closedGateWallPropPrefab;
        public BuildingProp hexCornerWallAPropPrefab;
        public BuildingProp hexCornerWallBPropPrefab;

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

            Instantiate(straightWallPropPrefab, tile.props.transform);
        }
    }
}