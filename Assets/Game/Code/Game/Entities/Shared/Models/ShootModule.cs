using Fusion;
using Game.Code.Extensions;
using Game.Code.Game.Services;
using UnityEngine;

namespace Game.Code.Game.Entities.Shared.Models
{
    public class ShootModule : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        
        private GameFactory _gameFactory;

        public void Construct(GameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public async void Shoot(PlayerRef shooterRef)
        {
            var shootDirection = GetShootDirection();
            
            var projectile = await _gameFactory.CreateProjectile(_shootPoint.position, shooterRef);
            projectile.SetupAndMove(shootDirection);
        }

        private Vector2 GetShootDirection() =>
            Vector2Extensions.Direction(transform.position, _shootPoint.position);
    }
}