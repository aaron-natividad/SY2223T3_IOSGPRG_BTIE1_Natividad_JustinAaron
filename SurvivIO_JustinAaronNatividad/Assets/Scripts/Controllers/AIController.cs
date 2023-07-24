using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Ranges")]
    public UnitDetection seekRange;
    public UnitDetection destroyRange;

    [Header("Roaming Parameters")]
    [SerializeField] private float roamRadius;
    [SerializeField] private float roamInterval;

    [Header("Guns")]
    [SerializeField] private GameObject[] gunPrefabs;

    // Components
    [HideInInspector] public MovementComponent movement;
    [HideInInspector] public AimComponent aiming;
    [HideInInspector] public InventoryComponent inventory;

    // States
    [HideInInspector] public Wander wanderState;
    [HideInInspector] public Seek seekState;
    [HideInInspector] public Destroy destroyState;

    // Private Variables
    private EnemyState currentState = null;

    private void Awake()
    {
        movement = GetComponent<MovementComponent>();
        aiming = GetComponent<AimComponent>();
        inventory = GetComponent<InventoryComponent>();

        wanderState = new Wander(this, roamRadius, roamInterval);
        seekState = new Seek(this);
        destroyState = new Destroy(this);
    }

    private void Start()
    {
        int gunIndex = Random.Range(0, gunPrefabs.Length);
        inventory.AddGun(gunPrefabs[gunIndex]);
        inventory.SetEquippedGun(gunPrefabs[gunIndex].GetComponent<Gun>().gunType);
        SetEnemyState(wanderState);
    }

    private void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }

    public void SetEnemyState(EnemyState state)
    {
        currentState?.OnExit();
        state.OnEnter();
        currentState = state;
    }
}
