using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

	private float initialX;
	private float currentX;
	private float Xdirection;

	private float initialY;
	private float currentY;
	private float Ydirection;

	void OnMouseDown() {
		initialX = Input.mousePosition.x;
		initialY = Input.mousePosition.y;
	}

	void OnMouseUp() {
		currentX = Input.mousePosition.x;
		Xdirection = initialX - currentX;

		currentY = Input.mousePosition.y;
		Ydirection = initialY - currentY;

		Debug.Log(ReturnDirection ());
	}

	string ReturnDirection() {
		string result;

		if (Ydirection < -100) {
			if (Xdirection > 180){
				result = "Attack-Left";
				return result;
			} else if (Xdirection < -180) {
				result = "Attack-Right";
				return result;
			} else {
				result = "Attack-Center";
				return result;
			}
		} else if (Ydirection > 180) {
			result = "Block";
			return result;
		} else if (Mathf.Abs (Xdirection) > 100) {
			if (Xdirection > 100) {
				result = "Move-Left";
				return result;
			} else if (Xdirection < -100) {
				result = "Move-Right";
				return result;
			}
		}

		result = "error";
		return result;
	}
}
