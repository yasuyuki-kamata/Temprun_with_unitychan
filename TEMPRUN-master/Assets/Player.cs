using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 6;

	// Update is called once per frame
	void Update () {
		rigidbody.MovePosition (transform.position + transform.forward * Time.deltaTime * speed);

		if( Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.UpArrow)){
			GetComponent<Animator>().SetTrigger("JUMP");
		}

		if( Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.DownArrow)){
			GetComponent<Animator>().SetTrigger("SLIDE");
		}
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.CompareTag ("goal")) {
			GetComponent<Animator>().SetTrigger("WIN");
			speed = 0;
		}
		var stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo (0);
		bool isJump = stateInfo.IsName("Base Layer.JUMP00");
		bool isSlide = stateInfo.IsName("Base Layer.SLIDE00");
		bool isRun = stateInfo.IsName("Base Layer.RUN00_F");

		bool isHigh = collider.CompareTag("High");
		bool isLow = collider.CompareTag("Low");

		if( (isRun == true) ||
		    (isJump == true && isHigh == true) ||
		    (isSlide == true && isLow == true))
		{
			GetComponent<Animator>().SetBool ("DAMAGED", true);
			speed = 0;
		}
	}

	Rect rect = new Rect (0f, (Screen.height / 2) - 100f, Screen.width, 200f);
	void OnGUI ()
	{
		var currentState = GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
		if (currentState.IsName ("Base Layer.LOSE") || currentState.IsName("Base Layer.WIN00")) {
			if (GUI.Button(rect, "RESTART")) {
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}
