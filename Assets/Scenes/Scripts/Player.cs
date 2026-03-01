using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private InputAction move;
    [SerializeField] [Header("Damping Coefficient")]
    private float dampCoef = 0.9f;
    [SerializeField] 
    private float speed = 10.0f;
    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private float accelerationCoef = 1.0f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private enum PlayerState {
        IDLE,
        WALK
    };

    private PlayerState playerState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerState = PlayerState.IDLE;
        move = InputSystem.actions.FindAction("Move");
        accelerationCoef = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:
                if (move.IsInProgress())
                {
                    playerState = PlayerState.WALK;
                    accelerationCoef = 1;
                    velocity = new Vector3(move.ReadValue<Vector2>().x, move.ReadValue<Vector2>().y, 0) * (speed * Time.deltaTime);
                    if (move.ReadValue<Vector2>().x < 0)
                    {
                        spriteRenderer.flipX = true;
                    } else
                    {
                        spriteRenderer.flipX = false;
                    }
                    animator.Play("Walk 1");
                }
                break;
            case PlayerState.WALK:
                if (!move.IsInProgress())
                {
                    playerState = PlayerState.IDLE;
                    accelerationCoef = dampCoef;
                    animator.Play("Walk");
                }

                velocity = new Vector3(move.ReadValue<Vector2>().x, move.ReadValue<Vector2>().y, 0) * (speed * Time.deltaTime);
                if (move.ReadValue<Vector2>().x < 0)
                {
                    spriteRenderer.flipX = true;
                } else
                {
                    spriteRenderer.flipX = false;
                }

                break;
            default:
                break;
        }

        velocity *= accelerationCoef;
        transform.position += velocity;
    }
}
