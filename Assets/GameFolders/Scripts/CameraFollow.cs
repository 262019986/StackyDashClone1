using System;
using UnityEngine;

namespace GameFolders.Scripts
{
    public class CameraFollow : BaseBehaviour
    {
        #region FIELDS
        public int orderID;
        private Vector3 _offset;
        [SerializeField] private Transform target;
        [SerializeField] private float maxXvalue;
        [SerializeField] private float minXvalue;
        #endregion

        #region PROPERTIES

        #endregion

        #region MONOBHEAVIOR

        #endregion

        private void OnEnable() => PlayerInteractions.OnCubeCollected += SetCameraBack;

        private void OnDisable() => PlayerInteractions.OnCubeCollected -= SetCameraBack;

        public override void Subscribe()
        {
            Executor.behaviours.Add(orderID,this);
        }

        public override void BaseStart()
        {
            _offset = transform.position - target.position;
        }

        public override void BaseUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            Vector3 destination = target.position + _offset;
            SetClamp(destination);
            Vector3 smoothFollow = Vector3.Lerp(transform.position, destination,1f);
            transform.position = smoothFollow;
        }

        private void SetClamp(Vector3 value)
        {
            value.x = Mathf.Clamp(value.x, minXvalue, maxXvalue);
        }

        private void SetCameraBack()
        {
            Camera.main.fieldOfView += 0.1f;
        }
    }
}
