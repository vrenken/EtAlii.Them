namespace Game.World
{
    using System.Collections;
    using UnityEngine;
    
    public class WorldBuilder : MonoBehaviour
    {
        public IEnumerable Build(WorldManager manager)
        {
            yield return null;
        }
    }
}