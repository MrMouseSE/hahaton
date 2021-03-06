﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class EnemyController : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private Transform _target;
        
        public GameObject _meshTransform;
        public ParticleSystem _particle;
        public List<AudioClip> _audioClips;
        public AudioSource _audioSource;
        
        public EnemyContainer _enemyContainer;

        public void Init(EnemySpawner enemySpawner, Transform target)
        {
            _enemySpawner = enemySpawner;
            _target = target;
            _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Count)];
        }

        private void Update()
        {
            if (_target == null) return;
            if (Vector3.Distance(transform.position,_target.position)>50)
            {
                SelfDestruction();
            }

            if (_enemyContainer.IsStatic) return;
            MoveTo();
        }
        
        private void MoveTo()
        {
            if (_target == null) return;

            transform.LookAt(_target);
            float step = _enemyContainer.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_target == null) return;
            
            var obj = other.gameObject;
            if (obj.layer == 10 || obj.layer == 9) return;
            
            if (obj.layer == 8)
            {
                TakeDamage(obj.GetComponent<BulletContainer>().Damage);
                return;
            }

            if (obj.layer == 11)
            {
                StartCoroutine(SelfDestruction());
            }

            //StartCoroutine(SelfDestruction());
        }

        private void TakeDamage(float damage)
        {
            _enemyContainer.Health -= damage;
            if (_enemyContainer.Health <= 0)
            {
                StartCoroutine(SelfDestruction());
            }
        }

        private IEnumerator SelfDestruction()
        {
            _enemySpawner.Enemyes.Remove(gameObject);
            _target = null;
            _meshTransform.SetActive(false);
            _audioSource.Play();
            _particle.Play();
            yield return new WaitForSeconds(_particle.main.duration);
            Destroy(gameObject);
        }
    }
}