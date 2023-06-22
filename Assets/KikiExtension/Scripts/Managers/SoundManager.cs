using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace KikiExtension.Managers
{
    [CreateAssetMenu(menuName = "KikiExtension/Managers/SoundManager")]

    public class SoundManager : ScriptableManager
    {

        public List <AudioClip> soundList = new List<AudioClip>();

        public AudioClip GetAudioClipWithIndex(int index)
        {
            return soundList[index];
        }

        public AudioClip GetAudioClipWithName(string audioName)
        {
            return soundList.Find(manager => manager.name.Contains(audioName));
        }


        //private string levelInfo;

        //private void Awake()
        //{
        //    levelInfo = Path.Combine(KikiStatics.LEVELS, KikiStatics.LEVELSTABIL + PlayerPrefs.GetInt(KikiStatics.ACTIVE_LEVEL, 1));
        //    LevelScriptable levelScriptable = Resources.Load<LevelScriptable>(levelInfo);
        //    Debug.Log(levelScriptable);
        //    Initialize();
        //}


    }
}