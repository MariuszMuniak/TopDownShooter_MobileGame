using System.Collections;
using System.Collections.Generic;
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
        float timeSinceAnimationStartedPlaying = Mathf.Infinity;
        float timeToNextIdleState;
        int currentIdleStateIndex;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            enabled = false;
            timeToNextIdleState = RandomTimeBetweenState();
        }

        private void Update()
        {
            if (CanPlayNextIdleState())
            {
                currentIdleStateIndex = PlayRandomIdleState();
                SetNewTimeToNextIdleState();
            }

            animator.SetBool("PlayIdleStat_b", IsPlayingIdleStateAnimation());
            UpdateTimer();
        }

        private float RandomTimeBetweenState()
        {
            return Random.Range(minTimeBetweenState, maxTimeBetweenState);
        }

        private bool CanPlayNextIdleState()
        {
            return timeSinceAnimationStartedPlaying >= timeToNextIdleState;
        }

        private int PlayRandomIdleState()
        {
            int stateIndex = Random.Range(0, idleStates.Length);

            animator.SetInteger("StateIndex_i", stateIndex);
            timeSinceAnimationStartedPlaying = 0f;

            return stateIndex;
        }

        private void SetNewTimeToNextIdleState()
        {
            timeToNextIdleState = idleStates[currentIdleStateIndex].playTime + RandomTimeBetweenState();
        }

        private bool IsPlayingIdleStateAnimation()
        {
            return timeSinceAnimationStartedPlaying <= idleStates[currentIdleStateIndex].playTime;
        }

        private void UpdateTimer()
        {
            timeSinceAnimationStartedPlaying += Time.deltaTime;
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