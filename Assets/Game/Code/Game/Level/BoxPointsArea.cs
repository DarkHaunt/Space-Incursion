using Game.Scripts.Extensions;
using UnityEngine;

namespace Game.Code.Game.Level.BoxArea
{
    public class BoxPointsArea : MonoBehaviour
    {
        [SerializeField] private Transform _leftBottomPoint;
        [SerializeField] private Transform _rightBottomPoint;

        [Header("--- Scatter Params ---")]
        [SerializeField, Range(0f, 0.5f)] private float _scatterOfPosition;

        private float? _lastX;
        private float _scatterIncome;
        private float _lengthOfBox;

        public Vector3 position
            => transform.position;
        public Transform LeftBottomPoint
            => _leftBottomPoint;
        public Transform RightBottomPoint
            => _rightBottomPoint;


        private void Awake()
        {
            _lengthOfBox = _rightBottomPoint.position.x - _leftBottomPoint.position.x;
            _scatterIncome = _lengthOfBox * _scatterOfPosition;
        }

        public Vector3 GetRandomPositionInsideExclude()
        {
            if (!_lastX.HasValue)
            {
                var pos = RandomExtensions.GetRandomInBox(_leftBottomPoint.position, _rightBottomPoint.position);
                _lastX = pos.x;

                return pos;
            }

            var lastVal = _lastX.Value;
            
            float y = Random.Range(_leftBottomPoint.position.y, _rightBottomPoint.position.y);
            var newPos = new Vector2(GetRandomX(lastVal), y);
            
            _lastX = newPos.x;
            
            return newPos;
        }

        private float GetRandomX(float lastVal)
        {
            var leftExclude = ClampExclude(lastVal - _scatterIncome);
            var rightExclude = ClampExclude(lastVal + _scatterIncome);
            
            var valueLeft = Random.Range(_leftBottomPoint.position.x, leftExclude);
            var valueRight = Random.Range(rightExclude, _rightBottomPoint.position.x);

            if (Mathf.Approximately(valueLeft, _leftBottomPoint.position.x))
                return valueRight;

            if (Mathf.Approximately(valueRight, _rightBottomPoint.position.x))
                return valueLeft;

            return RandomExtensions.boolean ? valueLeft : valueRight;
        }

        private float ClampExclude(float value)
            => Mathf.Clamp(value, _leftBottomPoint.position.x, _rightBottomPoint.position.x);
    }
}