using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "KikiExtension/ScriptableObjects/LevelScriptable", order = 1)]
public class LevelScriptable : ScriptableObject
{
	[AssetsOnly, AssetList(Path = "Prefabs/LevelPrefabs")] public GameObject levelPrefab;
}
