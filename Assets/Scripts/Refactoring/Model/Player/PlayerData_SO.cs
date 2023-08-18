using Assets.Scripts.Refactoring.Model.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData_SO : CharacterData_SO
{
    [Header("Move Params")]
    public float movementSpeed = 8f;

    [Header("Jump Params")]
    public float jumpForce = 8f;
    public float jumpPressedWindow = 0.5f;
    public int fallGravityScale;
    public int gravityScale;
    public float coyoteTime;

    [Header("Wall Jump Params")]
    public float wallJumpForce = 16f;
    public Vector2 wallJumpDirection = new Vector2(1, 2);

    [Header("Dash Params")]
    public float dashVelocity = 20f;
    public float dashTime;
    public float dashMaxHoldTime = 0.5f;
    public float dashTimeScale = 0.3f;
    public float dashDrag = 10f;
    public float dashCoolDown = 0.5f;
    public float dashEndMultiplier = 0.05f;

    [Header("Edge Climb Params")]
    public Vector2 startOffset;
    public Vector2 endOffset;

    //[Header("Check Params")]
    //public LayerMask GroundLayer;
    //public float GroundCheckRadius;
    //public float WallCheckDistance = 0.5f;

    [Header("Wall Slide Params")]
    public float wallSlideSpeed = 3f;
}
