using DG.Tweening;
using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    // Start is called before the first frame update

    private GameManager gameManager;
    private Animator dogAnim;
    private PlayerMovementController playerMovementController;
    [SerializeField]
    private GameObject sparkParent;
    [SerializeField]
    private GameObject smokeParticle;




    private void Awake()
    {
        gameManager = GameManager.Instance;
    }


 

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.GameStart += GameStarted;
        dogAnim = transform.GetComponent<Animator>();
        playerMovementController = transform.parent.GetComponent<PlayerMovementController>();
        gameManager.GameWin += GameWin;
     
    }

    private void GameWin()
    {
        dogAnim.SetInteger("AnimStatus", 3);
    }

    private void GameStarted()
    {
        dogAnim.SetInteger("AnimStatus", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") && dogAnim.GetInteger("AnimStatus") != 1)
        {
            dogAnim.SetInteger("AnimStatus", 1);
            smokeParticle.gameObject.SetActive(true);
            transform.DOLocalRotate(new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z), 0.1f);
        }
        else if (other.gameObject.CompareTag("Bar") && dogAnim.GetInteger("AnimStatus") != 2)
        {
            dogAnim.SetInteger("AnimStatus", 2);
            transform.DOLocalRotate(new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z), 0.5f);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                VibrationController.ContinuousHaptics(1, 1, 3);
                sparkParent.gameObject.SetActive(true);
                smokeParticle.gameObject.SetActive(false);
            });
            //audioSource.clip = audioClipsList[3];
            //audioSource.Play();
        }
        else if (other.gameObject.CompareTag("FailPlane"))
        {
            playerMovementController.PlayerSpeed = 0;
            gameManager.GameFail();
        }

        if (other.gameObject.CompareTag("Bar"))
        {
            //barTriggerCount++;
            //if (barTriggerCount > 1)
            //{
            //    playerMovementController.gameObject.GetComponentInParent<Player>().FreezeYPos();
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            sparkParent.gameObject.SetActive(false);
            VibrationController.StopContinuousHaptic();
            //audioSource.clip = audioClipsList[3];
            //audioSource.Play();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            smokeParticle.gameObject.SetActive(false);
        }
    }


}
