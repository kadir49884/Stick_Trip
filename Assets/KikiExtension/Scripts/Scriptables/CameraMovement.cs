using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KikiExtension;

public class CameraMovement : MonoBehaviour
{

	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	private Vector3 startOffset;
	[SerializeField] private float lerpTime = .1f;

	private Vector3 newPos;
	private bool isDistantly;

	private Vector3 distanceToChange = new Vector3(0, 0.5f, -0.7f);
	private Stave _stave;

	private Transform staveTransform;


	private void Start()
	{
		GameManager gameManager = GameManager.Instance;

		gameManager.GameWin += GameWin;
		gameManager.GameFail += GameFail;
		startOffset = offset;
		staveTransform = ObjectManager.Instance.StaveTransform;
	}

	private void Awake()
	{
		_stave = target.GetComponentInChildren<Stave>();
	}


	public Transform Target { get => target; set => target = value; }

	void FixedUpdate()
	{
		newPos = Target.localPosition + offset;
		newPos.x = 0;
		newPos.z = Target.localPosition.z + offset.z;
		transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, lerpTime);
	}

	public void ChangeDistance()
    {
		offset = startOffset + distanceToChange * staveTransform.localScale.y;
    }



	private void GameWin()
	{
		//	TODO 
	}
	private void GameFail()
	{
		this.enabled = false;
	}
}
