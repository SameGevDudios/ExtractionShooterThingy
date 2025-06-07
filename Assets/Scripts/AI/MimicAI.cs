using UnityEngine;
using UnityEngine.AI;

public class MimicAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private GameObject _player;
    [SerializeField] private BoxCollider _scoutZone;
    [SerializeField] private float _newScoutPointTimer, _followPlayerTimer, _checkPlayerTimer;
    private float _scoutCooldown, _followCooldown;
    private bool _followPlayer;

    private RaycastHit[] _hits = new RaycastHit[16];

    private void Start()
    {
        ResetFollowCooldown();
        CheckPlayer();
    }
    private void CheckPlayer()
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
                _followCooldown -= _checkPlayerTimer;
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
            if (_scoutCooldown <= 0)
                ResetScout();
            _followPlayer = PlayerInSight();
        }
        Invoke("CheckPlayer", _checkPlayerTimer);
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
        float playerHeight = 2f;
        Physics.RaycastNonAlloc(transform.position, _player.transform.position - transform.position + Vector3.up * playerHeight / 2, _hits);
        RaycastHitSorter.SortByDistance(_hits);
        if (_hits[0].collider == null) return false;
        return _hits[0].collider.CompareTag("Player");
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
