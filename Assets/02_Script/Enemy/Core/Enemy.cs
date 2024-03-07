using FSM_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour, IHitAble
{
    [SerializeField] EnemyDataSO enemyDataSO;
    public EnemyDataSO EnemyDataSO => enemyDataSO;

    
    [Header("Movement")]
    [SerializeField]
    float acceleration = 50, deacceleration = 100;
    [SerializeField]
    private float currentSpeed = 0;
    
    private Vector2 oldMovementInput;
    private Vector2 movementInput;
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    
    [Header("Health")]
    private int curHp;
    public bool Dead { get; private set; } = false;
    public event Action DeadEvent;

    //ETC
    public FeedbackPlayer feedbackPlayer { get; set; }
    public EnemyAnimController enemyAnimController { get; set; }

    private new Collider2D collider;
    private new Rigidbody2D rigidbody;
    public Collider2D Collider => collider;
    public Rigidbody2D Rigidbody => rigidbody;

    //public RoomInfo RoomInfo { get; private set; } //���� ���� ��ġ���ִ� room����;
    
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
       
        enemyAnimController = transform.Find("Visual").GetComponent<EnemyAnimController>();
        //Debug.Log(_mainMap.cellBounds);

        //RoomInfo roomInfo = new RoomInfo()
        //{
        //    bound = _mainMap.cellBounds,
        //    pos = _mainMap.transform.position,
        //};
        
        //SetRoomInfo(roomInfo);
    }

    private void Start()
    {
        curHp = enemyDataSO.MaxHP;
        DeadEvent += DieEvent;
    }

    private void Update()
    {
        if(movementInput != Vector2.zero)
            enemyAnimController.Flip(oldMovementInput);
    }

    private void FixedUpdate()
    {
        float maxSpeed = EnemyDataSO.Speed;
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
       
        //Debug.Log(movementInput);
        Vector3 position = rigidbody.position
                            + (oldMovementInput * currentSpeed * Time.deltaTime);
        rigidbody.MovePosition(position);
    }

    public void Hit(float damage)
    {
        if (Dead) return;

        curHp -= (int)damage;

        if (curHp <= 0)
        {
            Dead = true;
            Die();
            return;
        }

        feedbackPlayer.Play(damage + UnityEngine.Random.Range(0.25f, 1.75f));
    }

    private void Die()
    {
        Debug.Log("Die");

        DeadEvent?.Invoke();
    }

    private void DieEvent()
    {
                
    }

    //public void SetRoomInfo(RoomInfo curRoom)
    //{
    //    RoomInfo = curRoom;
    //}
}
