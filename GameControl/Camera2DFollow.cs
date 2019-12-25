using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float yOffset;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float _mOffsetZ;
        private Vector3 _mLastTargetPosition;
        private Vector3 _mCurrentVelocity;
        private Vector3 _mLookAheadPos;

        // Use this for initialization
        private void Start()
        {
            _mLastTargetPosition = target.position;
            _mOffsetZ = (transform.position - target.position).z;
            transform.parent = null;
            transform.position = target.position;
        }


        // Update is called once per frame
        private void FixedUpdate()
        {
            // only update lookahead pos if accelerating or changed direction
            var xMoveDelta = (target.position - _mLastTargetPosition).x;

            var updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                _mLookAheadPos = lookAheadFactor * Mathf.Sign(xMoveDelta) * Vector3.right;
            }
            else
            {
                _mLookAheadPos =
                    Vector3.MoveTowards(_mLookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            var aheadTargetPos = target.position + _mLookAheadPos + Vector3.forward * _mOffsetZ;
            var newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref _mCurrentVelocity, damping);

            transform.position = new Vector3(newPos.x, newPos.y + yOffset, newPos.z);

            _mLastTargetPosition = target.position;
        }
    }
}