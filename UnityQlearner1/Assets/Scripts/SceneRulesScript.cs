using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRulesScript : MonoBehaviour
{
	public bool tfrOn = false;
	public int tfr = 50;

	void Awake()
	{
		// Set Frame Rate
		if(tfrOn)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = tfr;
		}
	}
}
