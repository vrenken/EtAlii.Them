using UnityEngine;

namespace Tessera
{
    public interface IEngineInterface
    {
        void Destroy(Object o);
        void RegisterCompleteObjectUndo(Object objectToUndo);
        void RegisterCreatedObjectUndo(Object objectToUndo);
    }

    public class UnityEngineInterface : IEngineInterface
    {
        private static UnityEngineInterface instance;

        public static UnityEngineInterface Instance => instance;

        static UnityEngineInterface()
        {
            instance = new UnityEngineInterface();
        }

        public void Destroy(Object o)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(o);
                if (o is GameObject go)
                    go.SetActive(false);
            }
            else
            {
                Object.DestroyImmediate(o);
            }
        }

        public void RegisterCompleteObjectUndo(Object objectToUndo)
        {
            // Do nothing
        }

        public void RegisterCreatedObjectUndo(Object objectToUndo)
        {
            // Do nothing
        }
    }
}
