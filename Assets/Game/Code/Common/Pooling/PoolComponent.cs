using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.PoolComponent
{
    public class PoolComponent<T> where T : Component
    {
        private List<T> _pool;
        private readonly T _prefab;
        private readonly bool _autoExpand;
        private readonly Transform _container;

        public PoolComponent(T prefab, int count, bool autoExpand)
        {
            _prefab = prefab;
            _autoExpand = autoExpand;
            _container = null;

            CreatePool(count);
        }

        public PoolComponent(T prefab, int count, Transform container, bool autoExpand)
        {
            _prefab = prefab;
            _container = container;
            _autoExpand = autoExpand;
            CreatePool(count);
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out T element))
                return element;

            if (_autoExpand)
                return CreateObject(true);

            throw new Exception("There is no free element in pool");
        }

        public List<T> GetElements(int count)
        {
            var elements = new List<T>();
            for (int i = 0; i < count; i++)
            {
                if (HasFreeElement(out T element))
                    elements.Add(element);
                else if (_autoExpand)
                    elements.Add(CreateObject(true));
                else
                    throw new Exception("Not enough free elements in the pool to get the requested amount.");
            }
            return elements;
        }

        public List<T> GetAllFreeElements() => 
            _pool.Where(element => element.gameObject.activeInHierarchy == false).ToList();

        public List<T> GetAllElements() => 
            _pool.ToList();

        private void CreatePool(int count)
        {
            _pool = new List<T>();
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            T createdObject = Object.Instantiate(_prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (T mono in _pool.Where(mono => mono.gameObject.activeInHierarchy == false))
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
            element = null;
            return false;
        }
    }
}