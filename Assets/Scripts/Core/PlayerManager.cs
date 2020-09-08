using System.Collections;
using System.Collections.Generic;
using TDS_MG.Animations;
using TDS_MG.Control;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS_MG.Core
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] PlayerSetUp inMainMenu;
        [SerializeField] PlayerSetUp inPlayMode;

        const string MAIN_MENU_SCENE_NAME = "MainMenu";

        Animator animator;
        PlayerController playerController;
        PlayerMainMenuAnimator mainMenuAnimator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            playerController = GetComponent<PlayerController>();
            mainMenuAnimator = GetComponent<PlayerMainMenuAnimator>();
        }

        private void Start()
        {
            PlayerConfigurationDependingOnScene();
        }

        private void PlayerConfigurationDependingOnScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == MAIN_MENU_SCENE_NAME)
            {
                SetUpPlayer(inMainMenu);
                playerController.DisableBehaviours();
                mainMenuAnimator.enabled = true;
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
            public RuntimeAnimatorController animatorController = null;
        }
    }
}
