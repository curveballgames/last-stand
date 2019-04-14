using System.Collections.Generic;
using UnityEngine;

namespace LastStand
{
    public class GameObjectPool<T> where T : MonoBehaviour
    {
        public T Prefab { get; set; }

        private List<T> pool;

        public GameObjectPool()
        {
            pool = new List<T>();
        }

        public T Get(int index)
        {
            if (Prefab == null)
            {
                throw new UnityException("No prefab set on pool of type " + typeof(T));
            }

            while (index >= pool.Count)
            {
                Create();
            }

            return pool[index];
        }

        public void ReturnAllAfter(int index)
        {
            for (; index < pool.Count; index++)
            {
                pool[index].gameObject.SetActive(false);
            }
        }

        private void Create()
        {
            T instance = GameObject.Instantiate(Prefab.gameObject).GetComponent<T>();
            pool.Add(instance);
        }
    }
}