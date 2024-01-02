using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5f;
        [SerializeField] Animator _animator;
        [SerializeField] SpriteRenderer _renderer;



        void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            transform.position += moveDirection * _moveSpeed * Time.deltaTime;

            _animator.SetFloat("HorizontalVelocity", Mathf.Abs(moveDirection.x));
            _animator.SetFloat("VerticalVelocity", moveDirection.y);

            _renderer.flipX = moveDirection.x < 0;


        }
    }
}
