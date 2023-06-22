using System.Collections;
using UnityEngine;

public class FallingChecker : MonoBehaviour
{
    private bool _onOneBar;
    private Rigidbody _rigidbody;
    private PlayerAnimatorChanger _playerAnimator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimator = FindObjectOfType<PlayerAnimatorChanger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FallChekPoint checkPoint))
        {
            Falling();
        }
    }

    public void CheckForSecondBar()
    {
        _onOneBar = !_onOneBar;
        
        StartCoroutine("CheckForBarPositions");
    }

    private IEnumerator CheckForBarPositions()
    {
        yield return  new WaitForSeconds(0.15f);

        if (_onOneBar)
        {
            Falling();
        }
    }

    private void Falling()
    {
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _playerAnimator.Falling();
    }
}