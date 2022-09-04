namespace Game.TechTree
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Them/TechTree/Add UnitTemplate", order = 0)]
    public class UnitTemplate : ScriptableObject
    {
        public UnitTemplate[] Improvements;
        public bool IsAvailable;
    }
}