namespace Game.Players
{
    using Game.World;
    using UnityEngine;

    public abstract class MenuItemAction : ScriptableObject
    {
        public abstract bool IsValid(HexGrid grid, HexTile tile, out int priority, out object preparations);
        public abstract void Invoke(HexGrid grid, HexTile tile, object preparations);

    }
}