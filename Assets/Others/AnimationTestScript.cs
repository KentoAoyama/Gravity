using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AnimationTestScript : MonoBehaviour
{
    [SerializeField] Transform _pos;
    Script_SpriteStudio6_Root _spriteStudioRoot;
    [SerializeField] Vector3 _scale;

    //public ReactiveProperty<AnimationType> _ani = new();
    public AnimationType _ani = new();


    public enum AnimationType
    {
        animation_1,
        animation_2,
        animation_3,
        animation_4,
        animation_5,
        animation_6,
        animation_7
    }

    void Start()
    {
        transform.position = new(0, 0, 0);
        _spriteStudioRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);
        //_spriteStudioRoot = GetComponent<Script_SpriteStudio6_Root>();

        _scale.x = 0.0015f;
        _scale.y = 0.0015f;
        _scale.z = 1f;

        //��1����:�A�j���[�V�����̔ԍ�
        //��2����:���[�v�� 0�Ń��[�v
        //��3����:�A�j���[�V�����̍Đ��t���[���ԍ�
        //��4����:�A�j���[�V�����̍Đ����x
        _spriteStudioRoot.AnimationPlay((int)AnimationType.animation_1, 0, 0, 1);

        //animation���ς�����ۂɏ��������s����
        //_ani.Skip(1).Subscribe(a => );
    }


    void Update()
    {
        transform.localScale = _scale;
        transform.position = _pos.position;


        if (Input.GetKey(KeyCode.Space))
        {
            Change(AnimationType.animation_2);
        }

        if (Input.GetKey(KeyCode.M))
        {
            Change(AnimationType.animation_1);
        }
    }

    void Change(AnimationType type)
    {
        _spriteStudioRoot.AnimationPlay(-1, (int)type, 0);
    }


    void OnDisable()
    {
        transform.localScale = _scale;
    }
}
