using System.Collections;
using System.Collections.Generic;
using TDS_MG.Animations;
using TDS_MG.Control;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS_MG.Core
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] PlayerSetUp inMainMenu;
        [SerializeField] PlayerSetUp inPlayMode;

        const string maniMenuSceneName = "MainMenu";

        Animator animator;
        PlayerController playerController;
        PlayerMainMenuAnimator menuAnimator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            playerController = GetComponent<PlayerController>();
            menuAnimator = GetComponent<PlayerMainMenuAnimator>();
        }

        private void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == maniMenuSceneName)
            {
                SetUpPlayer(inMainMenu);
                playerController.DisableBehaviours();
                menuAnimator.enabled = true;
            }
            else
            {
                SetUpPlayer(inPlayMode);
                playerController.ActivateBehaviours();
            }
        }

        private void SetUpPlayer(PlayerSetUp setUp)
        {
            animator.transform.localScale = new Vector3(setUp.avatarScale, setUp.avatarScale, setUp.avatarScale);

            if (setUp.animatorController != null)
            {
                animator.runtimeAnimatorController = setUp.animatorController;
            }
        }

        [System.Serializable]
        private class PlayerSetUp
        {
            public float avatarScale = 1f;
            public AnimatorController animatorController = null;
        }
    }
}
