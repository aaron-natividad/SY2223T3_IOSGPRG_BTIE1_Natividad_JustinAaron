using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Run, Attack, Fall };

public class Player : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerDashGauge dashGauge;
    public AttackRange attackRange;
    public GroundCheck groundCheck;
    public TrailSpawner trailSpawner;
    public Animator anim;

    [Header("Parameters")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int dashPointValue;
    [SerializeField] private float attackTime;

    private PlayerState playerState;
    private float internalMoveSpeed;
    
    private Coroutine actionCoroutine;
    private Vector3 attackPosition;
    private Vector3 attackVelocity;

    private void OnEnable()
    {
        InputManager.OnSwipe += Attack;
        InputManager.OnTap += Dash;
        dashGauge.OnPowerActivate += Autoplay;
    }

    private void OnDisable()
    {
        InputManager.OnSwipe -= Attack;
        InputManager.OnTap -= Dash;
        dashGauge.OnPowerActivate -= Autoplay;
    }

    private void Start()
    {
        GameUIManager.instance.dashButton.onClick.AddListener(dashGauge.Activate);
        GameManager.instance.cam.target = gameObject;
        internalMoveSpeed = runSpeed;
    }

    private void FixedUpdate()
    {
        if (playerState != PlayerState.Attack)
        {
            transform.position += Vector3.up * internalMoveSpeed * Time.deltaTime;
            playerState = groundCheck.isGrounded ? PlayerState.Run : PlayerState.Fall;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, attackPosition, ref attackVelocity, 0.05f);
        }
        UpdateAnimation();
    }

    #region Delegate Handlers
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

    public void Autoplay()
    {
        StopAllCoroutines();
        actionCoroutine = StartCoroutine(CO_Autoplay());
    }
    #endregion

    #region Action Coroutines
    public IEnumerator CO_Dash()
    {
        internalMoveSpeed = dashSpeed;
        GameManager.instance.ReceivePoints(dashPointValue);
        StartCoroutine(trailSpawner.CO_SpawnTrail(dashTime));
        StartCoroutine(GameManager.instance.cam.CO_Pop());
        yield return new WaitForSeconds(dashTime);

        internalMoveSpeed = runSpeed;
        yield return new WaitForSeconds(dashCooldown);
        actionCoroutine = null;
    }

    public IEnumerator CO_Attack(SwipeDirection direction)
    {
        if (attackRange.enemies.Count > 0)
        {
            Enemy currentEnemy = TargetCurrentEnemy();
            yield return null;

            if (currentEnemy.CompareDirection(direction))
            {
                StartCoroutine(GameManager.instance.cam.CO_Shake());
                currentEnemy.KillEnemy(true);
                dashGauge.IncreaseGauge();
            }

            StartCoroutine(trailSpawner.CO_SpawnTrail(attackTime));
            playerState = PlayerState.Attack;
            yield return new WaitForSeconds(attackTime);

            playerState = PlayerState.Run;
        }

        actionCoroutine = null;
    }

    public IEnumerator CO_Autoplay()
    {
        StartCoroutine(GameManager.instance.cam.CO_ZoomOut());
        internalMoveSpeed = dashSpeed;
        yield return null;

        while(dashGauge.state == GaugeState.Cooldown)
        {
            internalMoveSpeed = dashSpeed;
            if (attackRange.enemies.Count > 0)
            {
                Enemy currentEnemy = TargetCurrentEnemy();
                yield return null;

                currentEnemy.KillEnemy(true);
                StartCoroutine(GameManager.instance.cam.CO_Shake());

                StartCoroutine(trailSpawner.CO_SpawnTrail(attackTime));
                playerState = PlayerState.Attack;
                yield return new WaitForSeconds(attackTime);

                playerState = PlayerState.Run;
            }
            yield return null;
        }

        StartCoroutine(GameManager.instance.cam.CO_ZoomIn());
        internalMoveSpeed = runSpeed;
        actionCoroutine = null;
    }
    #endregion

    public void UpdateAnimation()
    {
        switch (playerState)
        {
            case PlayerState.Run:
                anim.SetInteger("playerState", 0);
                break;
            case PlayerState.Attack:
                anim.SetInteger("playerState", 1);
                break;
            case PlayerState.Fall:
                anim.SetInteger("playerState", 2);
                break;
        }
    }

    public Enemy TargetCurrentEnemy()
    {
        GameObject currentEnemy;
        currentEnemy = attackRange.enemies[0];
        attackRange.enemies.RemoveAt(0);
        attackPosition = currentEnemy.transform.position;

        return currentEnemy.GetComponent<Enemy>();
    }
}
