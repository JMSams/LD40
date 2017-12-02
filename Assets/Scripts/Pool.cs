using System.Collections.Generic;
using UnityEngine;

namespace FallingSloth.LD40
{
    public class Pool<T> where T : MonoBehaviour
    {
        List<T> objects;

        T objectPrefab;

        bool canGrow;

        public Pool(T objectPrefab, int count = 1, bool canGrow = true)
        {
            this.canGrow = canGrow;
            this.objectPrefab = objectPrefab;
            objects = new List<T>();

            for (int i = 0; i < count; i++)
                AddObject();
        }

        void AddObject()
        {
            T temp = GameObject.Instantiate(objectPrefab);
            temp.gameObject.SetActive(false);
            objects.Add(temp);
        }

        public T GetObject()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (!objects[i].gameObject.activeSelf)
                    return objects[i];
            }

            if (canGrow)
            {
                AddObject();
                return objects[objects.Count - 1];
            }

            throw new System.Exception("Pool set to not grow, new object requested!");
        }
    }
}
