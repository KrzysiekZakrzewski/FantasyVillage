using BlueRacconGames.Animation;
using BlueRacconGames.Pool;
using Interactable;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownCharacterController2D : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private SpriteRenderer presentationGameObject;

        private Vector2 movement = Vector2.zero;
        private bool facingRight = true;
        private Vector2 direction = Vector2.down;

        public bool CanMove { get; private set; } = true;
        public Rigidbody2D Rb { get; private set; }

        public UnitAnimationControllerBase AnimationController { get; private set; }
        public InteractorControllerBase InteractorController { get; private set; }

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            AnimationController = GetComponent<UnitAnimationControllerBase>();
            InteractorController = GetComponent<InteractorControllerBase>();
        }

        public void Move(float horizontal, float vertical, bool run)
        {
            movement = new (horizontal, vertical);

            float movementSpeed = run ? runSpeed : walkSpeed;

            Rb.MovePosition(Rb.position + movementSpeed * Time.fixedDeltaTime * movement);
            AnimationController.WalkaalbleAnimation(movement.x, movement.y, movement.sqrMagnitude);

            ControlDirection(movement);
    
            if (horizontal > 0 && !facingRight)
                Flip();
            else if (horizontal < 0 && facingRight)
                Flip();
            
        }

        public void SetCanMove(bool canMove) => CanMove = canMove;

        private void ControlDirection(Vector2 newDirection)
        {
            if(direction == newDirection || newDirection == Vector2.zero) return;

            direction = newDirection;

            AnimationController.IdleAnimation(newDirection.x, newDirection.y);

            InteractorController.ChangeDirection(direction);
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = presentationGameObject.transform.localScale;
            theScale.x *= -1;
            presentationGameObject.transform.localScale = theScale;
        }
    }
}