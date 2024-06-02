using Game.Code.Game.Level;
using Game.Scripts.Extensions;
using UnityEngine;

namespace Game.Code.Game.Services
{
    public class EnemyPositionProvider
    {
        private Vector3 _leftBottomSpawnPos;
        private Vector3 _rightTopSpawnPos;


        public void Init(LevelModel levelArea)
        {
            _leftBottomSpawnPos = levelArea.LeftBottomEnemySpawnPoint.position;
            _rightTopSpawnPos = levelArea.RightTopEnemySpawnPoint.position;
        }

        /// <summary>
        /// Gets position strictly on spawn border
        /// </summary>
        public Vector2 GetEnemySpawnPosition()
        {
            bool pickStableX = RandomExtensions.boolean;
            float x, y;

            if (pickStableX)
            {
                x = RandomExtensions.boolean ? _leftBottomSpawnPos.x : _rightTopSpawnPos.x;
                y = Random.Range(_leftBottomSpawnPos.y, _rightTopSpawnPos.y);
            }
            else
            {
                y = RandomExtensions.boolean ? _leftBottomSpawnPos.y : _rightTopSpawnPos.y;
                x = Random.Range(_leftBottomSpawnPos.x, _rightTopSpawnPos.x);
            }

            return new Vector2(x, y);
        }

        /// <summary>
        /// Gets mirror position inside spawn area, within some angle range
        /// </summary>
        /// <param name="pos"></param>
        public Vector2 GetEnemyMovePosition(Vector2 pos)
        {
            var normal = Vector2.Perpendicular(pos.normalized);
            
            return Vector2.zero;
        }
    }
}