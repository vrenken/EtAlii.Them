namespace Game.Buildings
{
    using Game.TechTree;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class Building : ScriptableObject
    {
        public GameObject GameObject;
        public Tile Tile;
        public int Health;
        public int MaxHealth;

        public BuildingTemplate Template;
    }
}