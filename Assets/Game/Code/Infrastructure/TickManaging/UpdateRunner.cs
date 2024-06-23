using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Game.Code.Infrastructure.TickManaging
{
    public class UpdateRunner : ITickable, ITickSource
    {
        private readonly List<ITickListener> _listeners;

        public UpdateRunner(IEnumerable<ITickListener> listeners) => 
            _listeners = new List<ITickListener>(listeners);

        public void Tick()
        {
            for (var index = 0; index < _listeners.Count; index++)
                _listeners[index].Tick(Time.deltaTime);
        }

        public void AddListener(ITickListener listener)
        {
            if (_listeners.Contains(listener))
                return;
            
            _listeners.Add(listener);
        }

        public void RemoveListener(ITickListener listener) => 
            _listeners.Remove(listener);
    }
}