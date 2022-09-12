// namespace Game.Players
// {
//     using UnityEngine;
//     using UnityEngine.UIElements;
//
//     public class EllipseMesh
//     {
//         private int _numSteps;
//         private float _width;
//         private float _height;
//         private Color _color;
//         private float _borderSize;
//         private bool _isDirty;
//         public Vertex[] vertices { get; private set; }
//         public ushort[] indices { get; private set; }
//
//         public EllipseMesh(int numSteps)
//         {
//             _numSteps = numSteps;
//             _isDirty = true;
//         }
//
//         public void UpdateMesh()
//         {
//             if (!_isDirty)
//                 return;
//
//             var numVertices = numSteps * 2;
//             var numIndices = numVertices * 6;
//
//             if (vertices == null || vertices.Length != numVertices)
//                 vertices = new Vertex[numVertices];
//
//             if (indices == null || indices.Length != numIndices)
//                 indices = new ushort[numIndices];
//
//             var stepSize = 360.0f / numSteps;
//             var angle = -180.0f;
//
//             for (var i = 0; i < numSteps; ++i)
//             {
//                 angle -= stepSize;
//                 var radians = Mathf.Deg2Rad * angle;
//
//                 var outerX = Mathf.Sin(radians) * width;
//                 var outerY = Mathf.Cos(radians) * height;
//                 var outerVertex = new Vertex
//                 {
//                     position = new Vector3(width + outerX, height + outerY, Vertex.nearZ),
//                     tint = color
//                 };
//                 vertices[i * 2] = outerVertex;
//
//                 var innerX = Mathf.Sin(radians) * (width - borderSize);
//                 var innerY = Mathf.Cos(radians) * (height - borderSize);
//                 var innerVertex = new Vertex
//                 {
//                     position = new Vector3(width + innerX, height + innerY, Vertex.nearZ),
//                     tint = color
//                 };
//                 vertices[i * 2 + 1] = innerVertex;
//
//                 indices[i * 6] = (ushort)((i == 0) ? vertices.Length - 2 : (i - 1) * 2); // previous outer vertex
//                 indices[i * 6 + 1] = (ushort)(i * 2); // current outer vertex
//                 indices[i * 6 + 2] = (ushort)(i * 2 + 1); // current inner vertex
//
//                 indices[i * 6 + 3] = (ushort)((i == 0) ? vertices.Length - 2 : (i - 1) * 2); // previous outer vertex
//                 indices[i * 6 + 4] = (ushort)(i * 2 + 1); // current inner vertex
//                 indices[i * 6 + 5] = (ushort)((i == 0) ? vertices.Length - 1 : (i - 1) * 2 + 1); // previous inner vertex
//             }
//
//             _isDirty = false;
//         }
//
//         public bool isDirty => _isDirty;
//
//         void CompareAndWrite(ref float field, float newValue)
//         {
//             if (Mathf.Abs(field - newValue) > float.Epsilon)
//             {
//                 _isDirty = true;
//                 field = newValue;
//             }
//         }
//
//         public int numSteps
//         {
//             get => _numSteps;
//             set
//             {
//                 _isDirty = value != _numSteps;
//                 _numSteps = value;
//             }
//         }
//
//         public float width
//         {
//             get => _width;
//             set => CompareAndWrite(ref _width, value);
//         }
//
//         public float height
//         {
//             get => _height;
//             set => CompareAndWrite(ref _height, value);
//         }
//
//         public Color color
//         {
//             get => _color;
//             set
//             {
//                 _isDirty = value != _color;
//                 _color = value;
//             }
//         }
//
//         public float borderSize
//         {
//             get => _borderSize;
//             set => CompareAndWrite(ref _borderSize, value);
//         }
//     }
// }