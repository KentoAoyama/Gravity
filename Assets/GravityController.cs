using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public static Vector2 _upGravity = new (0, 10);
    public static Vector2 _downGravity = new (0, -10);
    public static Vector2 _leftGravity = new (-10, 0);
    public static Vector2 _rightGravity = new(10, 0);

    [Tooltip("�E���Ɍ���Ray�̍��W"),SerializeField] Transform _underR;
    [Tooltip("�����Ɍ���Ray�̍��W"), SerializeField] Transform _underL;
    [Tooltip("Ray�������郌�C���["), SerializeField] LayerMask _groundLayer = default;

    public PlayerGravity _playerGravity = PlayerGravity.Down;

    bool _isRotate = false;

    float _rotate;
    
    
    void Update()
    {
        ChangeGravity();

        if (!_isRotate)
        {
            GravityRaycast();
        }
    }

    
    void ChangeGravity()
    {
        if (_playerGravity == PlayerGravity.Up)
        {
            Physics2D.gravity = _upGravity;
        }

        if (_playerGravity == PlayerGravity.Down)
        {
            Physics2D.gravity = _downGravity;
        }

        if (_playerGravity == PlayerGravity.Left)
        {
            Physics2D.gravity = _leftGravity;
        }

        if (_playerGravity == PlayerGravity.Right)
        {
            Physics2D.gravity = _rightGravity;
        }
    }


    /// <summary>Raycast���΂�����</summary>
    void GravityRaycast()
    {       
        if (!UnderRayCast(_underR))
        {
            _isRotate = true;

            StartCoroutine(DownToLeft()); 
        }
        else if (!UnderRayCast(_underL))
        {
            _isRotate = true;

            StartCoroutine(LeftToDown());
        }
    }

    
    /// <summary>Raycast�ɂ��ڒn����</summary>
    bool UnderRayCast(Transform _underPos)
    {
        Vector2 start = this.transform.position;

        Vector2 vec = _underPos.position - transform.position;
        RaycastHit2D underHit = Physics2D.Linecast(start, start + vec, _groundLayer);
        Debug.DrawLine(start, start + vec);

        return underHit.collider;
    }


    /// <summary>�d�͂�������E�ɕύX</summary>
    IEnumerator DownToLeft()
    {
        _playerGravity = PlayerGravity.Up;
        yield return new WaitForSeconds(0.1f);

        _playerGravity = PlayerGravity.Right;
        yield return new WaitForSeconds(0.15f);

        transform.rotation = Quaternion.Euler(0, 0, _rotate -= 90);

        _playerGravity = PlayerGravity.Down;
        yield return new WaitForSeconds(0.3f);
        
        _playerGravity = PlayerGravity.Left;
    }

    
    /// <summary>�d�͂������牺�ɕύX</summary>
    IEnumerator LeftToDown()
    {        
        _playerGravity = PlayerGravity.Right;
        yield return new WaitForSeconds(0.1f);

        _playerGravity = PlayerGravity.Up;
        yield return new WaitForSeconds(0.15f);

        transform.rotation = Quaternion.Euler(0, 0, _rotate += 90);

        _playerGravity = PlayerGravity.Left;
        yield return new WaitForSeconds(0.3f);

        _playerGravity = PlayerGravity.Down;
    }

    void OnCollisionEnter2D(Collision2D collision)//�ڒn�������]���I���Ƃ���
    {
        _isRotate = false;
    }


    /// <summary>�v���C���[�ɂ������Ă���d��</summary>
    public enum PlayerGravity
    {
        /// <summary>��ɏd�͂���������</summary>
        Up,

        /// <summary>���ɏd�͂���������</summary>
        Down,

        /// <summary>���ɏd�͂���������</summary>
        Left,

        /// <summary>�E�ɏd�͂���������</summary>
        Right,
    }
}
