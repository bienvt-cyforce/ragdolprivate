using UnityEngine;
using System.Collections;

public class SlowMotionScript : MonoBehaviour
{
	Camera cam;
	public static SlowMotionScript Instance;
	bool slowed = false;
	bool lockslow = false;
	public float ortgSizeMin, ortgSizeMax, ortgSizeNormal;
	public float timeScale;
	bool zoomIn = false, zoomOUT = false;
	public float smooth;
	public bool camstart = true;
	public bool slowing = true;
	public float speedZoomIn, speedZoomOut;
	public bool lookedPlayer = false;
	public float timeSacleOnHit = 0.05f, realtimeCDOnHit = 0.2f;
	bool outslow = false;
	void Awake(){
		cam = Camera.main;
		cam.orthographicSize = 16;
	}
	void Start ()
	{
		Instance = this;
		slowed = false;
		ortgSizeMax += 2f;
		zoomIn = false;
		zoomOUT = false;
		blockZoom = false;
		slowing = false;
		//Application.targetFrameRate = 60;
		Profiler.maxNumberOfSamplesPerFrame = 1000;
		camstart = true;
		Time.timeScale = 0;
		delaySwitchZoomIn = false;
		delaySwitchZoomOut = false;
	}

	public void startCamZoom ()
	{
		StartCoroutine (delayCamZoomIn_Ready ());
		GUIScreenSpace.Instance.startCountReady ();
	}

	IEnumerator delayCamZoomIn_Ready ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (smooth));
		if (cam.orthographicSize > ortgSizeNormal) {
			cam.orthographicSize -= 0.02f;
			StartCoroutine (delayCamZoomIn_Ready ());
		} 
	}

	public void slowEnd_ (bool state)
	{
		slowEnd = state;
	}
	public bool stopcam = false;
	void Update ()
	{
//		if (!BadLogic.pause) {
//			/*if (!lockslow) {
//				if (Time.timeScale < timeScale) {
//					Time.timeScale = timeScale;
//					//Time.timeScale += 0.5f;
//				} else if (Time.timeScale >= timeScale) {
//					Time.timeScale = timeScale;
//					slowed = false;
//					lockslow = true;
//				}
//			}
//			if (zoomIn) {
//				if (cam.orthographicSize > ortgSizeMin) {
//					cam.orthographicSize -= 0.1f;
//				} else {
//					zoomIn = false;
//				}
//			}
//			if (zoomOUT) {
//				if (cam.orthographicSize < ortgSizeMax) {
//					cam.orthographicSize += 0.2f;
//				} else {
//					zoomOUT = false;
//				}
//			}
//		} else {
//			if (!camstart) {
//				if (cam.orthographicSize < ortgSizeMax) {
//					cam.orthographicSize += 0.1f;
//				} 
//			}
//		
//		}
//		if (blockZoom) {
//			if (!camstart) {
//				if (cam.orthographicSize < ortgSizeMax) {
//					cam.orthographicSize += 0.1f;
//				} else {
//					blockZoom = false;
//				}
//			}*/
//			if (!camstart) {
//				if (slowing) {
//					if (!slowOnHit) {
//						Time.timeScale = 0.9f;
//					}
//					if (Time.timeScale >= 0.89f) {
//						if (lookedPlayer) {
//							if (cam.orthographicSize > ortgSizeMin) {
//								cam.orthographicSize -= speedZoomIn * Time.deltaTime;
//							}
//						}
//					}
//				} else {
//					if (Time.timeScale == 0.9f || Time.timeScale == timeSacleOnHit || Time.timeScale == 0.1f || Time.timeScale == 0.7f) {
//						if (!slowOnHit) {
//							StartCoroutine (delayEndSlow ());
//						}
//					} else {
//						if (Time.timeScale >= 0.89f) {
//							if (cam.orthographicSize < ortgSizeNormal) {
//								cam.orthographicSize += speedZoomOut * Time.deltaTime;
//							}
//						}
//					}
//				}
//				if (!lookedPlayer) {
//					if (Time.timeScale >= 0.89f) {
//						if (ortgSizeNormal < ortgSizeMax) {
//							ortgSizeNormal += speedZoomOut * Time.deltaTime;
//						}
//					}
//				}
//				if (slowDeath) {
//					if (ortgSizeNormal < ortgSizeMax) {
//						ortgSizeNormal += speedZoomOut * Time.deltaTime;
//					}
//				}
//				Time.fixedDeltaTime = 0.02F * Time.timeScale;
//			}
//		}
		if (!BadLogic.pause && !stopcam) {
			if (!camstart) {
				if (slowOnHit) {
					Time.timeScale = timeSacleOnHit;
					if (lookedPlayer) {
						outslow = false;
						if (cam.orthographicSize > ortgSizeMin) {
							cam.orthographicSize -= 0.1f;
						}
					} else {
						outslow = true;
					}
				} else {
					Time.timeScale = timeScale;
				}
				if (!lookedPlayer) {
					DelaySwitchZoomOut ();
				} else {
					DelaySwitchZoomIN ();
				}
				Time.fixedDeltaTime = 0.02F * Time.timeScale;
				if (zoomOUT) {
					if (cam.orthographicSize < ortgSizeMax) {
						if (Time.timeScale == timeScale) {
							cam.orthographicSize += speedZoomOut * Time.deltaTime;
						}
						//cam.orthographicSize = ortgSizeMax;
					}
				}
				if (zoomIn) {
					if (!slowOnHit) {
						//print ("ccc nhe");
						if (cam.orthographicSize > ortgSizeNormal) {
							if (Time.timeScale == timeScale) {
								cam.orthographicSize -= 0.1f;
							} 
						}
					} else {
						if (outslow) {
							if (Time.timeScale == timeScale) {
								cam.orthographicSize -= 0.1f;
							} 
						}
					}
				}
			}
		}
	}

	void LateUpdate ()
	{
	}

	bool delaySwitchZoomIn = false;
	bool delaySwitchZoomOut = false;

	public void DelaySwitchZoomIN ()
	{
		if (!delaySwitchZoomIn) {
			zoomOUT = false;
			zoomIn = false;
			delaySwitchZoomIn = true;
			delaySwitchZoomOut = false;
			StartCoroutine (delay_SwitchSizeCam ("IN"));
		}
	}

	public void DelaySwitchZoomOut ()
	{
		if (!delaySwitchZoomOut) {
			zoomOUT = false;
			zoomIn = false;
			delaySwitchZoomOut = true;
			delaySwitchZoomIn = false;
			StartCoroutine (delay_SwitchSizeCam ("OUT"));
		}
	}

	IEnumerator delay_SwitchSizeCam (string zoom)
	{
		yield return new WaitForSecondsRealtime (0.25f);
		switch (zoom) {
		case"IN":
			zoomIn = true;
			zoomOUT = false;
			break;
		case "OUT":
			zoomOUT = true;
			zoomIn = false;
			break;
		}
	}

	IEnumerator delayEndSlow ()
	{
		Time.timeScale = 0.89f;
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.8f));
		if (Time.timeScale == 0.89f) {
			Time.timeScale = timeScale;
		} else {
			delayEndSlow ();
		}
	}

	int countHit = 0, countHitLate;
	bool slowEnd = true;

	public void _slowMotion ()
	{
		/*if (!blockZoom) {
			countHit += 1;
			if (!slowed) {
				countHitLate = countHit;
				Time.timeScale = timeScale / 2f;
				zoomIn = true;
				slowed = true;
				lockslow = true;
				StartCoroutine (delayUnlock ());
			}
		}*/
	}

	IEnumerator delayUnlock ()
	{
		yield return new WaitForSeconds (0.1f);
		if (countHitLate == countHit) {
			lockslow = false;
			if (!zoomIn) {
				if (!blockZoom) {
					zoomIn = true;
				}		
			}
			zoomOUT = true;
			countHitLate = 0;
			countHit = 0;
		} else {
			countHitLate = countHit;
			StartCoroutine (delayUnlock ());
		}
	}

	public void _zoomIn ()
	{
		zoomIn = true;
	}

	bool blockZoom = false;

	public void _zoomOUT ()
	{
		//zoomOUT = true;
		//blockZoom = true;
		//zoomIn = false;
	}

	bool slowOnHit = false;
	float timeScaleLate;


	int countSlowOnHit = 0, countSlowOnHitLate = 0;

	public void _SlowOnHit ()
	{
		countSlowOnHit += 1;
		if (!slowOnHit) {
			countSlowOnHitLate = countSlowOnHit;
			slowOnHit = true;
			StartCoroutine (delay_SlowOnHit (realtimeCDOnHit));
			//StartCoroutine (delay_SlowOnHitFix ());
		}
	}

	public void SetCamNormal ()
	{
		cam.orthographicSize = 14f;
	}

	IEnumerator delay_SlowOnHitFix ()
	{
		yield return new WaitForEndOfFrame ();
		slowOnHit = false;
	}

	IEnumerator delay_SlowOnHit (float time)
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (time));
		if (countSlowOnHitLate == countSlowOnHit) {
			if (!slowDeath) {
				slowOnHit = false;
			} else {
				slowDeath = false;
			}
			countSlowOnHit = 0;
			countSlowOnHitLate = 0;
		} else {
			countSlowOnHitLate = countSlowOnHit;
			StartCoroutine (delay_SlowOnHit (realtimeCDOnHit));
		}
	}

	bool slowDeath = false;

	public void _slowOnDeath ()
	{
		slowOnHit = true;
		slowDeath = true;
		Time.timeScale = 0.01f;
		StartCoroutine (delay_slowOnDeath ());
		StartCoroutine (delay_SlowOnHit (0.8f));
	}

	IEnumerator delay_slowOnDeath ()
	{
		yield return StartCoroutine (CoroutineUtil.WaitForRealSeconds (0.8f));
		slowDeath = false;
	}
}
