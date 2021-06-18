using MarioGame.GUI;
using MarioGame.Manager;
using MarioGame.Pads;
using MarioGame.TurtleController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioGame.Player
{
    public class Mario : MonoBehaviour
    {
        public float speed;

        Rigidbody2D rb;

        Animator animator;

        bool isDead;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (isDead && rb)
                rb.velocity = new Vector2(0f, rb.velocity.y);
            if (isDead || !GameManager.Ins.IsGamebegun) return;
            MoveHandle();
            
        }

        private void Update()
        {
            if (isDead || !GameManager.Ins.IsGamebegun) return;
            Flip();
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -8.33f, 8.33f),
                transform.position.y,
                transform.position.z);
           
               
        }

        void MoveHandle()
        {
            if (GamePadsController.Ins.CanMoveLeft)
            {
                if (rb)
                    rb.velocity = new Vector2(-1f, rb.velocity.y) * speed;
                if (animator)
                    animator.SetBool("Run", true);
            }
            else if (GamePadsController.Ins.CanMoveRight)
            {
                if (rb)
                    rb.velocity = new Vector2(1f, rb.velocity.y) * speed;
                if (animator)
                    animator.SetBool("Run", true);
            }
            else
            {
                if (rb)
                    rb.velocity = new Vector2(0, rb.velocity.y) * speed;
                if (animator)
                    animator.SetBool("Run", false);
            }
           
        }

        void Flip()
        {
            if (GamePadsController.Ins.CanMoveLeft)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1,
                        transform.localScale.y, transform.localScale.z);
                }
            }
            else if (GamePadsController.Ins.CanMoveRight)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1,
                        transform.localScale.y, transform.localScale.z);
                }
            }
        }

        void Die()
        {
            isDead = true;
            if (animator)
                animator.SetTrigger("Dead");
            if (rb)
                rb.velocity = new Vector2(0f, rb.velocity.y);
            AudioController.Ins.PlaySound(AudioController.Ins.loseSound);
            AudioController.Ins.StopPlayMusic();
            Destroy(gameObject, 1.6f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Turtle"))
            {
                if (isDead) return;
                Turtle turtle = collision.gameObject.GetComponent<Turtle>();
                if (turtle && !turtle.IsGround)
                {
                    Die();

                    GameManager.Ins.IsGameover = true;
                    GameGUIManager.Ins.ShowGameover(true);
                }
            }
        }
    }
}
    

