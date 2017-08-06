using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlingBling : MonoBehaviour {

	public Color color;
	SpriteRenderer spriteRenderer;

	public float changeColorSpeed;

	public int colorMode = 0;

	bool glow;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		color = spriteRenderer.color;

	}

	void Update() {
		if (glow) {
			switch(colorMode) {
				case 0:
					color.r += Time.deltaTime;
					if (color.r > 1) {
						color.r = 1;
						goto default;
					}
				break;
				case 1:
					color.g += Time.deltaTime;
					if (color.g > 1) {
						color.g = 1;
						goto default;
					}
				break;
				case 2:
					color.b += Time.deltaTime;
					if (color.b > 1) {
						color.b = 1;
						goto default;
					}
				break;
				default:
					glow = false;
				break;
			}
		} else {
			switch(colorMode) {
				case 0:
					color.r -= Time.deltaTime;
					if (color.r < 0) {
						color.r = 0;
						goto default;
					}
				break;
				case 1:
					color.g -= Time.deltaTime;
					if (color.g < 0) {
						color.g = 0;
						goto default;
					}
				break;
				case 2:
					color.b -= Time.deltaTime;
					if (color.b < 0) {
						color.b = 0;
						goto default;
					}
				break;
				default:
					glow = true;
				break;
			}
		}

		spriteRenderer.color = color;
	}
}
