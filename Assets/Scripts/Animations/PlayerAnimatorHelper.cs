using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Animations
{
    public class PlayerAnimatorHelper : MonoBehaviour
    {
        [SerializeField] AnimationData[] animationDatas = new AnimationData[0];

        public float GetShootAnimationSpeed(WeaponType weaponType)
        {
            return GetAnimationData(weaponType).shootAnimationSpeed;
        }

        private AnimationData GetAnimationData(WeaponType weaponType)
        {
            foreach (AnimationData animationData in animationDatas)
            {
                if (animationData.weaponType == weaponType)
                {
                    return animationData;
                }
            }

            return new AnimationData();
        }

        [System.Serializable]
        private class AnimationData
        {
            public WeaponType weaponType = WeaponType.NoWeapon;
            public float shootAnimationSpeed = 1f;
        }
    }
}
