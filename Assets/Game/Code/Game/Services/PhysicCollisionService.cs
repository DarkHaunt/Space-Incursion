using System;
using UnityEngine;
using static Game.Code.Game.StaticData.Indents.PhysicLayers;

namespace Game.Code.Game.Services
{
    public class PhysicCollisionService : IDisposable
    {
        public void Enable()
        {
            Physics2D.IgnoreLayerCollision(WallLayer, EnemyLayer, true);
            
            Physics2D.IgnoreLayerCollision(PlayerLayer, PlayerLayer, true);
            Physics2D.IgnoreLayerCollision(PlayerLayer, ProjectileLayer, true);
        }

        public void Disable()
        {
            Physics2D.IgnoreLayerCollision(WallLayer, EnemyLayer, false);
            
            Physics2D.IgnoreLayerCollision(PlayerLayer, PlayerLayer, false);
            Physics2D.IgnoreLayerCollision(PlayerLayer, ProjectileLayer, false);
        }

        public void Dispose() =>
            Disable();
    }
}