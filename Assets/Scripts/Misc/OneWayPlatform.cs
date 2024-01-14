using System.Collections;
using UnityEngine;


public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private float _ignoreColliderTime = 1f;

    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        EnableIgnorePhysic();
    }

    private void EnableIgnorePhysic()
    { 
        if(!PlayerController.Instance.CheckOnPlatform()) { return; }

        if (PlayerController.Instance.MoveDir.y < 0)
        {
           StartCoroutine(DisablePlatformRoutine());
        }
    }

    private IEnumerator DisablePlatformRoutine()
    {
        CapsuleCollider2D[] playerColliders = PlayerController.Instance.GetComponents<CapsuleCollider2D>();

        foreach(CapsuleCollider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(playerCollider, _collider2D, true);
        }

        yield return new WaitForSeconds(_ignoreColliderTime);

        foreach(CapsuleCollider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(playerCollider,_collider2D,false);
        }
    }
}
