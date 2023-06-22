using UnityEngine;

public class BonusStave : MonoBehaviour
{
    private float _lenght;

    public float Lenght => _lenght;

    private void Start()
    {
        _lenght = transform.localScale.y/2;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
