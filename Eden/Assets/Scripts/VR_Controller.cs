using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Controller : MonoBehaviour {


	public SteamVR_Controller.Device Controller
	{
		get
		{
			return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
		}
	}
}
