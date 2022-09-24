using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyBase
{
    [Header("Raycast")]
    [SerializeField, Tooltip("�E����Ray���΂��ꏊ")] Transform _rightRayPos;
    [SerializeField, Tooltip("�E�㑤��Ray���΂��ꏊ")] Transform _uRightRayPos;
    [SerializeField, Tooltip("������Ray���΂��ꏊ")] Transform _leftRayPos;
    [SerializeField, Tooltip("���㑤��Ray���΂��ꏊ")] Transform _uLeftRayPos;
    [SerializeField, Tooltip("Ray�������郌�C���[")] LayerMask _groundLayer;

    [Header("Move")]
    [SerializeField, Tooltip("�ړ��̑��x")] float _moveSpeed = 10f;
    [SerializeField, Tooltip("�������ɉ��������鑬�x")] float _upSpeed = 10f;
    [SerializeField, Tooltip("�͂�������Ԋu")] float _impulseInterval = 1f;
    [SerializeField, Tooltip("�^�[���ɂ����鎞��")] float _turnTime = 1f;
    [Tooltip("�^�[�������Ă��邩")] bool _isTurn ;

    [Header("Gravity")]
    [SerializeField, Tooltip("�d�͂̑傫��")] float _gravityLevel = 20f;

    [Header("Damage")]
    [SerializeField, Tooltip("�v���C���[�ɗ^����_���[�W")] int _playerDamage = 10;
    [SerializeField, Tooltip("�������H�炤�_���[�W")] int _damage = 1;

    int _dir = 1;
    float _timer;

    Rigidbody2D _rb;   


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.gravityScale = 0;
        _timer = _impulseInterval;
    }


    void FixedUpdate()
    {        
        Warning();
    }
    

    /// <summary>�G�P�̓���</summary>
    public override void Move()
    {
        _rb.AddForce(-transform.up * _gravityLevel, ForceMode2D.Force);�@//��Ɏ������猩�ĉ��ɗ͂�������

        //�_���[�W���󂯂��瓮�����~�߂�
        if (_isDamage)
        {
            _rb.velocity = Vector2.zero;
            _timer = 0;
        }
        //�ʏ�̈ړ��̏���
        else if (!_isTurn)
        {
            _timer += Time.deltaTime;
            
            if (_timer > _impulseInterval)
            {
                _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);�@//���̊Ԋu�ŗ͂�������
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }

        //Ray���n�ʂɂ������Ă��炸�A�^�[�����łȂ���΃^�[��������
        if (!GroundRay(_rightRayPos) && !_isTurn || !GroundRay(_leftRayPos) && !_isTurn ||
            GroundRay(_uRightRayPos) && !_isTurn || GroundRay(_uLeftRayPos) && !_isTurn)
        {
            StartCoroutine(EnemyTurn());�@
        }
    }


    /// <summary>Raycast�ɂ��ڒn����</summary>
    bool GroundRay(Transform rayPos)
    {
        Vector2 start = transform.position;

        Vector2 vec = rayPos.position - transform.position;
        RaycastHit2D hit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return hit.collider;  //Ray�̔����Ԃ�
    }


    IEnumerator EnemyTurn()
    {
        _isTurn = true;
        _dir *= -1;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(_turnTime / 2);  //�ړ��̌������t�ɂ���velocity��zero�ɂ���

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        yield return new WaitForSeconds(_turnTime / 2);�@//�����𔽓]������

        _rb.AddForce(transform.right * _moveSpeed * _dir, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_impulseInterval);�@//���]���������Ɉړ�������
        
        _timer = _impulseInterval;
        _isTurn = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IAddDamage addDamage))
        {
            addDamage.AddDamage(_playerDamage);
            AddDamage(_damage);
        }
    }


    void Warning()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _warningDis)
        {
            WarningMove();
            _isWarning = true;
        }
    }


    void WarningMove()
    {
        if (!_isWarning)
        {
           
            _moveSpeed += _upSpeed;
        }
    }


    public override void Damage()
    {
        _rb.velocity = Vector2.zero;
    }
}
