namespace Game.TechTree
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Them/TechTree/Add BuildingTemplate", order = 0)]
    public class BuildingTemplate : ScriptableObject
    {
        public BuildingTemplate[] Improvements;
        public bool IsAvailable;

        public bool IsBuildable;
        public bool IsUpgrade;

        public uint TechCostBlue;
        public uint TechCostGreen;
        public uint TechCostRed;
    }
}