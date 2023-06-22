using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimator : MonoBehaviour
{

    void Start()
    {
        transform.GetComponent<Animator>().SetFloat("IdleId", Random.Range(0, 6));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stave"))
        {
            transform.DOMoveZ(transform.position.z + 20, 6);
            transform.GetComponent<Animator>().SetTrigger("RunTrigger");
        }
    }
 
}
