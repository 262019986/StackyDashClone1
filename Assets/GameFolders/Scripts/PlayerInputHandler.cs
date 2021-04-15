using UnityEngine;

namespace GameFolders.Scripts
{
    public enum Direction
    {
        None,Right,Left,Up,Down
    }
    public class PlayerInputHandler : BaseBehaviour
    {
        #region FIELDS
        
        [SerializeField]private int orderID;
        private Vector2 _firstTouchPos;

        #endregion 
 
        #region PROPERTIES

        public Direction GetDirection { get; set; } = Direction.None;

        #endregion 
 
        #region MONOBHEAVIOR

        private PlayerMovement _playerMovement;
        #endregion 
        
        public override void Subscribe()
        {
            Executor.behaviours.Add(orderID, this);
        }

        public override void BaseAwake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public override void BaseUpdate()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
                _firstTouchPos = Input.mousePosition;

            if (Input.GetMouseButton(0) && _playerMovement.state == State.Reached)
                SetDirection(Input.mousePosition);
        }
        
        //TODO RETURN TYPE DIRECTION 
        private void SetDirection(Vector3 inputPos)
        {
            var displacementX = Mathf.Abs(_firstTouchPos.x - inputPos.x);
            var displacementY = Mathf.Abs(_firstTouchPos.y - inputPos.y);

            if (!(displacementX > 0) && !(displacementY > 0)) return;
            // TO DETECT DIRECTION WHICH MOST CHANGED 
            if (displacementX > displacementY)
            {
                GetDirection = _firstTouchPos.x > inputPos.x ? GetDirection = Direction.Left : GetDirection = Direction.Right;
            }
            else
            {
                GetDirection = _firstTouchPos.y > inputPos.y ? GetDirection = Direction.Down : GetDirection = Direction.Up;
                //Debug.Log(_firstTouchPos.y > inputPos.y ? "Down" : "Up");
            }
        }
    }
}