using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using KikiExtension;
using KikiExtension.Managers;
using System.IO;
using UnityEditor;



public class GameManager : MonoBehaviour
{


	private bool IsGameFinish { get; set; }
	private bool IsGameStarted { get; set; }

	public Action GameStart { get; set; }
	public Action GameWin { get; set; }
	public Action GameFail { get; set; }

	public bool RunGame { get => IsGameStarted && !IsGameFinish; }


	public static GameManager Instance { get => instance; set => instance = value; }

	private static GameManager instance;

	[SerializeField]
	private bool isReadyForLevel;

	public ScriptableManager[] managers;
	public LevelManager LevelManager { get; private set; }
	public ParticleManager ParticleManager { get; private set; }
	public SoundManager SoundManager { get; private set; }

    private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (isReadyForLevel)
		{
			GetLevel();
		}
	}

	private void Start()
	{

		GameStart += Initialize;
		GameWin += Game_Win;
		GameFail += Game_Fail;

		ParticleManager = (ParticleManager)managers.Find(manager => manager.name.Contains("ParticleManager"));
		SoundManager = (SoundManager)managers.Find(manager => manager.name.Contains("SoundManager"));
        TinySauce.OnGameStarted(LevelManager.activeLevel.ToString());
    }

	private void GetLevel()
	{
		//getPath = Path.Combine("ScriptableObjects", "LevelManager");
		//LevelManager levelManager = Resources.Load<LevelManager>(getPath);
		LevelManager = (LevelManager)managers.Find(manager => manager.name.Contains("LevelManager"));
		LevelManager.Initialize();
	}


	public void Initialize()
	{
		IsGameStarted = true;
	}

	private void Game_Win()
	{
		IsGameFinish = true;
        TinySauce.OnGameFinished(true, 100, LevelManager.activeLevel.ToString());
        ObjectManager.Instance.GameWinPanel.gameObject.SetActive(true);
	}


	private void Game_Fail()
	{
		IsGameFinish = true;
        TinySauce.OnGameFinished(false, 50, LevelManager.activeLevel.ToString());
        ObjectManager.Instance.GameFailPanel.gameObject.SetActive(true);
	}

	public void NextLevel()
	{
		PlayerPrefsManager.LevelNext();
		RestartLevel();
		GetLevel();
	}
	public void PreviousLevel()
	{
		PlayerPrefsManager.PreviousLevel();
		RestartLevel();
		GetLevel();
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

#if UNITY_EDITOR
    private void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

        if (Input.GetKeyDown(KeyCode.N))
			NextLevel();

		if (Input.GetKeyDown(KeyCode.B))
			PreviousLevel();
	}
#endif

}



