using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public Script_SpriteStudio6_Root spriteStudioRoot;

	enum AnimationType
	{
		animation_1,
	}

	// Use this for initialization
	void Start()
	{
		spriteStudioRoot.AnimationPlay((int)AnimationType.animation_1, 0, 0, 1);
	}
}
