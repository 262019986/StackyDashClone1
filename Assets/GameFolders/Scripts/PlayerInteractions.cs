using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolders.Scripts
{
    public class PlayerInteractions : BaseBehaviour
    {
        
        #region FIELDS
        
        public int orderID;
        public static event Action OnCubeCollected; 
        [SerializeField] private List<Transform> cubeList = new List<Transform>();
        [SerializeField] private Transform model;
        [SerializeField] private float upAmount;
        
        #endregion 
 
        #region PROPERTIES
        #endregion 
 
        #region MONOBHEAVIOR

        private PlayerInputHandler _playerInput;
        private PlayerMovement _playerMovement;
        #endregion

        public override void Subscribe()
        {
            Executor.behaviours.Add(orderID,this);
        }
        public override void BaseAwake()
        {
            _playerInput = GetComponent<PlayerInputHandler>();
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            OnCollectableHit(other);
            OnDropHit(other);
            OnHitReachPoint(other);
            OnHitMovePoint(other);
            OnHitTurnPoint(other);
            OnHitFinish(other);
            OnHitForAuto(other);
        }
        
        private void OnCollectableHit(Component other)
        {
            if (other.CompareTag(Tags.Collectable))
            {
                cubeList.Add(other.transform);
                
                var pos = model.localPosition;
                pos += Vector3.up * upAmount;
                model.localPosition = pos;

                var tns = other.transform;
                tns.tag = Tags.UnCollectable;
                tns.SetParent(transform);
                tns.localPosition = Vector3.zero;
                tns.SetParent(model.transform);
                
                OnCubeCollected?.Invoke();
            }
        }

        private void OnDropHit(Component other)
        {
            if (other.CompareTag(Tags.Drop) && cubeList.Count != 0)
            {
                cubeList.RemoveAndParentNull();
                
                var pos = model.localPosition;
                pos += Vector3.up * (-1 * upAmount);
                model.localPosition = pos;

                var tns = other.transform;
                tns.tag = Tags.UnCollectable;

                Camera.main.fieldOfView -= 0.1f;
            }
        }

        private void OnHitReachPoint(Component other)
        {
            if (other.CompareTag(Tags.Reached))
            {
                _playerMovement.state = State.Reached;
            }
        }

        private void OnHitMovePoint(Component other)
        {
            if (other.CompareTag(Tags.MovePoint))
               _playerMovement.state = State.Moving;
        }

        private void OnHitTurnPoint(Component other)
        {
            if (other.CompareTag(Tags.DirectionBack) ||
                other.CompareTag(Tags.DirectionForward) || 
                other.CompareTag(Tags.DirectionLeft) ||
                other.CompareTag(Tags.DirectionRight))

            {
                Rotator rotator = other.GetComponent<Rotator>();
                _playerInput.GetDirection = _playerMovement.autoMoveState == AutoMoveState.StartToEnd
                    ? rotator.forStartToEnd
                    : _playerInput.GetDirection = rotator.forEndToStart;
            }
        }
        
        private void OnHitFinish(Component other)
        {
            if (other.CompareTag(Tags.Finish))
            {
                for (int i = 0; i < cubeList.Count; i++)
                {
                    cubeList.RemoveAndParentNull();
                }
                StartCoroutine(SetVelocityZero(_playerMovement.RigidbodyInstance, 0.25f));
            }
        }

        private void OnHitForAuto(Component other)
        {
            if (other.CompareTag(Tags.StartToEnd))
                _playerMovement.autoMoveState = AutoMoveState.StartToEnd;
            else if (other.CompareTag(Tags.EndToStart))
                _playerMovement.autoMoveState = AutoMoveState.EndToStart;
        }
        
        //TODO MAYBE STOP THE COROUTINE WHEN CLICKED
        private IEnumerator SetVelocityZero(Rigidbody rb, float duration)
        {
            yield return new WaitForSeconds(duration);
            _playerInput.GetDirection = Direction.None;
            rb.velocity = Vector3.zero;
        }
    }
}