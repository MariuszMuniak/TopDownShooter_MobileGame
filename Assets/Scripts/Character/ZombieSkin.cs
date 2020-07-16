using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Character
{
    public class ZombieSkin : MonoBehaviour
    {
        [SerializeField] SkinnedMeshRenderer[] skins = new SkinnedMeshRenderer[1];

        public void SetRandomSkin()
        {
            DeactivateAllSkins();

            int index = Random.Range(0, skins.Length);
            skins[index].gameObject.SetActive(true);
        }

        private void DeactivateAllSkins()
        {
            foreach(SkinnedMeshRenderer skinnedMesh in skins)
            {
                skinnedMesh.gameObject.SetActive(false);
            }
        }
    }
}
