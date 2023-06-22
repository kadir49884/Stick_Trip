using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorChanger : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;

    private Animator _animator;
    private float groundRadius = 0.08f;
    private bool _isGrounded;
    private StartGame _startGame;

    private const string IsFinished = "IsFinished";
    private const string IsFalling = "IsFalling";
    private const string IsSliding = "IsSliding";
    private const string IsJumping = "IsJumping";
    private const string IsStart = "IsStart";
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _startGame = FindObjectOfType<StartGame>();
        
        InvokeRepeating("CheckGround",0.5f, 0.1f );
    }
    
    private void OnEnable()
    {
        _startGame.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _startGame.GameStarted -= OnGameStarted;
    }

    private void CheckGround()
    {
        if (Physics.CheckSphere(_groundCheck.position, groundRadius, _whatIsGround))
        {
            _isGrounded = true;
        }
  
        if (_isGrounded)
        {
            _animator.SetBool(IsSliding, false);
            _animator.SetBool(IsJumping, false);
        }
    }
    
    private void OnGameStarted()
    {
        _animator.SetBool(IsStart, true);
    }
    
    public void Sliding()
    {
        ChangeAnimatorValues(true,false);
    }

    public void StopSliding()
    {
        ChangeAnimatorValues(false,true);
    }

    private void ChangeAnimatorValues(bool sliding, bool jumping)
    {
        _isGrounded = false;
        _animator.SetBool(IsSliding, sliding);
        _animator.SetBool(IsJumping, jumping);
    }

    public void Finished()
    {
        _animator.SetTrigger(IsFinished);
    }

    public void Falling()
    {
        _animator.SetTrigger(IsFalling);
    }
}
