// ReSharper disable All
using DeBroglie.Constraints;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tessera
{
    /// <summary>
    /// Abstract class for all generator constraint components.
    /// > [!Note]
    /// > This class is available only in Tessera Pro
    /// </summary>
    public abstract class TesseraConstraint : MonoBehaviour
    {
        internal virtual IEnumerable<ITesseraInitialConstraint> GetInitialConstraints(IGrid grid) 
        {
            return Enumerable.Empty<ITesseraInitialConstraint>();
        }

        internal virtual IEnumerable<ITileConstraint> GetTileConstraint(TileModelInfo tileModelInfo, IGrid grid)
        {
            return Enumerable.Empty<ITileConstraint>();
        }

        internal IEnumerable<ModelTile> GetModelTiles(IEnumerable<TesseraTileBase> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile == null)
                    continue;

                foreach (var rot in tile.CellType.GetRotations(tile.rotatable, tile.reflectable, tile.rotationGroupType))
                {
                    foreach (var offset in tile.offsets)
                    {
                        var modelTile = new ModelTile(tile, rot, offset);
                        yield return modelTile;
                    }
                }
            }
        }
    }
}
