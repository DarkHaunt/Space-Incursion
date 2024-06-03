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
        /// Gets enemy core points of mirror arena sides
        /// </summary>
        /// <param name="startPos">Enemy start postion</param>
        /// <param name="movePos">Enemy move end position</param>
        public void CalculateEnemyPositions(out Vector2 startPos, out Vector2 movePos)
        {
            var isStableX = RandomExtensions.boolean;
            var isLeftSpawnPoint = RandomExtensions.boolean;
                
            startPos = GetEnemySpawnPos(isStableX, isLeftSpawnPoint);
            movePos = GetEnemyMovePosition(isStableX, isLeftSpawnPoint);
        }

        private Vector2 GetEnemySpawnPos(bool isStableX, bool isLeftSpawnPoint)
        {
            float x, y;

            if (isStableX)
            {
                x = isLeftSpawnPoint ? _leftBottomSpawnPos.x : _rightTopSpawnPos.x;
                y = Random.Range(_leftBottomSpawnPos.y, _rightTopSpawnPos.y);
            }
            else
            {
                y = isLeftSpawnPoint ? _leftBottomSpawnPos.y : _rightTopSpawnPos.y;
                x = Random.Range(_leftBottomSpawnPos.x, _rightTopSpawnPos.x);
            }
            
            var startPos = new Vector2(x, y);
            return startPos;
        }

        private Vector2 GetEnemyMovePosition(bool isStableX, bool isLeftSpawnPoint)
        {
            float x;
            float y;
            
            if (isStableX)
            {
                x = isLeftSpawnPoint ? _rightTopSpawnPos.x : _leftBottomSpawnPos.x;
                y = Random.Range(_leftBottomSpawnPos.y, _rightTopSpawnPos.y);
            }
            else
            {
                y = isLeftSpawnPoint ? _rightTopSpawnPos.y : _leftBottomSpawnPos.y;
                x = Random.Range(_leftBottomSpawnPos.x, _rightTopSpawnPos.x);
            }

            return new Vector2(x, y);
        }
    }
}