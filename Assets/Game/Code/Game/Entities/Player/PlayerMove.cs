using Fusion;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private PhysicMove _physicMove;


        public void Move(Vector2 direction)
        {
            if (direction.magnitude > 0)
                _physicMove.Move(direction);
            else
                _physicMove.Stop();
        }

        public void Rotate(Vector2 direction)
        {
            
        }
    }
}