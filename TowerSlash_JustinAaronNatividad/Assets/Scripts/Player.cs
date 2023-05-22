using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Run, Attack, Fall };

public class Player : MonoBehaviour
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Animator anim;

    [Header("Parameters")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float attackTime;
    [SerializeField] private float gravity;

    private Coroutine actionCoroutine;
    private float internalMoveSpeed;
    private Vector3 attackPosition;
    public PlayerState playerState;

    private void OnEnable()
    {
        InputManager.OnSwipe += Attack;
        InputManager.OnTap += Dash;
    }

    private void OnDisable()
    {
        InputManager.OnSwipe -= Attack;
        InputManager.OnTap -= Dash;
    }

    private void Start()
    {
        Physics2D.gravity = new Vector2(gravity, 0f);
        internalMoveSpeed = runSpeed;
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && actionCoroutine == null) actionCoroutine = StartCoroutine(CO_Dash());
        if (Input.GetKeyDown(KeyCode.Space) && actionCoroutine == null) actionCoroutine = StartCoroutine(CO_Attack());
    }*/

    private void FixedUpdate()
    {
        if (playerState != PlayerState.Attack)
        {
            transform.position += Vector3.up * internalMoveSpeed * Time.deltaTime;
            if (groundCheck.isGrounded) playerState = PlayerState.Run;
            else playerState = PlayerState.Fall;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, attackPosition, 0.3f);
        }
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        switch (playerState)
        {
            case PlayerState.Run: anim.SetInteger("playerState", 0); break;
            case PlayerState.Attack: anim.SetInteger("playerState", 1); break;
            case PlayerState.Fall: anim.SetInteger("playerState", 2); break;
        }
    }

    public void Dash()
    {
        if (actionCoroutine != null) return;
        actionCoroutine = StartCoroutine(CO_Dash());
    }

    public void Attack(SwipeDirection direction)
    {
        if (actionCoroutine != null) return;
        actionCoroutine = StartCoroutine(CO_Attack(direction));
    }

    IEnumerator CO_Dash()
    {
        internalMoveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        internalMoveSpeed = runSpeed;
        yield return new WaitForSeconds(dashCooldown);
        actionCoroutine = null;
    }

    IEnumerator CO_Attack(SwipeDirection direction)
    {
        GameObject currentEnemy;
        yield return null;

        if(attackRange.enemies.Count > 0)
        {
            currentEnemy = attackRange.enemies[0];
            attackRange.enemies.RemoveAt(0);
            attackPosition = currentEnemy.transform.position;
            yield return null;
            Destroy(currentEnemy);
            playerState = PlayerState.Attack;
            yield return new WaitForSeconds(attackTime);
            playerState = PlayerState.Run;
        }

        actionCoroutine = null;
    }
}
