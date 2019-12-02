using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverMars
{
    public abstract class Turret : MonoBehaviour
    {
#pragma warning disable 0649

        [SerializeField] private protected WeaponEquipmentItem _weaponEquipmentItem; // Item for this turret
        [SerializeField] private GameObject _turretBase; // Base platform of the turret that rotates horizontally
        [SerializeField] private protected GameObject _turretCannons; // Cannons of the turret that totates vertically
        [SerializeField] private protected GameObject _shootPlace;
        [SerializeField] private protected GameObject _shootAnimationPrefab;

#pragma warning restore 0649

        private protected AudioSource _audioSource;
        private protected List<string> _targetTags; // Targets for this turret
        private protected Transform _target;
        private protected Transform _targetPart;

        private Vector3 _aimPoint; // The point that the turret should look at

        private float _turnRate; // Turret turning speed
        private protected float _turretRange;
        private protected float _maxCooldown;
        private protected float _currentCooldown;

        [Range(0.0f, 180.0f)] private float _rightTraverse; // Maximum right turn in degrees
        [Range(0.0f, 180.0f)] private float _leftTraverse; // Maximum left turn in degrees

        private float _defaultTimeToFindTarget = 1f;
        private float _currentTimeToFindTarget;

        public float Range => _turretRange;
        public float RightTraverse => _rightTraverse;
        public float LeftTraverse => _leftTraverse;

        public abstract void Shoot();

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start() // Start is called on the frame when a script is enabled just before any of the Update methods are called the first time
        {
            SetTurretParameters();
        }

        public virtual void SetTurretParameters()
        {
            _turnRate = _weaponEquipmentItem.TurnRate;
            _turretRange = _weaponEquipmentItem.Range;
            _maxCooldown = _weaponEquipmentItem.ReloadTime;
            _currentCooldown = _maxCooldown;

            _rightTraverse = _weaponEquipmentItem.ShootingAngle / 2f;
            _leftTraverse = _weaponEquipmentItem.ShootingAngle / 2f;

            // Check this turret tag
            switch (transform.root.tag)
            {
                case "Player":  // If this turret is on a player ship
                    _targetTags = new List<string> { "Enemy" }; // Set enemies as targets for this turret
                    break;
                case "Ally": // If this turret is on an ally ship
                    _targetTags = new List<string> { "Enemy" }; // Set enemies as targets for this turret
                    break;
                case "Enemy": // If this turret is on an enemy ship
                    _targetTags = new List<string> { "Player", "Ally" }; // Set allies as targets for this turret
                    break;
                default:
                    break;
            }
        }

        private void FixedUpdate() // FixedUpdate is called at a fixed framerate frequency
        {
            //if (PlayerController.IsMenuOpened)
            //{
            //    return;
            //}

            FindTarget();
            AutomaticTurretControl();
            CooldownDecrease();
        }

        // Decrease turret cooldown each fixed update
        public void CooldownDecrease()
        {
            if (_currentCooldown >= 0)
            {
                _currentCooldown -= Time.deltaTime;
            }
        }

        public bool CooldownIsZero()
        {
            if (_currentCooldown <= 0)
            {
                return true;
            }
            return false;
        }

        // Method executes when turret AI is enabled
        public void AutomaticTurretControl()
        {
            if (_targetPart != null) // If there is any target
            {
                _aimPoint = _targetPart.position; // Set a target position as an aim point

                RotateTurret();
            }
            else // If there is no target
            {
                RotateToDefault(); // Rotate turret to default
            }

            // If the turret is aimed at the enemy, its cooldown is zero and it is not aimed at the owner
            if (CooldownIsZero() && AimedAtEnemy() && !AimedAtOwner())
            {
                Shoot();
            }
        }

        public void RotateTurret()
        {
            // Get local position of aim point in relative to this turret
            Vector3 localTargetPos = transform.InverseTransformPoint(_aimPoint);
            localTargetPos.y = 0f; // Put the aiming point at the same height with this tower

            Vector3 clampedLocalVector2Target = localTargetPos; // New point to rotate with clamped rotate traverses

            float traverse = localTargetPos.x >= 0 ? _rightTraverse : _leftTraverse;
            clampedLocalVector2Target = Vector3.RotateTowards(Vector3.forward, localTargetPos, Mathf.Deg2Rad * traverse, float.MaxValue);

            Quaternion rotationGoal = Quaternion.LookRotation(clampedLocalVector2Target); // Create a new rotation that looking at new point
                                                                                          // Rotates current turret to the new quaternion
            Quaternion newRotation = Quaternion.RotateTowards(_turretBase.transform.localRotation, rotationGoal, _turnRate * Time.deltaTime);

            _turretBase.transform.localRotation = newRotation; // Apply intermediate rotation to the turret
        }

        public void RotateToDefault()
        {
            // Set new intermediate rotation of base and cannons to default rotation
            Quaternion newBaseRotation = Quaternion.RotateTowards(_turretBase.transform.localRotation, Quaternion.identity, _turnRate * Time.deltaTime);
            Quaternion newCannonRotation = Quaternion.RotateTowards(_turretCannons.transform.localRotation, Quaternion.identity, 2.0f * _turnRate * Time.deltaTime);

            // Apply intermediate rotation
            _turretBase.transform.localRotation = newBaseRotation;
            _turretCannons.transform.localRotation = newCannonRotation;
        }

        public virtual bool AimedAtEnemy()
        {
            // Select specific layers by shifting the bits. These layers will be ignored by the turret raycast
            // Layer 8 is a bullet and 9 is a missile
            int layerMask = (1 << 8) | (1 << 9);
            layerMask = ~layerMask; // Invert these layers. So raycast will ignore bullets and missiles

            // Create an outgoing ray from cannons with turret range lenght
            Ray aimingRay = new Ray(_turretCannons.transform.position, _turretCannons.transform.forward * _turretRange);

            // If the turret is targeting an object except bullets and rockets (determined by layerMask)
            if (Physics.Raycast(aimingRay, out RaycastHit hit, _turretRange, layerMask))
            {
                if (hit.collider.transform.root == _target) // If aiming on current nearest target
                {
                    return true; // Aimed at the enemy
                }
            }
            return false; // Not aimed at the enemy
        }

        // If the turret aimed at the ship on which it is attached
        public bool AimedAtOwner()
        {
            // Create an outgoing ray from cannons with turret range lenght
            Ray aimingRay = new Ray(_turretCannons.transform.position, _turretCannons.transform.forward * _turretRange);

            if (Physics.Raycast(aimingRay, out RaycastHit hit, _turretRange)) // If turret aiming at some object
            {
                if (hit.transform.root == transform.root) // If this object is the current ship on which turret is attached
                {
                    return true; // Aimed at the owner
                }
            }
            return false; // Not aimed at the owner
        }

        private void FindTarget()
        {
            _currentTimeToFindTarget -= Time.fixedDeltaTime;

            if (_currentTimeToFindTarget <= 0)
            {
                _targetPart = Methods.SearchNearestTarget(transform, _targetTags, out Transform target);
                _target = target;
                _currentTimeToFindTarget = _defaultTimeToFindTarget;
            }
        }
    }
}
