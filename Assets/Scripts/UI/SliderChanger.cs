using UnityEngine;
using UnityEngine.UI;

public class SliderChanger : MonoBehaviour
{
    [SerializeField] private Slider _distanceTraveled;
    [SerializeField] private Transform _endPoint;

    private float _allDistance;
    private float _sliderSpeed = 0.5f;

    private void Start()
    {
        _allDistance = Mathf.Abs(transform.position.z - _endPoint.transform.position.z);
    }
    
    private void Update()
    {
        _distanceTraveled.value =
            Mathf.Lerp(_distanceTraveled.value, transform.position.z / _allDistance, Time.deltaTime * _sliderSpeed);
    }
}
