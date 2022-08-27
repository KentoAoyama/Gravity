using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
	public Script_SpriteStudio6_Root spriteStudioRoot;
	[SerializeField] Vector3 _humanScale;

	enum AnimationType
	{
		animation_1,
	}

	void Start()
	{
        _humanScale.x = 0.0015f;
        _humanScale.y = 0.0015f;
		_humanScale.z = 1f;
		spriteStudioRoot.AnimationPlay((int)AnimationType.animation_1, 0, 0, 1);		
	}

    void FixedUpdate()
    {
        transform.localScale = _humanScale;
    }
}
