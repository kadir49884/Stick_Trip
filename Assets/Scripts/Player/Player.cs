using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _timeToBurnStave;
    
    public event UnityAction FireStepped;
    public event UnityAction<float> BonusGoted;

    private Rigidbody _rigidbody;
    private float _timer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BonusStave bonusStave))
        {
            BonusGoted?.Invoke(bonusStave.Lenght);
            bonusStave.Destroy();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Fire fire))
        {
            FireStayed();
        }
    }

    private void FireStayed()
    {
        _timer += Time.deltaTime;
        
        if (_timer > _timeToBurnStave)
        {
            _timer = 0;
            FireStepped?.Invoke();
        }
    }
    
    public void FreezeYPos()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _rigidbody.freezeRotation = true;
    }

    public void DefrostYPos()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.freezeRotation = true;
    }
}
