using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
	[SerializeField]
	private Camera orthoCamera;
    public Camera OrthoCamera { get => orthoCamera; set => orthoCamera = value; }
    public Transform CameraTransform { get => cameraTransform; set => cameraTransform = value; }
    public Transform StaveTransform { get => staveTransform; set => staveTransform = value; }
    public Transform DogObject { get => dogObject; set => dogObject = value; }
    public Transform GameWinPanel { get => gameWinPanel; set => gameWinPanel = value; }
    public Transform GameFailPanel { get => gameFailPanel; set => gameFailPanel = value; }
    public Material MaterialGray { get => materialGray; set => materialGray = value; }

    [SerializeField]
    private Transform cameraTransform;
    
    [SerializeField]
    private Transform staveTransform;

    [SerializeField]
    private Transform dogObject;

    [SerializeField]
    private Transform gameWinPanel;
    [SerializeField]
    private Transform gameFailPanel;

    [SerializeField]
    private Material materialGray;
}