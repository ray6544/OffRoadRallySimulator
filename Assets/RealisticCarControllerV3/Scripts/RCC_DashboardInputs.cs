﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2020 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Receiving inputs from active vehicle on your scene, and feeds dashboard needles, texts, images.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Inputs")]
public class RCC_DashboardInputs : MonoBehaviour {

	// Getting an Instance of Main Shared RCC Settings.
	#region RCC Settings Instance

	private RCC_Settings RCCSettingsInstance;
	private RCC_Settings RCCSettings {
		get {
			if (RCCSettingsInstance == null) {
				RCCSettingsInstance = RCC_Settings.Instance;
				return RCCSettingsInstance;
			}
			return RCCSettingsInstance;
		}
	}

	#endregion


	public GameObject KMHNeedle;
	

	
	private float KMHNeedleRotation = 0f;
	private float BoostNeedleRotation = 0f;
	private float NoSNeedleRotation = 0f;
	private float heatNeedleRotation = 0f;
	private float fuelNeedleRotation = 0f;


	internal float KMH;
	internal int direction = 1;
	internal float Gear;
	internal bool changingGear = false;
	internal bool NGear = false;

	internal bool ABS = false;
	internal bool ESP = false;
	internal bool Park = false;
	internal bool Headlights = false;

	internal RCC_CarControllerV3.IndicatorsOn indicators;

	void Update(){

		GetValues();

	}

	void GetValues(){

		if(!RCC_SceneManager.Instance.activePlayerVehicle)
			return;

		if(!RCC_SceneManager.Instance.activePlayerVehicle.canControl || RCC_SceneManager.Instance.activePlayerVehicle.externalController)
			return;

		
		

		
		

		
		
	
		KMH = RCC_SceneManager.Instance.activePlayerVehicle.speed;
		direction = RCC_SceneManager.Instance.activePlayerVehicle.direction;
		Gear = RCC_SceneManager.Instance.activePlayerVehicle.currentGear;
		changingGear = RCC_SceneManager.Instance.activePlayerVehicle.changingGear;
		NGear = RCC_SceneManager.Instance.activePlayerVehicle.NGear;
		
		ABS = RCC_SceneManager.Instance.activePlayerVehicle.ABSAct;
		ESP = RCC_SceneManager.Instance.activePlayerVehicle.ESPAct;
		Park = RCC_SceneManager.Instance.activePlayerVehicle.handbrakeInput > .1f ? true : false;
		Headlights = RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn || RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn;
		indicators = RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn;

		

		if(KMHNeedle){
			
			if(RCCSettings.units == RCC_Settings.Units.KMH)
				KMHNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.speed);
			else
				KMHNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.speed * 0.62f);
			
			KMHNeedle.transform.eulerAngles = new Vector3(KMHNeedle.transform.eulerAngles.x ,KMHNeedle.transform.eulerAngles.y, -KMHNeedleRotation);

		}

		

		
			
	}

}



