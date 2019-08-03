using System;
using System.Collections.Generic;

namespace app{
	public class Point{
		public int x,y;
		public Point(int x, int y){
			this.x=x;
			this.y=y;
		}
		public setPoint(int x,int y){
			this.x=x;
			this.y=y;
		}
	}
	public enum direction{up,down,left,right};//방향
	
	public class Snake{
		List<Point> body = new List<Point>(); //몸
		enum S{ up,down,left,right, body, wall, pray}; //모양이름설정
		string[] shape = {"▲" "▼","◀","▶","◆","▣","◈"}//모양
		int dir= direction.right; //방향변수
		int len = 3; //초기 길이
		
		public snake(){ //초기화
			for(int i =0; i < len ; i++){				
				body.add(new Point(20,12-i)); //뱀 초기 위치
			}
			
			dir = direction.up;
		}
		public void eat(){//다음 위치가 먹이일때
			move(dir); //이동 후
			body.add(new Point(body[len-1].x, body[len-1].y)) //
			
			len++;
		}
		public void move(int dir){
			for(int i = body.count(); i>0 ;i-){
				body[i] = body[i-1];
			}
		}
		public printPoint(int x,int y, string shape){
			Console.SetCursorPosition(x*2,y);
			Console.Write(shape);
		}
	}
	
	public class Map{
		public static int x = 40, y=24; //맵 크기
		Point pray; //먹이 위치
		Random r;
		
		public Map(){
			int rx = r.Next(1,x-1);
			int ry = r.Next(1,y-1);
			pray = new Point(rx,ry); //초기 먹이 위치
			
		}
		
		public void start(){
			Snake snake = new Snake();
			
		}
		
		public void drawMap(){
			
			
		}
	}
	public class Program{
		public static void Main(string[] args){
			
		}
	}
}