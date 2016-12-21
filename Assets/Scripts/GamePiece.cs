using UnityEngine;
using System.Collections;

public class GamePiece : MonoBehaviour {

	public int xIndex;
	public int yIndex;

	bool m_isMoving = false;

	public enum InterpType{
		Linear,
		EaseOut,
		EaseIn,
		SmoothStep,
		SmootherStep
	};

	public InterpType interpolation = InterpType.SmootherStep;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
//		if(Input.GetKey(KeyCode.RightArrow)){
//			Move((int)transform.position.x +2, (int)transform.position.y, 0.5f);
//		}
//
//		if(Input.GetKey(KeyCode.LeftArrow)){
//			Move((int)transform.position.x -2, (int)transform.position.y, 0.5f);
//		}	
	}

	public void SetCoord(int x, int y){
		xIndex = x;
		yIndex = y;
	}

	public void Move(int destX, int destY, float timeToMove){
		if(!m_isMoving){
			StartCoroutine( MoveRoutine( new Vector3(destX,destY,0), timeToMove) ) ;	
		}
	}

	IEnumerator MoveRoutine(Vector3 destination, float timeToMove){
		Vector3 startPosition = transform.position;

		bool reachedDestination = false;

		float elapsedTime = 0f;

		m_isMoving = true;

		while (!reachedDestination){

			//if we are close enought ot destination
			if(Vector3.Distance(transform.position, destination) < 0.01f){
				reachedDestination = true;
				transform.position = destination;
				SetCoord( (int)destination.x, (int)destination.y );
				break;
			}

			elapsedTime += Time.deltaTime;

			float t = Mathf.Clamp( elapsedTime / timeToMove, 0f, 1f);

			switch(interpolation){
				case InterpType.Linear:
					break;
				case InterpType.EaseOut:
					t = Mathf.Sin(t* Mathf.PI * 0.5f);
					break;	
				case InterpType.EaseIn:
					t = 1 - Mathf.Cos(t*Mathf.PI * 0.5f);
					break;	
				case InterpType.SmoothStep:
					t = t*t*(3-2*t);
					break;	
				case InterpType.SmootherStep:
					t = t*t*t*(t*(t*6-15)+10);
					break;	
			}
				
			transform.position = Vector3.Lerp(startPosition, destination, t);

			//wait until the next frame
			yield return null;
		}

		m_isMoving = false;
	}
}
