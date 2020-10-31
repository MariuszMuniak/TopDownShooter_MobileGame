using System.Collections;
using System.Collections.Generic;
using TDS_MG.Core;
using TDS_MG.Saving;
using TDS_MG.SceneManagement;
using TDS_MG.Shop;
using TDS_MG.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS_MG.Control
{
    public class LevelController : MonoBehaviour, ISaveable
    {
        [SerializeField] float timeToDisplayVictoryPanel = 10f;
        [SerializeField] float timeToDisplayDefeatPanel = 2f;
        [SerializeField] int[] rewardsPerLevel = new int[0];

        const int MAX_SCENE_BUILD_INDEX = 5;

        EnemyGenerator enemyGenerator;
        GameplayPanelsMenager panelsMenager;
        bool isVictorySequenceStarted = false;
        int currentSceneBuildIndex = 0;
        bool[] complishedLevels = new bool[MAX_SCENE_BUILD_INDEX];

        private void Awake()
        {
            enemyGenerator = FindObjectOfType<EnemyGenerator>();
            panelsMenager = FindObjectOfType<GameplayPanelsMenager>();
            currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            complishedLevels[0] = true;
        }

        private void Update()
        {
            if (currentSceneBuildIndex == 0) { return; }

            if (enemyGenerator.AreAllZombiesDead() && !isVictorySequenceStarted)
            {
                StartCoroutine(VictorySequence());
            } 
        }

        private IEnumerator VictorySequence()
        {
            isVictorySequenceStarted = true;
            yield return new WaitForSeconds(timeToDisplayVictoryPanel);
            complishedLevels[currentSceneBuildIndex - 1] = true;
            FindObjectOfType<PlayerController>().DisableBehaviours();
            panelsMenager.ShowOnlyVictoryPanel();
            FindObjectOfType<Wallet>().AddMoney(GetReward(SceneManager.GetActiveScene().buildIndex));
            FindObjectOfType<SavingWrapper>().Save();
        }

        public void Defeat()
        {
            StartCoroutine(DefeatSequence());
        }

        private IEnumerator DefeatSequence()
        {
            yield return new WaitForSeconds(timeToDisplayDefeatPanel);
            panelsMenager.ShowOnlyDefeatPanel();
        }

        public int GetReward(int level)
        {
            if (level <= rewardsPerLevel.Length)
            {
                return rewardsPerLevel[level - 1];
            }

            return 0;
        }

        public int GetMaxSceneBuildIndex()
        {
            return MAX_SCENE_BUILD_INDEX;
        }

        public bool IsPreviousLevelComplished(int buildSceneIndex)
        {
            if (buildSceneIndex <= 1)
            {
                return true;
            }

            return complishedLevels[buildSceneIndex - 2];
        }

        public int GecCurrentSceneBuildIndex()
        {
            return currentSceneBuildIndex;
        }

        public object CaptureState()
        {
            return complishedLevels;
        }

        public void RestoreState(object state)
        {
            complishedLevels = (bool[])state;
        }
    }
}
