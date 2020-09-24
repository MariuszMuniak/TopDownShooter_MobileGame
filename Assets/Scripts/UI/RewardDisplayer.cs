using System.Collections;
using System.Collections.Generic;
using TDS_MG.Control;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDS_MG.UI
{
    public class RewardDisplayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI rewardText = null;

        LevelController levelController;

        private void Awake()
        {
            levelController = FindObjectOfType<LevelController>();
        }

        private void Update()
        {
            rewardText.text = levelController.GetReward(SceneManager.GetActiveScene().buildIndex).ToString();
        }
    }
}
