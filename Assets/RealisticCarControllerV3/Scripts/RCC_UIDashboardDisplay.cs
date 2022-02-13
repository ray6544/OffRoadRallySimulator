//----------------------------------------------
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
using UnityEngine.UI;

/// <summary>
/// Handles RCC Canvas dashboard elements.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Displayer")]
[RequireComponent (typeof(RCC_DashboardInputs))]
public class RCC_UIDashboardDisplay : MonoBehaviour {

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

	private RCC_DashboardInputs inputs;

	public DisplayType displayType;
	public enum DisplayType{Full, Customization, TopButtonsOnly, Off}

	public GameObject controllerButtons;
	
	public Text KMHLabel;


	public Dropdown mobileControllers;

	void Awake(){

		inputs = GetComponent<RCC_DashboardInputs>();

		if (!inputs) {

			enabled = false;
			return;

		}

	}
	
	void Start () {
		
		CheckController ();
		
	}

	void OnEnable(){

		RCC_SceneManager.OnControllerChanged += CheckController;

	}

	private void CheckController(){

		if (RCCSettings.selectedControllerType == RCC_Settings.ControllerType.Keyboard || RCCSettings.selectedControllerType == RCC_Settings.ControllerType.XBox360One || RCCSettings.selectedControllerType == RCC_Settings.ControllerType.PS4 || RCCSettings.selectedControllerType == RCC_Settings.ControllerType.LogitechSteeringWheel) {

			if(mobileControllers)
				mobileControllers.interactable = false;
			
		}

		if (RCCSettings.selectedControllerType == RCC_Settings.ControllerType.Mobile) {

			if(mobileControllers)
				mobileControllers.interactable = true;
			
		}

	}

	void Update(){

		switch (displayType) {

		case DisplayType.Full:


			if(controllerButtons && !controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(true);

			break;

		case DisplayType.Customization:

			if(controllerButtons && controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(false);


			break;

		case DisplayType.TopButtonsOnly:
			if(controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(false);

			break;

		case DisplayType.Off:
			if(controllerButtons &&controllerButtons.activeInHierarchy)
				controllerButtons.SetActive(false);

			break;

		}

	}
	
	void LateUpdate () {

		if (RCC_SceneManager.Instance.activePlayerVehicle) {
	
		
			if (KMHLabel) {
			
				if (RCCSettings.units == RCC_Settings.Units.KMH)
					KMHLabel.text = inputs.KMH.ToString ("0") + "\nKMH";
				else
					KMHLabel.text = (inputs.KMH * 0.62f).ToString ("0") + "\nMPH";
			
			}

		}

		

	}
}

