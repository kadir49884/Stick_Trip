using UnityEngine;

public class FinishPlane : MonoBehaviour
{
    private Animator _animator;

    private const string OnTouched = "OnTouched";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerAnimatorChanger player))
        {
            player.Finished();
            player.gameObject.GetComponent<PlayerMover>().Finished();
            _animator.SetTrigger(OnTouched);
        }
    }
}
