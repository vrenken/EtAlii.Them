namespace Game.World
{
    using UnityEngine;
    using Game.Units;
    
    public class World : ScriptableObject
    {
        public uint Width { get; set; }
        public uint Height { get; set; }

        public BuildingsMap Buildings { get; set; }
        public Unit[] Units { get; set; }
    }
}