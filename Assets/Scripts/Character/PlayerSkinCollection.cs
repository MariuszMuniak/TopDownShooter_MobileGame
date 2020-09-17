using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TDS_MG.Saving;

namespace TDS_MG.Character
{
    public class PlayerSkinCollection : MonoBehaviour, ISaveable
    {
        [SerializeField] CollectedSkin[] collectedSkins = new CollectedSkin[22];

        CollectedSkin activeSkin = null;
        PlayerSkinType defaultSkinType = PlayerSkinType.MaleHunter;

        private void Awake()
        {
            FindActiveSkin();
        }

        private void Start()
        {
            Activate(defaultSkinType);
        }

        private void FindActiveSkin()
        {
            foreach (CollectedSkin collectedSkin in collectedSkins)
            {
                if (collectedSkin.skinnedMesh.gameObject.activeSelf)
                {
                    activeSkin = collectedSkin;
                    break;
                }
            }
        }

        public int GetActiveSkinIndex() => (int)activeSkin.skinType;

        public bool IsOwned(PlayerSkinType skinType) => GetCollectedSkin(skinType).isOwned;

        public void TakePossession(PlayerSkinType skinType) => GetCollectedSkin(skinType).isOwned = true;

        private CollectedSkin GetCollectedSkin(PlayerSkinType skinType)
        {
            foreach (CollectedSkin collectedSkin in collectedSkins)
            {
                if (collectedSkin.skinType == skinType)
                {
                    return collectedSkin;
                }
            }

            return new CollectedSkin();
        }

        public void Activate(PlayerSkinType skinType)
        {
            if (activeSkin.skinnedMesh != null)
            {
                DeactivateCurrentSkin();
            }

            activeSkin = GetCollectedSkin(skinType);
            activeSkin.skinnedMesh.gameObject.SetActive(true);
        }

        private void DeactivateCurrentSkin()
        {
            activeSkin.skinnedMesh.gameObject.SetActive(false);
        }

        public object CaptureState()
        {
            CollectedSkinSaveData saveData = new CollectedSkinSaveData();
            List<bool> ownedList = new List<bool>();

            foreach (CollectedSkin collectedSkin in collectedSkins)
            {
                ownedList.Add(collectedSkin.isOwned);
            }

            saveData.activatePlayerSkinType = activeSkin.skinType;
            saveData.ownedList = ownedList;

            return saveData;
        }

        public void RestoreState(object state)
        {
            CollectedSkinSaveData saveData = (CollectedSkinSaveData)state;

            defaultSkinType = saveData.activatePlayerSkinType;
            List<bool> ownedList = saveData.ownedList;

            for (int i = 0; i < collectedSkins.Length; i++)
            {
                collectedSkins[i].isOwned = ownedList[i];
            }
        }

        [System.Serializable]
        private class CollectedSkin
        {
            public PlayerSkinType skinType;
            public bool isOwned = false;
            public SkinnedMeshRenderer skinnedMesh = null;
        }

        [System.Serializable]
        private class CollectedSkinSaveData
        {
            public PlayerSkinType activatePlayerSkinType = PlayerSkinType.MaleHunter;
            public List<bool> ownedList = new List<bool>();
        }
    }
}
