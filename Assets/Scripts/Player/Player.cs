using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerEdgeState EdgeState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondAttackState { get; private set; }

    #endregion

    #region Components

    public Animator Animator { get; private set; }
    public AnimatorStateInfo AnimatorInfo { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public InputHandler InputHandler { get; private set; }
    public PlayerData_SO PlayerData;

    #endregion

    #region Variables

    public Transform GroundCheck;
    public Transform WallCheck;
    public Transform EdgeCheck;
    public Transform DashIndicator;

    private Vector2 workSpace;
    public Vector2 CurrentVelocity { get; private set; }

    public int FacingDirection { get; private set; }
    public int amountOfJump;

    #endregion

    #region Unity Callback Functions

    private void Awake() {
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new PlayerStateMachine();

        InitializeState();
    }

    private void Start() {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<InputHandler>();

        StateMachine.Initialize(IdleState);

        FacingDirection = 1;
        workSpace = new Vector2(0, 0);
    }

    private void Update() {
        CurrentVelocity = RB.velocity;

        StateMachine.CurrentState.OnUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.OnFixedUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float veloX) {
        workSpace.Set(veloX, CurrentVelocity.y);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityY(float veloY) {
        workSpace.Set(CurrentVelocity.x, veloY);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocity(float velo, Vector2 angle, int direction) {
        angle.Normalize();
        workSpace.Set(velo * angle.x * direction, velo * angle.y);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    /// <summary>
    /// Set velocity to zero
    /// </summary>
    /// <param name="velo"></param>
    public void SetVelocity(float velo) {
        if (velo != 0) {
            return;
        }
        workSpace.Set(0f, 0f);
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    #endregion

    #region Check Function

    public void CheckShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }

    public bool CheckIfGrounded() {
        return Physics2D.OverlapCircle(GroundCheck.position, PlayerData.GroundCheckRadius, PlayerData.GroundLayer);
    }

    public bool CheckIfTouchingWall() {
        return Physics2D.Raycast(WallCheck.position, Vector2.right * FacingDirection, PlayerData.WallCheckDistance, PlayerData.GroundLayer);
    }

    public bool CheckIfTouchingWallBack() {
        return Physics2D.Raycast(WallCheck.position, Vector2.right * -FacingDirection, PlayerData.WallCheckDistance, PlayerData.GroundLayer);
    }

    public bool CheckIfTouchingEdge() {
        return Physics2D.Raycast(EdgeCheck.position, Vector2.right * FacingDirection, PlayerData.WallCheckDistance, PlayerData.GroundLayer);
    }

    public bool CheckAnimFinished(string animName) {
        AnimatorInfo = Animator.GetCurrentAnimatorStateInfo(0);
        if (AnimatorInfo.IsName(animName) && AnimatorInfo.normalizedTime > 0.99f) {
            Debug.Log($"{animName} finished");
            return true;
        }
        return false;
    }

    public bool AnimationTrigger(string animName) {
        AnimatorInfo = Animator.GetCurrentAnimatorStateInfo(0);
        if (AnimatorInfo.IsName(animName)) {
            return true;
        }
        return false;
    }

    #endregion

    #region Util Functions

    private void InitializeState() {
        //IdleState = new PlayerIdleState(StateMachine, this, PlayerData, "idle");
        //MoveState = new PlayerMoveState(StateMachine, this, PlayerData, "move");
        //LandState = new PlayerLandState(StateMachine, this, PlayerData, "land");
        //JumpState = new PlayerJumpState(StateMachine, this, PlayerData, "inAir");
        //InAirState = new PlayerInAirState(StateMachine, this, PlayerData, "inAir");
        //WallSlideState = new PlayerWallSlideState(StateMachine, this, PlayerData, "wallSlide");
        //WallJumpState = new PlayerWallJumpState(StateMachine, this, PlayerData, "inAir");
        //EdgeState = new PlayerEdgeState(StateMachine, this, PlayerData, "edgeClimbState");
        //DashState = new PlayerDashState(StateMachine, this, PlayerData, "inAir");
        //PrimaryAttackState = new PlayerAttackState(StateMachine, this, PlayerData, "attack");
        //SecondAttackState = new PlayerAttackState(StateMachine, this, PlayerData, "attack");
    }

    public void Flip() {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public Vector2 GetCornerPos() {
        var xhit = Physics2D.Raycast(WallCheck.position, Vector2.right * FacingDirection, 
            PlayerData.WallCheckDistance, PlayerData.GroundLayer);
        float xdis = (xhit.distance + 0.015f) * FacingDirection;
        workSpace.Set(xdis, 0f);

        var yhit = Physics2D.Raycast(EdgeCheck.position + (Vector3)workSpace, Vector2.down,
            EdgeCheck.position.y - WallCheck.position.y + 0.015f, PlayerData.GroundLayer);
        float ydis = yhit.distance;

        workSpace.Set(transform.position.x + xdis, EdgeCheck.position.y - ydis);

        Debug.Log($"coner position: {workSpace}");
        return workSpace;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(GroundCheck.position, PlayerData.GroundCheckRadius);
    }

    #endregion
}