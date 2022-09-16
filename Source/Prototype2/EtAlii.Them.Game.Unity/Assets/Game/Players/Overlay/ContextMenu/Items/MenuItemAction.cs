namespace Game.Players
{
    using Game.World;
    using UnityEngine;

    public abstract class MenuItemAction : ScriptableObject
    {
        public abstract bool IsValid(HexTile tile, out int priority);
        public abstract void Invoke(HexTile tile, HexGrid grid);

    }
}