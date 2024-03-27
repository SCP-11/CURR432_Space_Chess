using System.Collections;
using UnityEngine;

public class ChessPiece : MonoBehaviour {

	private string type;
	private bool red;
	private bool atRiver;
	private Vector2 boardPosition;

	private int lockRange;
	private bool moveAttack;
	public ChessPiece(string t, bool r, int x, int y){
		type = t;
		red = r;
		boardPosition = new Vector2(x, y);
	}

	public ChessPiece(){
		type = "";
		red = true;
		boardPosition = new Vector2(-1, -1);
		lockRange = 1;
		moveAttack = true;
	}

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }
	public int LockRange
    {
        get
        {
            return lockRange;
        }

        set
        {
            lockRange = value;
        }
    }
	public bool MoveAttack
    {
        get
        {
            return moveAttack;
        }

        set
        {
            moveAttack = value;
        }
    }

    public void SetRed(bool r){
		red = r;
	}

	public bool GetRed(){
		return red;
	}

	public void SetBoardPosition(int x, int y){
		boardPosition = new Vector2(x, y);
	}

	public Vector2 GetBoardPosition(){
		return boardPosition;
	}

	public virtual ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		return new ArrayList();
	}
	public virtual ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
		return new ArrayList();
	}
	public void RemoveOutRangeAttack(ArrayList possibleAttacks, int[] frontLines){
		for (int i = possibleAttacks.Count-1; i >=0; i--){
			bool canLock;
			if(GetRed()){
				canLock = (int)((Vector2) possibleAttacks[i]).x >= frontLines[(int)((Vector2) possibleAttacks[i]).y];
			}else{
				canLock = (int)((Vector2) possibleAttacks[i]).x <= frontLines[(int)((Vector2) possibleAttacks[i]).y];
			}
			if(!canLock){
				possibleAttacks.RemoveAt(i);
			}
		}
	}
	public bool isInBounds(int x, int y){
		if(x >= 0 && x < 10 && y >= 0 && y < 9){
			return true;
		}else{
			return false;
		}
	}

	public void checkAndAddMove(ChessPiece[,] pieces, ArrayList list, Vector2 target){
		int targetX = (int) target.x;
		int targetY = (int) target.y;

		if(isInBounds(targetX, targetY)){
			if(pieces[targetX, targetY] == null){
				list.Add(target);
			}
		}
	}
	public void checkAndAddAttack(ChessPiece[,] pieces, ArrayList list, Vector2 target){
		if(isInBounds((int) target.x, (int) target.y)){
			if(pieces[(int) target.x, (int) target.y] != null){
				if(GetRed() != pieces[(int) target.x, (int) target.y].GetRed()){
					list.Add(new Vector2((int) target.x, (int) target.y));
				}
			}
		}
	}

	public virtual void AttackAnimation(Vector3 startPosition, Vector2 start, Vector2 target){
	}
}
