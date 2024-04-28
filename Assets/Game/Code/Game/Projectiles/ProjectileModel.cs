using System;
using Game.Scripts.Infrastructure.TickManaging;
using Fusion;
using UnityEngine;

namespace Game.Code.Game.Projectiles
{
    public class ProjectileModel : NetworkBehaviour, ITickListener
    {
        public event Action<ITickListener> OnDisposed;
        
        [SerializeField] private PhysicMove _move;

        public void Construct(Vector2 pos, Vector2 direction)
        {
        }


        public void Tick(float deltaTime)
        {
        }
    }
}