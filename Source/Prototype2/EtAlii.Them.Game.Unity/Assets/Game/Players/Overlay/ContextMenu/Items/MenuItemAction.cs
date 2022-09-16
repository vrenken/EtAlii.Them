namespace Game.Players
{
    using Game.World;
    using UnityEngine;

    public abstract class MenuItemAction : ScriptableObject
    {
        public abstract bool IsValid(HexTile tile, HexGrid grid, out int priority, out object preparations);
        public abstract void Invoke(HexTile tile, HexGrid grid, object preparations);

    }
}