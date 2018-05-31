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

		if (Mathf.Abs (Xdirection) < 100 &&
		    Mathf.Abs (Ydirection) < 100) {
			result = "none";
			return result;
		}

		if (Ydirection < 0) {
			if (Xdirection > 100) {
				result = "Attack-Left";
				return result;
			} else if (Xdirection < -100) {
				result = "Attack-Right";
				return result;
			} else {
				result = "Attack-Center";
				return result;
			}
		} else if (Ydirection > 0) {
			if (Xdirection > 100) {
				result = "Dodge-Left";
				return result;
			} else if (Xdirection < -100) {
				result = "Dodge-Right";
				return result;
			} else {
				result = "Dodge-Center";
				return result;
			}
		}

		result = "error";
		return result;
	}
}
