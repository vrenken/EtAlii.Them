namespace Game.Units
{
    using System.Collections;
    using UnityEngine;

    public class UnitFactory
    {
        IEnumerable Instantiate(Vector2 position, Unit unit)
        {
            // switch (unit)
            // {
            //     case Builder builder:
            //         break;
            //     case Explorer explorer:
            //         break;
            //     case Repairer repairer:
            //         break;
            //     case Attacker attacker:
            //         break;
            //     case Player player:
            //         break;
            // }

            yield return null;
        }
    }
}