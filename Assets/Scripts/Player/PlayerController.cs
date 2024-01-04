using CuteNewtTest.Utils;
using System;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5f;
        [SerializeField] Animator _animator;
        [SerializeField] SpriteRenderer _renderer;
        [SerializeField] Rigidbody2D _rigidbody;
        [SerializeField] float _offsetToCheckClimb = 0.5f;

        IHeightResolver _heightResolver;

        PlayerInput _playerInput;
        Vector3 _facingDirection;
        bool _performClimb;

        public void InjectHeightResolver(IHeightResolver heightResolver)
        {
            _heightResolver = heightResolver;
        }

        void Start()
        {
            _playerInput = new();
            _playerInput.Enable();
            _playerInput.Gameplay.Climb.performed += _ =>
            {
                _performClimb = CheckIfFacingWall();
            };
        }

        void FixedUpdate()
        {
            ProccessWalking();
            ProcessWallClimb();
            UpdateHeight();
        }

        void ProccessWalking()
        {
            Vector2 moveDirection = _playerInput.Gameplay.Move.ReadValue<Vector2>();
            _facingDirection = moveDirection == Vector2.zero ? Vector3.up : moveDirection;

            _animator.SetFloat("HorizontalVelocity", moveDirection.x);
            _animator.SetFloat("VerticalVelocity", moveDirection.y);
            _renderer.flipX = moveDirection.x < 0;

            _rigidbody.MovePosition(_rigidbody.position + moveDirection * _moveSpeed * Time.deltaTime);
        }

        void ProcessWallClimb()
        {
            if (!_performClimb)
                return;

            _performClimb = false;
            transform.Translate(_facingDirection * CalculateWallSize());
            UpdateHeight();
        }

        bool CheckIfFacingWall()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingDirection, 1f, LayerUtils.LayerToMask(Constants.WALL_LAYER));
            return hit.collider != null;
        }

        float CalculateWallSize()
        {
            RaycastHit2D hit;
            Vector3 positionToCheck = transform.position;
            do
            {
                positionToCheck += _facingDirection * _offsetToCheckClimb;
                hit = Physics2D.Raycast(positionToCheck, _facingDirection, _offsetToCheckClimb);
            } while (hit.collider != null);

            return Vector3.Distance(positionToCheck, transform.position);
        }

        void UpdateHeight()
        {
             int height = _heightResolver?.GetHeightIndexInPosition(transform.position) ?? 0;
            _renderer.sortingOrder = SortingOrderUtils.GetHeightLevelSortingOrder(height + 1);
        }
    }
}
