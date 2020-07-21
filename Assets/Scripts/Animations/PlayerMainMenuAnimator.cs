using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

namespace TDS_MG.Animations
{
    public class PlayerMainMenuAnimator : MonoBehaviour
    {
        [SerializeField] float minTimeBetweenState = 1f;
        [SerializeField] float maxTimeBetweenState = 2f;
        [Space]
        [SerializeField] IdleState[] idleStates = new IdleState[0];

        Animator animator;
        bool isPlayingIdleState = false;
        float timeSinceStartStartPlaying = 0f;
        float timeToNextIdleStat = 2f;
        float playTime = 1f;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            enabled = false;
            timeToNextIdleStat = RandomTimeBetweenState();
        }

        private void Update()
        {
            if (CanPlayNextIdleState())
            {
                PlayRandomIdleState();
            }

            UpdateAnimator();

            timeSinceStartStartPlaying += Time.deltaTime;
        }

        private void UpdateAnimator()
        {
            isPlayingIdleState = timeSinceStartStartPlaying <= playTime;

            animator.SetBool("PlayIdleStat_b", isPlayingIdleState);
        }

        private bool CanPlayNextIdleState()
        {
            return timeSinceStartStartPlaying >= timeToNextIdleStat;
        }

        private void PlayRandomIdleState()
        {
            int statIndex = Random.Range(0, idleStates.Length);

            animator.SetInteger("StateIndex_i", statIndex);

            IdleState idleState = idleStates[statIndex];

            if (idleState.isSingleShot)
            {
                playTime = 0.5f;
            }
            else
            {
                playTime = idleState.playTime;
                timeToNextIdleStat = playTime + RandomTimeBetweenState();
            }

            timeSinceStartStartPlaying = 0f;
        }

        private float RandomTimeBetweenState()
        {
            return Random.Range(minTimeBetweenState, maxTimeBetweenState);
        }

        [System.Serializable]
        private class IdleState
        {
            public int stateIndex = 0;
            public bool isSingleShot = false;
            public float playTime = 1f;
        }
    }
}