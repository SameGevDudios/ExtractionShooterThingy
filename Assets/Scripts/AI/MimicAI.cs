using UnityEngine;
using UnityEngine.AI;

public class MimicAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private GameObject _player;
    [SerializeField] private BoxCollider _scoutZone;
    [SerializeField] private float _newScoutPointTimer, _followPlayerTimer;
    private float _scoutCooldown, _followCooldown;
    private bool _followPlayer;
    private void Start()
    {
        ResetFollowCooldown();
    }
    private void Update()
    {
        if (_followPlayer)
        {
            _navMesh.destination = _player.transform.position;
            if (PlayerInSight())
            {
                ResetFollowCooldown();
            }
            else
            {
                _followCooldown -= Time.deltaTime;
                if (_followCooldown <= 0)
                {
                    ResetScout();
                    _followPlayer = false;
                }
            }
        }
        else
        {
            _scoutCooldown -= Time.deltaTime;
            if(_scoutCooldown <= 0)
                ResetScout();
            _followPlayer = PlayerInSight();
        }
    }

    private void ResetFollowCooldown()
    {
        _followCooldown = _followPlayerTimer;
    }

    private void ResetScout()
    {
        _navMesh.destination = GetRandomScoutPoint();
        _scoutCooldown = _newScoutPointTimer;
    }
    private bool PlayerInSight()
    {
        Physics.Raycast(transform.position, _player.transform.position, out RaycastHit hit);
        return hit.collider.gameObject == _player;
    }
    private Vector3 GetRandomScoutPoint()
    {
        return new Vector3(
            Random.Range(_scoutZone.bounds.min.x, _scoutZone.bounds.max.x),
            Random.Range(_scoutZone.bounds.min.y, _scoutZone.bounds.max.y),
            Random.Range(_scoutZone.bounds.min.z, _scoutZone.bounds.max.z)
        );
    }
}
