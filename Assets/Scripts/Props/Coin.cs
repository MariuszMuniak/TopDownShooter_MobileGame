using System.Collections;
using System.Collections.Generic;
using TDS_MG.Shop;
using UnityEngine;

namespace TDS_MG.Props
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] int minValue = 10;
        [SerializeField] int maxValue = 100;
        [SerializeField] float rotationSpeed = 1f;
        [SerializeField] Transform coinModel = null;
        [SerializeField] GameObject pickUpEffect = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InstantiatePickUpEffekt();
                GivePlayerRandomValue(other);
                DisableColliderAndMeshRenderer();
                PlaySoundEffect();
                Destroy(gameObject, 5f);
            }
        }

        private void GivePlayerRandomValue(Collider other)
        {
            Wallet wallet = other.GetComponent<Wallet>();
            int randomValue = Random.Range(minValue, maxValue + 1);
            wallet.AddMoney(randomValue);
        }

        private void InstantiatePickUpEffekt()
        {
            if (pickUpEffect != null)
            {
                GameObject instantiatedEffect = Instantiate(pickUpEffect, coinModel.position, Quaternion.identity);
                Destroy(instantiatedEffect, 5f);
            }
        }

        private void DisableColliderAndMeshRenderer()
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        private void PlaySoundEffect()
        {
            AudioSource audioSource = GetComponentInChildren<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
