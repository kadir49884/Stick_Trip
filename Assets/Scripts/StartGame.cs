using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    public event UnityAction GameStarted;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameStarted?.Invoke();
            gameObject.SetActive(false);
        }
    }
}