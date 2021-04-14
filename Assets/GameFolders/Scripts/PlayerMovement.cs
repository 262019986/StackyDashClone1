using System;
using UnityEngine;

namespace GameFolders.Scripts
{
    public enum AutoMoveState
    {
        StartToEnd,EndToStart
    }
    public enum State
    {
        Moving,Reached
    }
    public class PlayerMovement : BaseBehaviour
    {
        #region FIELDS

        public int orderID;
        public  Rigidbody RigidbodyInstance { get; set; }
        public State state = State.Reached;
        public AutoMoveState autoMoveState = AutoMoveState.StartToEnd;
        
        [Header("Movement Settings")]
        [SerializeField] private float speed;
        #endregion 
 
        #region PROPERTIES
        #endregion 
 
        #region MONOBHEAVIOR
        private PlayerInputHandler _inputHandler;
        #endregion 
        
        public override void Subscribe()
        {
            Executor.behaviours.Add(orderID,this);
        }
        
        public override void BaseAwake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();
            RigidbodyInstance = GetComponent<Rigidbody>();
        }
        
        public override void BaseFixedUpdate()
        {
            Move(_inputHandler.GetDirection);
            //SendToTarget(targetPos);
        }
        
        private void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    RigidbodyInstance.velocity = Vector3.Lerp(Vector3.zero, Vector3.right * 10, speed * Time.deltaTime);
                    //TODO LERP
                    //TODO TWEEN
                    break;
                case Direction.Left:
                    RigidbodyInstance.velocity = Vector3.Lerp(Vector3.zero, Vector3.left * 10, speed * Time.deltaTime);
                    break;
                case Direction.Down:
                    RigidbodyInstance.velocity = Vector3.Lerp(Vector3.zero, Vector3.back * 10 , speed * Time.deltaTime);
                    break;
                case Direction.Up:
                    RigidbodyInstance.velocity = Vector3.Lerp(Vector3.zero, Vector3.forward * 10, speed * Time.deltaTime);
                    break;
                case Direction.None: //TODO CONTINUE NOT SETTING ZERO VELOCITY
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void SendToTarget(Vector3 inTarget)
        {
            transform.position = Vector3.Lerp(transform.position, inTarget, speed * Time.deltaTime);
        }
    }
}