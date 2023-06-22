using UnityEngine;

public class Bar : MonoBehaviour
{
    //[SerializeField] private GameObject _sparkTemplate;
    [SerializeField] private ParticleSystem _wind;

    private GameObject _spark;
    private Vector3 _windTransform;
    private Quaternion _windRotation;
    private int barTriggerCount;

    private void Start()
    {
        _windTransform = new Vector3(transform.position.x, transform.position.y + 13, transform.position.z + 25f);
        _windRotation = Quaternion.Euler(transform.rotation.x + 90, transform.rotation.y + 45, transform.rotation.z - 45);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Stave stave))
        {
            stave.GetComponentInParent<PlayerMover>().Accelerate();
            stave.GetComponentInParent<PlayerAnimatorChanger>().Sliding();

            stave.GetComponentInParent<Stave>().BarTriggerCount++;

            if (stave.GetComponentInParent<Stave>().BarTriggerCount > 1)
            {
                stave.GetComponentInParent<Player>().FreezeYPos();
            }

            //_spark = Instantiate(_sparkTemplate, transform.position, Quaternion.identity);
            Instantiate(_wind, _windTransform, _windRotation, transform);
            stave.GetComponent<FallingChecker>().CheckForSecondBar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Stave stave))
        {
            stave.GetComponentInParent<Stave>().BarTriggerCount = 0;
            stave.GetComponentInParent<PlayerMover>().Braking();
            stave.GetComponentInParent<PlayerAnimatorChanger>().StopSliding();
            stave.GetComponentInParent<Player>().DefrostYPos();

            //Destroy(_spark);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out Stave stave))
    //    {
    //        Vector3 target = new Vector3(_spark.transform.position.x, _spark.transform.position.y, stave.transform.position.z);
    //        _spark.transform.position = target;
    //    }
    //}
}
