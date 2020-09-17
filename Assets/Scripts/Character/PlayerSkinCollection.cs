using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TDS_MG.Character
{
    public class PlayerSkinCollection : MonoBehaviour
    {
        [SerializeField] CollectedSkin[] collectedSkins = new CollectedSkin[22];

        CollectedSkin activeSkin = null;

        private void Awake()
        {
            FindActiveSkin();
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
            if (activeSkin.skinnedMesh == null)
            {
                return;
            }

            DeactivateCurrentSkin();

            activeSkin = GetCollectedSkin(skinType);
            activeSkin.skinnedMesh.gameObject.SetActive(true);
        }

        private void DeactivateCurrentSkin()
        {
            activeSkin.skinnedMesh.gameObject.SetActive(false);
        }

        [System.Serializable]
        private class CollectedSkin
        {
            public PlayerSkinType skinType;
            public bool isOwned = false;
            public SkinnedMeshRenderer skinnedMesh = null;
        }
    }
}
