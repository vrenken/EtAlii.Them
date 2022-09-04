namespace Game.TechTree
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Them/TechTree/Add TechTree", order = 0)]
    public class TechTree : ScriptableObject
    {
        public BuildingTemplate[] Buildings;
        public UnitTemplate[] Units;
    }
}