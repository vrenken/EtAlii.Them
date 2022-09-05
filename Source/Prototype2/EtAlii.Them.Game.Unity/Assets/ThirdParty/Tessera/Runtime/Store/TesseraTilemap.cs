using System.Collections.Generic;
using UnityEngine;

namespace Tessera
{
    internal class TesseraTilemap
    {
        public IGrid Grid { get; set; }
        public IDictionary<Vector3Int, ModelTile> Data { get; set; }
    }
}
