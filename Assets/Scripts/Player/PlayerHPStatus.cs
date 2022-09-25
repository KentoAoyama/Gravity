using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


/// <summary>MVRPパターンのM(Model)にあたるクラス。GUIに表示するデータの実体をもつ部分のこと</summary>
public class PlayerHPStatus : MonoBehaviour, IAddDamage
{
    const int Hp = 100; //HPの値を定義

    int _maxHp = Hp;

    /// <summary>最大HP</summary>
    public int MaxHp => _maxHp;

    /// <summary>ReactivePropertyとして参照可能にする</summary>    //Presenterからのアクセスを可能にするため
    public IReadOnlyReactiveProperty<int> PlayerHP => _playerHP;
    //公開することで、Modelの内部状態が変化したときに
    readonly IntReactiveProperty _playerHP = new(Hp);              //それがObservableとして外部に通知できる


    [SerializeField, Tooltip("GodModeの時間")] float _godTime = 1.5f;
    [Tooltip("GodModeのレイヤー")] const int GOD_MODE_LAYER = 13;
    [Tooltip("Playerのレイヤー")] const int PLAYER_LAYER = 8;
    [Tooltip("ダメージを受けるアニメーションの時間を表す定数")] const float DAMAGE_TIME = 0.5f;

    bool _isDamage;
    /// <summary>ダメージを受けたことを表すプロパティ</summary>
    public bool IsDamage => _isDamage;

    Animator _animator;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _sound;
    [SerializeField] AudioSource _hitAudioSource;
    [SerializeField] AudioClip _hitsound;


    void Start()
    {
        _animator = GetComponent<Animator>();

        _audioSource.clip = _sound;
        _hitAudioSource.clip = _hitsound;
    }


    void Update()
    {
        _animator.SetBool("IsDamage", _isDamage);
    }


    public void AddDamage(int damage)
    {
        _hitAudioSource.Play();
        _playerHP.Value -= damage;
        StartCoroutine(DamageCoroutine());
    }


    /// <summary>ダメージを受けた際の処理をコルーチンで実行</summary>
    IEnumerator DamageCoroutine()
    {
        _isDamage = true;
        gameObject.layer = GOD_MODE_LAYER;
        yield return new WaitForSeconds(DAMAGE_TIME);

        _isDamage = false;
        yield return new WaitForSeconds(_godTime);

        gameObject.layer = PLAYER_LAYER;
    }


    /// <summary>プレイヤーのHPが回復した際に呼び出すメソッド</summary>
    public void Heal(int value)
    {
        _audioSource.Play();

        _playerHP.Value += value;

        if (_playerHP.Value > 100)
        {
            _playerHP.Value = 100;
        }
    }


    void OnDestroy()
    {
        //いらなくなったら適宜破棄する
        _playerHP.Dispose();
    }
}
