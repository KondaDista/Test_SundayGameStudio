using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour 
{
	private WheelCollider[] wheels;
	[SerializeField] private Light[] backLights;

	public float maxAngle = 30;
	public float maxTorque = 300;
	public GameObject wheelShape;

	bool pressedHorizontal = false;

	public float anglePresetHorizontal;
	public float speedPresetHorizontal;
	public int wawePresetHorizontal;

	[SerializeField] private float angle;
	[SerializeField] private float torque;

	// here we find all the WheelColliders down in the hierarchy
	public void Start()
	{
		wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			// create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = GameObject.Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
				if (i is 1 or 3)
				{
					ws.transform.GetChild(0).rotation = new Quaternion(0,180,0,0);
				}
			}
		}
	}

	public void Update()
	{
		//angle = maxAngle * Input.GetAxis("Horizontal");
		//torque = maxTorque * Input.GetAxis("Vertical");

		foreach (WheelCollider wheel in wheels)
		{
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = torque;

			if (wheelShape)
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose(out p, out q);

				Transform shapeTransform = wheel.transform.GetChild(0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
				shapeTransform.localScale = new Vector3(1, 1, 1);
			}

		}

		if (pressedHorizontal && Mathf.Abs(angle) <= maxAngle)
		{
			anglePresetHorizontal += Time.deltaTime * speedPresetHorizontal;
			angle = maxAngle * (anglePresetHorizontal * wawePresetHorizontal);
		}
		else if (!pressedHorizontal && anglePresetHorizontal > 0)
		{
			anglePresetHorizontal -= Time.deltaTime * speedPresetHorizontal;
			angle = maxAngle * (anglePresetHorizontal * wawePresetHorizontal);
		}
	}

	public void onDownHorizontal(int input)
	{
		pressedHorizontal = true;
		wawePresetHorizontal = input;
	}

	public void onUpHorizontal(int input)
	{
		pressedHorizontal = false;
	}

	public void MoveInput(float input)
	{
		torque = maxTorque * input;
		foreach (var light in backLights)
		{
			switch (input)
			{
				case 1:
					light.intensity = 1;
					break;
				case -1:
					light.intensity = 3;
					break;
			}
		}
	}

	public void onUpMoveInput()
	{
		foreach (var light in backLights)
		{
			light.intensity = 1;
		}
	}
}
