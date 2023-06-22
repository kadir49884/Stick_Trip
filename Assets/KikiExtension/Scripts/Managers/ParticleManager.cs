using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace KikiExtension.Managers
{
    [CreateAssetMenu(menuName = "KikiExtension/Managers/ParticleManager")]

    public class ParticleManager : ScriptableManager
    {
        public List <ParticleSystem> particlesList = new List<ParticleSystem>();

        public ParticleSystem GetParticleWithIndex(int index)
        {
            return particlesList[index];
        }
        public ParticleSystem GetParticleWithName(string particleName)
        {
            return particlesList.Find(manager => manager.name.Contains(particleName));
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