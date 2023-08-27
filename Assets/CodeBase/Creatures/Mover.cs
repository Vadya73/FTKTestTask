using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Creatures
{
    public class Mover : MonoBehaviour, IInitializable
    {
        private Creature _creature;
        private Vector3 _defaultPosition;
        private bool _isMoving = false;

        public event Action OnMoveComplete;

        public void Initialize()
        {
            _creature = GetComponent<Creature>();
            _defaultPosition = transform.position;
        }

        public void MoveToTarget(Creature target)
        {
            if (_isMoving) return;

            Vector3 offset = target.transform.forward * 1;
            Vector3 pointToMove = target.transform.position + offset;

            StartCoroutine(Move(pointToMove));
        }

        public void MoveToDefaultPosition()
        {
            if (_isMoving || !gameObject.activeSelf) return;

            StartCoroutine(Move(_defaultPosition));
        }

        private IEnumerator Move(Vector3 positionToMove)
        {
            _isMoving = true;

            Quaternion originalRotation = transform.rotation;

            float distanceThreshold = 0.09f;

            while (Vector3.Distance(transform.position, positionToMove) > distanceThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, positionToMove, _creature.MoveSpeed * Time.deltaTime);
                transform.LookAt(positionToMove);
                yield return null;
            }

            transform.position = positionToMove;
            _isMoving = false;

            transform.rotation = originalRotation;

            yield return new WaitForSeconds(1f);

            if (_creature.CanAttack)
            {
                OnMoveComplete?.Invoke();
            }
        }
    }
}