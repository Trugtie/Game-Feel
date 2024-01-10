using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,ILeanable,IDamgeable
{
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _jumpInterval = 4f;
    [SerializeField] private float _changeDirectionInterval = 3f;

    private int _currentDirection;

    private Rigidbody2D _rigidBody;

    private Movement _movement;

    private ColorChanger _colorChanger;

    private Flash _flash;
    private Health _health;
    private Knockback _knockback;

    public Vector2 MoveDir => new Vector2(_currentDirection,0);

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _movement = GetComponent<Movement>();

        _colorChanger = GetComponent<ColorChanger>();

        _flash = GetComponent<Flash>();
        _health = GetComponent<Health>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start() {
        StartCoroutine(ChangeDirectionRoutine());
        StartCoroutine(RandomJumpRoutine());
    }

    public void Init(Color color)
    {
        _colorChanger.SetDefaultColor(color);
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            _currentDirection = UnityEngine.Random.Range(0, 2) * 2 - 1; // 1 or -1
            _movement.SetCurrentDir(_currentDirection);
            yield return new WaitForSeconds(_changeDirectionInterval);
        }
    }

    private IEnumerator RandomJumpRoutine() 
    {
        while (true)
        {
            yield return new WaitForSeconds(_jumpInterval);
            float randomDirection = Random.Range(-1, 1);
            Vector2 jumpDirection = new Vector2(randomDirection, 1f).normalized;
            _rigidBody.AddForce(jumpDirection * _jumpForce, ForceMode2D.Impulse);
        }
    }

    public void TakeDamge(int damgeAmout, float knockbackThurst)
    {
        _health.TakeDamage(damgeAmout);

        _knockback.GetKnockback(PlayerController.Instance.transform.position, knockbackThurst);
    }

    public void TakeHit()
    {
        _flash.StartFlash();
    }
}
