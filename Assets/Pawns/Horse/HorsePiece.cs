using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class HorsePiece : ChessPiece
{
	public GameObject missle;
	public HorsePiece(){
		this.Type = "horse";
		this.LockRange = 2;
	}
    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();

		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX+1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX-1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY+1));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY-1));

		if(isInBounds(startX+1, startY)){
			if(pieces[startX+1, startY]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+2, startY-1));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+2, startY+1));
			}
		}
		if(isInBounds(startX-1, startY)){
			if(pieces[startX-1, startY]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-2, startY-1));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-2, startY+1));
			}
		}
		if(isInBounds(startX, startY-1)){
			if(pieces[startX, startY-1]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY-2));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY-2));
			}
		}
		if(isInBounds(startX, startY+1)){
			if(pieces[startX, startY+1]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY+2));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY+2));
			}
		}


		return possibleMoves;
    }
	
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
		ArrayList possibleAttacks = new ArrayList();
		
		if(isInBounds(startX+1, startY)){
			if(pieces[startX+1, startY]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+2, startY-1));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+2, startY+1));
			}
		}
		if(isInBounds(startX-1, startY)){
			if(pieces[startX-1, startY]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-2, startY-1));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-2, startY+1));
			}
		}
		if(isInBounds(startX, startY-1)){
			if(pieces[startX, startY-1]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+1, startY-2));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-1, startY-2));
			}
		}
		if(isInBounds(startX, startY+1)){
			if(pieces[startX, startY+1]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+1, startY+2));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-1, startY+2));
			}
		}

		RemoveOutRangeAttack(possibleAttacks, frontLines);
		return possibleAttacks;
	}

    public override void CheckSpecial(Board board, ChessPiece[,] pieces)
    {
        // base.CheckSpecial();
		int selfX = (int)boardPosition.x;
		int selfY = (int)boardPosition.y;
		ChessPiece front = pieces[selfX+(red? 1: -1), selfY];
		String d = $"";
		// d += $"Checking {(selfX+(red? -1: 1), selfY)}";	///////////	DEBUG
		// Debug.Log(d);	///////////	DEBUG
		if(front != null){
			if(front.Type == "cannon"&&front.GetRed()==GetRed()){
				d = "Horse Cannon Special Attack!: ";
				// MoveAttack = true;
				// d += $"Checking {(selfX+1, selfY+(red? -2: 2))} and {(selfX-1, selfY+(red? -2: 2))}";	///////////	DEBUG

				// ChessPiece target = pieces[selfX+(red? -2: 2), selfY+1];
				int targetX = selfX+(red? -2: 2);
				int targetY = selfY+1;
				ChessPiece target = pieces[targetX, targetY];
				if(pieces[targetX, targetY] != null){
					if(pieces[targetX, targetY].GetRed() != red){
						d+= $"Remove {(targetX, targetY)}";	///////////	DEBUG
						pieces[targetX, targetY] = null;
						board.RemovePieceAfterAnimation(target);
					}
				}

				targetX = selfX+(red? -2: 2);
				targetY = selfY-1;
				target = pieces[targetX, targetY];
				if(pieces[targetX, targetY] != null){
					if(pieces[targetX, targetY].GetRed() != red){
						d+= $"Remove {(targetX, targetY)}";	///////////	DEBUG
						pieces[targetX, targetY] = null;
						board.RemovePieceAfterAnimation(target);
					}
				}
				Debug.Log(d);	///////////	DEBUG
			}
		}

    }
	
	public override void AttackAnimation(Board board, Vector3 startPosition, Vector2 start, ChessPiece targetPiece, int endX, int endY){
		int targetX = (int) targetPiece.GetBoardPosition().x;
		int targetY = (int) targetPiece.GetBoardPosition().y;

		Vector3 targetPosition = startPosition + new Vector3((targetY - start.y) * 20f, (targetX - start.x) * 20f, 0.0f);
		StartCoroutine(LaunchMissle(board, startPosition, targetPosition, targetPiece, endX, endY));
	}

	IEnumerator LaunchMissle(Board board, Vector3 startPosition, Vector3 targetPosition, ChessPiece targetPiece, int endX, int endY){
		// GameObject missles = Instantiate(missle);
		GameObject missles = new GameObject();
		missles.transform.parent = this.transform;
		missles.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		missles.transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
		
		GameObject missleInstance1 = Instantiate(missle);
		missleInstance1.transform.parent = missles.transform;
		// missleInstance1.transform.parent = this.transform;

		GameObject missleInstance2 = Instantiate(missle);
		missleInstance2.transform.parent = missles.transform;
		// missleInstance2.transform.parent = this.transform;

		GameObject missleInstance3 = Instantiate(missle);
		missleInstance3.transform.parent = missles.transform;
		// missleInstance3.transform.parent = this.transform;

		// missles.transform.LookAt(targetPosition);
		// this.transform.LookAt(targetPosition);
		// Vector3 direction = targetPosition - transform.position;
		// UnityEngine.Quaternion lookRotation = UnityEngine.Quaternion.LookRotation(-direction, Vector3.forward);
		// transform.rotation = UnityEngine.Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z);
       
		// missles.transform.Rotate(90, 0, 0);
		// this.transform.Rotate(0, 0, 0);
		float time = 0;
		float duration = 1f;
		while(time < duration){
			missles.transform.position = Vector3.Lerp(startPosition, targetPosition, time/duration);
			// this.transform.position = Vector3.Lerp(startPosition, targetPosition, time/duration);
			int count = missles.transform.childCount;
			// int count = this.transform.childCount;
			missles.transform.LookAt(targetPosition);
			for(int i = 0; i < count; i++){
				missles.transform.GetChild(i).transform.localPosition = new Vector3( 
				10 * Mathf.Sin(Mathf.PI * time/duration + i*Mathf.PI/count),10 * Mathf.Cos(Mathf.PI * time/duration + i*Mathf.PI/count), 0);
				// this.transform.GetChild(i).transform.localPosition = new Vector3(2 * Mathf.Sin(2 * Mathf.PI * time/duration + i*Mathf.PI/count),
				// 0.0f,2 * Mathf.Cos(2 * Mathf.PI * time/duration + i*Mathf.PI/count));
			}
			time += Time.deltaTime;
			yield return null;
		}
		Destroy(missles);
		board.RemovePieceAfterAnimation(targetPiece);
		if(MoveAttack){
			board.MovePieceAfterAnimation(this, endX, endY);
		}
	}

    public override string GetInfo()
    {
        String info = "Missle ship: \n";
		info += "It can move 2 vertical steps and 1 horizontal step in any direction.\n";
		info += "Attack is the same as move.\n";
		info += "Special Attack: attack when cannon is behind.\n";

		return info;
    }
}
