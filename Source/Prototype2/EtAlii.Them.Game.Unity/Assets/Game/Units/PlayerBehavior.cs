namespace Game.Units
{
    using System.Collections;
    using Game.Buildings;
    using UnityEngine;

    public class PlayerBehavior : MonoBehaviour
    {
        public IEnumerable MoveTo(Vector2 position)
        {
            yield return null;
        }
        public IEnumerable CreateBuilding<TBuilding>(Vector2 position)
            where TBuilding: Building
        {
            yield return null;
        }
        public IEnumerable Configure<TBuilding>(Vector2 position)
            where TBuilding: Building
        {
            yield return null;
        }

        public IEnumerable CreateUnit<TUnit>(Vector2 position)
            where TUnit: Unit
        {
            yield return null;
        }
        public IEnumerable RepairUnit<TUnit>(Vector2 position)
            where TUnit: Unit
        {
            yield return null;
        }
    }
}