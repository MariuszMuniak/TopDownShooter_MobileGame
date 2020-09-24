using System.Collections;
using System.Collections.Generic;
using TDS_MG.Core;
using TDS_MG.SceneManagement;
using TDS_MG.Shop;
using TDS_MG.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS_MG.Control
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] float timeToDisplayVictoryPanel = 10f;
        [SerializeField] float timeToDisplayDefeatPanel = 2f;
        [SerializeField] int[] rewardsPerLevel = new int[0];

        EnemyGenerator enemyGenerator;
        GameplayPanelsMenager panelsMenager;
        bool isVictorySequenceStarted = false;

        private void Awake()
        {
            enemyGenerator = FindObjectOfType<EnemyGenerator>();
            panelsMenager = FindObjectOfType<GameplayPanelsMenager>();
        }

        private void Update()
        {
            if (enemyGenerator.AreAllZombiesDead() && !isVictorySequenceStarted)
            {
                StartCoroutine(VictorySequence());
            }
        }

        private IEnumerator VictorySequence()
        {
            isVictorySequenceStarted = true;
            yield return new WaitForSeconds(timeToDisplayVictoryPanel);
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
    }
}
