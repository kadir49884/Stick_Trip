using DG.Tweening;
using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stave : MonoBehaviour
{
    [SerializeField] private GameObject _smallPartTemplate;
    [SerializeField] private float _lenghToCut;
    [SerializeField] private float _moveCenterSpeed;

    private Player _player;
    private Vector3 _stavePosition;
    private Vector3 _smallPartPos;
    private Vector3 _largePartPos;
    private float _cutPoint;
    private float _differenceValue;
    private float _largePartScale;
    private float _smallPartScale;
    private float _smallPartXPos;
    private float _largePartXPos;
    private bool _isInCenter = true;

    private float slapForceValue = 200;
    private Transform cameraTransform;
    private bool isOnFinish;

    private int barTriggerCount = 0;

    public int BarTriggerCount { get => barTriggerCount; set => barTriggerCount = value; }

    public event UnityAction<float> SizeChanged;


    private ObjectManager objectManager;
    private GameManager gameManager;
    private AudioSource audioSource;

    private List<AudioClip> audioClipsList = new List<AudioClip>();

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        gameManager = GameManager.Instance;
    }


    private void Start()
    {
        cameraTransform = ObjectManager.Instance.CameraTransform;
        objectManager = ObjectManager.Instance;
        audioSource = objectManager.GetComponent<AudioSource>();


        for (int i = 0; i < gameManager.SoundManager.soundList.Count; i++)
        {
            audioClipsList.Add(gameManager.SoundManager.GetAudioClipWithIndex(i));
        }
    }

    private void OnEnable()
    {
        _player.FireStepped += OnStepOnFire;
        _player.BonusGoted += OnBonusGot;
    }

    private void OnDisable()
    {
        _player.FireStepped -= OnStepOnFire;
        _player.BonusGoted -= OnBonusGot;
    }

    private void Update()
    {
        if (!_isInCenter)
        {
            Vector3 target = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, _moveCenterSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - _player.transform.position.x) < 0.03f)
            {
                _isInCenter = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("StaveOnRoad"))
        {
            cameraTransform.GetComponent<CameraMovement>().ChangeDistance();
            BoneMove();
            audioSource.clip = audioClipsList[0];
            audioSource.Play();
        }

        if (other.gameObject.TryGetComponent(out Saw saw))
        {

            if (SetStavesPositionsAndLocalScale(saw))
            {
                ChangeCurrentStavePos(_largePartPos, _largePartScale);
                CreateSmallPartStave(_smallPartPos, _smallPartScale);

                StartCoroutine("StartMoveCenter");
            }
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;
            cameraTransform.GetComponent<CameraMovement>().ChangeDistance();
            transform.DOLocalMoveZ(0.7f, 0.5f);
            transform.DOLocalMoveX(0, 0.5f);
            audioSource.clip = audioClipsList[1];
            audioSource.Play();
            DOVirtual.DelayedCall(0.5f, () =>
             {
                 transform.DOLocalMoveZ(0.7f, 0.5f);
                 transform.DOLocalMoveX(0, 0.5f);

             });
            BoneMove();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            isOnFinish = true;
            VibrationController.ContinuousHaptics(1, 1, 3);
            DOVirtual.DelayedCall(2f, () =>
            {
                audioSource.clip = audioClipsList[3];
                audioSource.Play();
            });
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("People"))
        {

            Transform peopleTransform = other.transform;
            peopleTransform.GetComponent<CapsuleCollider>().enabled = false;
            VibrationController.Vibrate(HapticPatterns.PresetType.HeavyImpact);
            audioSource.clip = audioClipsList[2];
            audioSource.Play();
            GameObject hitParticle = Instantiate(gameManager.ParticleManager.GetParticleWithName("HitParticle").gameObject);
            Vector3 particlePos = peopleTransform.position;
            particlePos.y += 3;
            particlePos.z -= 2f;
            hitParticle.transform.position = particlePos;
            
            peopleTransform.DOLocalRotate(new Vector3(peopleTransform.localRotation.x - 45, peopleTransform.localRotation.y, peopleTransform.localRotation.z), 0.3f);
            peopleTransform.DOMoveY(peopleTransform.position.y + 2f, 0.3f);
            peopleTransform.GetComponent<Animator>().enabled = false;

            foreach (var item in peopleTransform.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.material = objectManager.MaterialGray;
            }

            foreach (var item in other.transform.GetComponentsInChildren<Rigidbody>())
            {
                item.isKinematic = false;
            }

            foreach (var item in other.transform.GetComponentsInChildren<Rigidbody>())
            {

                if (item.gameObject.name == "mixamorig:Spine" || item.gameObject.name == "mixamorig:Head")
                {
                    item.AddForce(slapForceValue * (-Vector3.forward * 10 + Vector3.up * 15));

                    if (peopleTransform.position.x > 0)
                    {
                        item.AddForce(slapForceValue * (Vector3.right * 8));
                    }
                    else
                    {
                        item.AddForce(slapForceValue * (Vector3.left * 8));
                    }
                }
                else
                {
                    item.AddForce(slapForceValue * (Vector3.forward * 5 + Vector3.up * 5));

                    if (peopleTransform.position.x > 0)
                    {
                        item.AddForce(slapForceValue * (Vector3.right * 3));
                    }
                    else
                    {
                        item.AddForce(slapForceValue * (Vector3.left * 3));
                    }
                }
            }
        }
    }

    private void BoneMove()
    {
        if (transform.localScale.y > 3)
        {
            transform.GetChild(0).GetComponents<DynamicBone>()[0].enabled = true;
            transform.GetChild(0).GetComponents<DynamicBone>()[1].enabled = true;
        }
        else
        {
            transform.GetChild(0).GetComponents<DynamicBone>()[0].enabled = false;
            transform.GetChild(0).GetComponents<DynamicBone>()[1].enabled = false;
        }
    }

    private IEnumerator StartMoveCenter()
    {
        yield return new WaitForSeconds(0.8f);

        _isInCenter = false;
    }

    private bool SetStavesPositionsAndLocalScale(Saw saw)
    {
        _cutPoint = saw.transform.position.x;
        _differenceValue = Mathf.Abs(transform.position.x - _cutPoint);

        _largePartScale = transform.localScale.y / 2 + _differenceValue / 2;
        _smallPartScale = transform.localScale.y - _largePartScale;

        if (_smallPartScale < 0)
        {
            return false;
        }

        if (transform.position.x < saw.transform.position.x)
        {
            _smallPartXPos = _cutPoint + _smallPartScale;
            _largePartXPos = _cutPoint - _largePartScale;
        }
        else
        {
            _smallPartXPos = _cutPoint - _smallPartScale;
            _largePartXPos = _cutPoint + _largePartScale;
        }

        _smallPartPos = GetStavePartPosition(_smallPartXPos);
        _largePartPos = GetStavePartPosition(_largePartXPos);

        return true;
    }

    private void ChangeCurrentStavePos(Vector3 largePartPos, float largePartScale)
    {
        transform.localScale = new Vector3(transform.localScale.x, largePartScale, transform.localScale.z);
        transform.position = largePartPos;

        SizeChanged?.Invoke(transform.localScale.y);

    }

    private void CreateSmallPartStave(Vector3 smallPartPos, float smallPartScale)
    {
        Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
        GameObject smallPartStave = Instantiate(_smallPartTemplate, smallPartPos, rotation);
        smallPartStave.transform.localScale = new Vector3(transform.localScale.x, smallPartScale, transform.localScale.z);
        Destroy(smallPartStave, 0.8f);
    }



    private void OnStepOnFire()
    {
        if (isOnFinish && transform.localScale.y <= 0.6f)
        {
            GameManager.Instance.GameWin();
        }

        if (transform.localScale.y < _lenghToCut)
            return;

        Vector3 lenghtToCut = new Vector3(0, _lenghToCut, 0);

        float partsSize = _lenghToCut / 2;

        float leftXPos = transform.position.x - transform.localScale.y + partsSize / 2;
        float rightXPos = transform.position.x + transform.localScale.y - partsSize / 2;

        Vector3 rightPos = GetStavePartPosition(rightXPos);
        Vector3 leftPos = GetStavePartPosition(leftXPos);

        CreateSmallPartStave(leftPos, partsSize);
        CreateSmallPartStave(rightPos, partsSize);

        transform.localScale -= lenghtToCut;
        SizeChanged?.Invoke(transform.localScale.y);
    }

    private Vector3 GetStavePartPosition(float xPosition)
    {
        return new Vector3(xPosition, transform.position.y, transform.position.z);

    }

    private void OnBonusGot(float bonusLenght)
    {
        if (bonusLenght > 0)
        {
            transform.localScale += new Vector3(0f, bonusLenght, 0.0f);
            SizeChanged?.Invoke(transform.localScale.y);
        }
    }

}