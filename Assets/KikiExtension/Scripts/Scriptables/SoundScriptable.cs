using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "KikiExtension/ScriptableObjects/SoundScriptable", order = 3)]
public class SoundScriptable : ScriptableObject
{
	public List<AudioClip> audioClipsList = new List<AudioClip>();
}