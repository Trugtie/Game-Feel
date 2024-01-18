using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,ILeanable
{
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _jumpInterval = 4f;
    [SerializeField] private float _changeDirectionInterval = 3f;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _knockbackThurst = 25f;

    private int _currentDirection;

    private Rigidbody2D _rigidBody;

    private Movement _movement;

    private ColorChanger _colorChanger;

    public Vector2 MoveDir => new Vector2(_currentDirection,0);

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _movement = GetComponent<Movement>();

        _colorChanger = GetComponent<ColorChanger>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player == null) { return; }

        Ihitable iHitable = collision.gameObject.GetComponent<Ihitable>();
        iHitable?.TakeHit();

        IDamgeable iDamgeable = collision.gameObject.GetComponent<IDamgeable>();

        Vector2 damgeDir = (collision.transform.position - transform.position).normalized;

        iDamgeable?.TakeDamge(damgeDir, _damageAmount, _knockbackThurst);
    }
}
