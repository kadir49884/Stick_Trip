using UnityEngine;

namespace Lofelt.NiceVibrations
{
    public static class VibrationController
    {
        public static void Vibrate(HapticPatterns.PresetType hapticType)
        {
            //MMVibrationManager.Haptic(hapticType,true,true,this,-1);
            //MMVibrationManager.Haptic(hapticType, false, true, this);
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
        }

        public static void ContinuousHaptics(float ContinuousAmplitude, float ContinuousFrequency, float ContinuousDuration)
        {
            // START
            HapticController.fallbackPreset = HapticPatterns.PresetType.LightImpact;
            HapticPatterns.PlayConstant(ContinuousAmplitude, ContinuousFrequency, ContinuousDuration);

            //// STOP
            //HapticController.Stop();
        }

        //public static void Vibrate(float instensity, float sharpness, float duration)
        //{
        //    //MMVibrationManager.Haptic(hapticType,true,true,this,-1);
        //    //MMVibrationManager.ContinuousHaptic(instensity, sharpness, duration, HapticTypes.None, this, true);
        //}

        public static void StopContinuousHaptic()
        {
            //MMVibrationManager.Haptic(hapticType,true,true,this,-1);
            //MMVibrationManager.StopContinuousHaptic(true);
            HapticController.Stop();
        }
    }
}