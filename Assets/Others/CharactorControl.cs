using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    // �Đ��A�j���[�V������Resources�t�H���_���̃T�u�p�X
    [SerializeField]
    public Object[] AnimationList;

    // �Đ��A�j���[�V�����w��p 
    private enum AnimationPattern : int
    {
        Wait = 33,      // �ҋ@
        Attack = 1,     // �U��
        Run = 24,       // ����
        Count
    }

    // �L�����N�^�[�Ǘ��p 
    private GameObject m_goCharacter = null;
    private GameObject m_goCharPos = null;
    private Vector3 m_vecCharacterPos;      // �L�����N�^�[�ʒu
    private Vector3 m_vecCharacterScale;    // �L�����N�^�[�X�P�[��

    // �����X�e�b�v�p 
    private enum Step : int
    {
        Init = 0,   // ������ 
        Title,      // �^�C�g�� 
        Wait,       // �ҋ@ 
        Move,       // �ړ� 
        Attack,     // �U��
        End
    }

    // �����X�e�b�v�Ǘ��p 
    private Step m_Step = Step.Init;

    // �ėp
    // ���낢��g���܂킷�p�ϐ�
    private int m_Count = 0;
    private bool m_SW = true;

    // Use this for initialization
    void Start()
    {

        // �L�����N�^�[�p�����[�^�֘A��ݒ� 

        // ���W�ݒ� 
        m_vecCharacterPos.x = 0.0f;
        m_vecCharacterPos.y = -240.0f;
        m_vecCharacterPos.z = 0.0f;

        // �X�P�[���ݒ� 
        m_vecCharacterScale.x = 0.5f;
        m_vecCharacterScale.y = 0.5f;
        m_vecCharacterScale.z = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_Step)
        {
            // ������
            case Step.Init:
                m_Count = 0;
                m_SW = true;
                m_Step = Step.Title;
                break;
            // �^�C�g��
            case Step.Title:
                if (++m_Count > 15)
                {
                    m_SW = !m_SW;
                    m_Count = 0;
                }
                if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    AnimationStart();   // �A�j���[�V�����J�n����(�ݒ�)
                    m_Step = Step.Wait;
                }
                break;
            // �ҋ@
            case Step.Wait:
                if (Input.GetKeyDown(KeyCode.Z) == true)        // �U�� 
                {
                    // �U���ɕύX 
                    AnimationChange(AnimationPattern.Attack);
                    m_Step = Step.Attack;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)   // ���ړ� 
                {
                    if (m_vecCharacterScale.x < 0)
                        m_vecCharacterScale.x *= -1;    // �������ɂ��܂�
                    m_goCharPos.transform.localScale = m_vecCharacterScale; // �����ݒ� 
                    // ����ɕύX 
                    AnimationChange(AnimationPattern.Run);
                    m_Step = Step.Move;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) == true)  // �E�ړ� 
                {
                    if (m_vecCharacterScale.x > 0)
                        m_vecCharacterScale.x *= -1;    // �E�����ɂ��܂�
                    m_goCharPos.transform.localScale = m_vecCharacterScale; // �����ݒ� 
                    // ����ɕύX 
                    AnimationChange(AnimationPattern.Run);
                    m_Step = Step.Move;
                }
                break;
            // �ړ� 
            case Step.Move:
                if (Input.GetKey(KeyCode.LeftArrow) == true)   // ���ړ� 
                {
                    if (m_vecCharacterPos.x > -560.0f)
                        m_vecCharacterPos.x -= 10.0f;
                }
                else if (Input.GetKey(KeyCode.RightArrow) == true)  // �E�ړ� 
                {
                    if (m_vecCharacterPos.x < 560.0f)
                        m_vecCharacterPos.x += 10.0f;
                }
                else
                {
                    // �ҋ@�ɕύX 
                    AnimationChange(AnimationPattern.Wait);
                    m_Step = Step.Wait;
                }
                m_goCharPos.transform.localPosition = m_vecCharacterPos;    // ���W���f 
                break;
            // �U���� 
            case Step.Attack:
                if (IsAnimationPlay() == false)
                {
                    // �ҋ@�ɕύX 
                    AnimationChange(AnimationPattern.Wait);
                    m_Step = Step.Wait;
                }
                break;
            default:
                break;
        }
    }

    private void OnGUI()
    {
        // GUI�ύX
        GUIStyle guiStyle = new GUIStyle();
        GUIStyleState styleState = new GUIStyleState();

        switch (m_Step)
        {
            // �^�C�g��
            case Step.Title:
                if (m_SW == true)
                {
                    styleState.textColor = Color.black; // �����F �� 
                    guiStyle.normal = styleState;       // �X�^�C���̐ݒ�B
                    GUI.Label(new Rect(420, 180, 100, 50), "PRESS SPACE", guiStyle);
                }
                break;
            default:
                break;
        }
    }

    // �A�j���[�V�����J�n 
    private void AnimationStart()
    {
        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime �𑀍삷�邽�߂̃N���X
        int listLength = AnimationList.Length;

        // ���łɃA�j���[�V���������� or ���\�[�X�ݒ薳���ꍇ��return
        if (m_goCharacter != null || listLength < 1)
            return;

        // �Đ����郊�\�[�X�������X�g����擾���čĐ�����
        Object resourceObject = AnimationList[0];
        if (resourceObject != null)
        {
            // �A�j���[�V���������̉�
            m_goCharacter = Instantiate(resourceObject, Vector3.zero, Quaternion.identity) as GameObject;
            if (m_goCharacter != null)
            {
                scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(m_goCharacter);
                if (scriptRoot != null)
                {
                    // ���W�ݒ肷�邽�߂�GameObject�쐬
                    m_goCharPos = new GameObject();
                    if (m_goCharPos == null)
                    {
                        // �쐬�ł��Ȃ��P�[�X�Ή� 
                        Destroy(m_goCharacter);
                        m_goCharacter = null;
                    }
                    else
                    {
                        // Object���ύX 
                        m_goCharPos.name = "Comipo";

                        // ���W�ݒ� 
                        m_goCharacter.transform.parent = m_goCharPos.transform;

                        // �����̎q�Ɉړ����č��W��ݒ�
                        m_goCharPos.transform.parent = this.transform;
                        m_goCharPos.transform.localPosition = m_vecCharacterPos;
                        m_goCharPos.transform.localRotation = Quaternion.identity;
                        m_goCharPos.transform.localScale = m_vecCharacterScale;

                        //�A�j���[�V�����Đ�
                        AnimationChange(AnimationPattern.Wait);
                    }
                }
            }
        }
    }

    // �A�j���[�V���� �Đ�/�ύX 
    private void AnimationChange(AnimationPattern pattern)
    {
        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime �𑀍삷�邽�߂̃N���X
        int iTimesPlay = 0;

        if (m_goCharacter == null)
            return;

        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(m_goCharacter);
        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.Wait:
                    iTimesPlay = 0;    // ���[�v�Đ� 
                    break;
                case AnimationPattern.Attack:
                    iTimesPlay = 1;    // 1�񂾂��Đ� 
                    break;
                case AnimationPattern.Run:
                    iTimesPlay = 0;    // ���[�v�Đ� 
                    break;
                default:
                    break;
            }
            scriptRoot.AnimationPlay(-1, (int)pattern, iTimesPlay);
        }
    }

    // �A�j���[�V�������Đ�������~��(�G���[��)���擾���܂�
    private bool IsAnimationPlay()
    {
        bool ret = false;

        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime �𑀍삷�邽�߂̃N���X

        if (m_goCharacter != null)
        {
            scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(m_goCharacter);
            if (scriptRoot != null)
            {
                // �Đ��񐔂��擾���āA�v���C�I�����𔻒f���܂�
                int Remain = scriptRoot.PlayTimesGetRemain(0);
                if (Remain >= 0)
                    ret = true;
            }
        }

        return ret;
    }

}
