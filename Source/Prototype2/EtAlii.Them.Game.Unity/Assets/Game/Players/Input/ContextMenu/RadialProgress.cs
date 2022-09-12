// namespace Game.Players
// {
//     using Unity.Collections;
//     using UnityEngine;
//     using UnityEngine.UIElements;
//
//     // An element that displays progress inside a partially filled circle
//     public class RadialProgress : VisualElement
//     {
//         public new class UxmlTraits : VisualElement.UxmlTraits
//         {
//             // The progress property is exposed to UXML.
//             readonly UxmlFloatAttributeDescription _progressAttribute = new UxmlFloatAttributeDescription()
//             {
//                 name = "progress"
//             };
//
//             // The Init method is used to assign to the C# progress property from the value of the progress UXML
//             // attribute.
//             public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
//             {
//                 base.Init(ve, bag, cc);
//
//                 ((RadialProgress)ve).progress = _progressAttribute.GetValueFromBag(bag, cc);
//             }
//         }
//
//         // A Factory class is needed to expose this control to UXML.
//         public new class UxmlFactory : UxmlFactory<RadialProgress, UxmlTraits> { }
//
//         // These are USS class names for the control overall and the label.
//         public static readonly string ussClassName = "radial-progress";
//         public static readonly string ussLabelClassName = "radial-progress__label";
//
//         // These objects allow C# code to access custom USS properties.
//         static readonly CustomStyleProperty<Color> TrackColor = new CustomStyleProperty<Color>("--track-color");
//         static readonly CustomStyleProperty<Color> ProgressColor = new CustomStyleProperty<Color>("--progress-color");
//
//         // These are the meshes this control uses.
//         private readonly EllipseMesh _trackMesh;
//         private readonly EllipseMesh _progressMesh;
//
//         // This is the label that displays the percentage.
//         private readonly Label _label;
//
//         // This is the number of outer vertices to generate the circle.
//         const int NumSteps = 200;
//
//         // This is the number that the Label displays as a percentage.
//         float _progress;
//
//         /// <summary>
//         /// A value between 0 and 100
//         /// </summary>
//         public float progress
//         {
//             // The progress property is exposed in C#.
//             get => _progress;
//             set
//             {
//                 // Whenever the progress property changes, MarkDirtyRepaint() is named. This causes a call to the
//                 // generateVisualContents callback.
//                 _progress = value;
//                 _label.text = Mathf.Clamp(Mathf.Round(value), 0, 100) + "%";
//                 MarkDirtyRepaint();
//             }
//         }
//
//         // This default constructor is RadialProgress's only constructor.
//         public RadialProgress()
//         {
//             // Create a Label, add a USS class name, and add it to this visual tree.
//             _label = new Label();
//             _label.AddToClassList(ussLabelClassName);
//             Add(_label);
//
//             // Create meshes for the track and the progress.
//             _progressMesh = new EllipseMesh(NumSteps);
//             _trackMesh = new EllipseMesh(NumSteps);
//
//             // Add the USS class name for the overall control.
//             AddToClassList(ussClassName);
//
//             // Register a callback after custom style resolution.
//             RegisterCallback<CustomStyleResolvedEvent>(evt => CustomStylesResolved(evt));
//
//             // Register a callback to generate the visual content of the control.
//             generateVisualContent += context => GenerateVisualContent(context);
//
//             progress = 0.0f;
//         }
//
//         static void CustomStylesResolved(CustomStyleResolvedEvent evt)
//         {
//             RadialProgress element = (RadialProgress)evt.currentTarget;
//             element.UpdateCustomStyles();
//         }
//
//         // After the custom colors are resolved, this method uses them to color the meshes and (if necessary) repaint
//         // the control.
//         void UpdateCustomStyles()
//         {
//             if (customStyle.TryGetValue(ProgressColor, out var progressColor))
//             {
//                 _progressMesh.color = progressColor;
//             }
//
//             if (customStyle.TryGetValue(TrackColor, out var trackColor))
//             {
//                 _trackMesh.color = trackColor;
//             }
//
//             if (_progressMesh.isDirty || _trackMesh.isDirty)
//                 MarkDirtyRepaint();
//         }
//
//         // The GenerateVisualContent() callback method calls DrawMeshes().
//         static void GenerateVisualContent(MeshGenerationContext context)
//         {
//             RadialProgress element = (RadialProgress)context.visualElement;
//             element.DrawMeshes(context);
//         }
//
//         // DrawMeshes() uses the EllipseMesh utility class to generate an array of vertices and indices, for both the
//         // "track" ring (in grey) and the progress ring (in green). It then passes the geometry to the MeshWriteData
//         // object, as returned by the MeshGenerationContext.Allocate() method. For the "progress" mesh, only a slice of
//         // the index arrays is used to progressively reveal parts of the mesh.
//         void DrawMeshes(MeshGenerationContext context)
//         {
//             float halfWidth = contentRect.width * 0.5f;
//             float halfHeight = contentRect.height * 0.5f;
//
//             if (halfWidth < 2.0f || halfHeight < 2.0f)
//                 return;
//
//             _progressMesh.width = halfWidth;
//             _progressMesh.height = halfHeight;
//             _progressMesh.borderSize = 10;
//             _progressMesh.UpdateMesh();
//
//             _trackMesh.width = halfWidth;
//             _trackMesh.height = halfHeight;
//             _trackMesh.borderSize = 10;
//             _trackMesh.UpdateMesh();
//
//             // Draw track mesh first
//             var trackMeshWriteData = context.Allocate(_trackMesh.vertices.Length, _trackMesh.indices.Length);
//             trackMeshWriteData.SetAllVertices(_trackMesh.vertices);
//             trackMeshWriteData.SetAllIndices(_trackMesh.indices);
//
//             // Keep progress between 0 and 100
//             float clampedProgress = Mathf.Clamp(_progress, 0.0f, 100.0f);
//
//             // Determine how many triangles are used to depending on progress, to achieve a partially filled circle
//             int sliceSize = Mathf.FloorToInt((NumSteps * clampedProgress) / 100.0f);
//
//             if (sliceSize == 0)
//                 return;
//
//             // Every step is 6 indices in the corresponding array
//             sliceSize *= 6;
//
//             var progressMeshWriteData = context.Allocate(_progressMesh.vertices.Length, sliceSize);
//             progressMeshWriteData.SetAllVertices(_progressMesh.vertices);
//
//             var tempIndicesArray = new NativeArray<ushort>(_progressMesh.indices, Allocator.Temp);
//             progressMeshWriteData.SetAllIndices(tempIndicesArray.Slice(0, sliceSize));
//             tempIndicesArray.Dispose();
//         }
//
//     }
// }