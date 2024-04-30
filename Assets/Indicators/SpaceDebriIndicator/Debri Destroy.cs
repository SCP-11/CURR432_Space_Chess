using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriDestroy : MonoBehaviour
{
    private Board board;
    private int x;
    private int y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyPawn(){
        if(board != null){
            ChessPiece target = board.pieces[x, y];
            board.pieces[x, y] = null;
            board.RemovePieceAfterAnimation(target);
        }
    }
    public void DestroySelf(){
        Destroy(gameObject);
    }
    public void DestroyXY(Board board, int x, int y){
        this.board  = board;
        this.x = x;
        this.y = y;
    }
}
