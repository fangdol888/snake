using System;
using System.Collections.Generic;
using System.Threading;

namespace app{
	public class Point{ //점
		public int x,y;
		public Point(int x, int y){
			this.x=x;
			this.y=y;
		}
		public void setPoint(int x,int y){
			this.x=x;
			this.y=y;
		}
	}
	public enum direction{up,down,left,right};//방향 열거
	
	public class Snake{
		List<Point> body = new List<Point>(); //몸
		enum S{ up,down,left,right, body, wall, pray}; //모양이름설정
		public static string[] shape = {"▲" ,"▼","◀","▶","◆","▣","◈"};//모양
		int dir= (int)direction.right; //방향변수
		int len = 3; //초기 길이
		
		public Snake(){ //초기화
			for(int i =0; i < len ; i++){				
				body.Add(new Point(20,12-i)); //뱀 초기 위치
			}
			
			dir = (int)direction.up;
		}
		
		public void eat(int x, int y){//다음 위치가 먹이일때
			body.Insert(0,new Point(x,y)); //길이 길어짐
			len++; //길이 늘어남
		}
		
		public void move(int dir){//이동
			this.dir = dir;
			
			for(int i = body.Count; i>0 ;i--){
				body[i] = body[i-1];
			}
			int dx=0, dy=0;
			
			switch((direction)dir){ //방향에 따른 머리 이동 위치 확인
				case direction.up:
				dy--;
				break;
				case direction.down:
				dy++;
				break;
				case direction.left:
				dx--;
				break;
				case direction.right:
				dx++;
				break;
			}
			body[0].setPoint(body[0].x+dx,body[0].y+dy); //그 뱡향으로 머리 이동
			
		}
		
		public Point getHead(){return body[0];} //머리 위치 반환
		public void printPoint(int x,int y, string shape){ //그 위치에 출력
			Console.SetCursorPosition(x*2,y);
			Console.Write(shape);
		}
	}
	
	public class Map{
		public static int x = 40, y=24; //맵 크기
		Snake snake = new Snake();
		Point pray; //먹이 위치
		Random r = new Random();
		
		public Map(){
			int rx = r.Next(1,x-1);
			int ry = r.Next(1,y-1);
			pray = new Point(rx,ry); //초기 먹이 위치
			
		}
		
		public void start(){
			Snake snake = new Snake();
			int dir = (int)direction.up; //초기 방향
			char input;
			drawMap();
			
			while(true){
				//딜레이
				Thread.sleep(500);
				//키 입력 시 방향 전환
				if(Console.KeyAvailable == true){
					input = Console.ReadKey(true).KeyChar	
					
				}
				//이동 전 충돌 판정
				
				//먹이 충돌
				
				//벽 or 몸 충돌
				
				//이동
				snake.move(dir);
				
			}
		}
		
		public void drawMap(){
			for(int i= 0 ; i< Map.x;i++){
				printPoint(i,0,Snake.shape[(int)S.wall]);
			}
			for(int i= 0 ; i< Map.y-1;i++){
				printPoint(0,i,Snake.shape[(int)S.wall]);
				printPoint(40,i,Snake.shape[(int)S.wall]);
			}
			
			for(int i= 0 ; i< Map.x;i++){
				printPoint(i,23,Snake.shape[(int)S.wall]);
			}
			
		}
		public void printPoint(int x,int y, string shape){ //그 위치에 출력
			Console.SetCursorPosition(x*2,y);
			Console.Write(shape);
		}
	}
	
	public class Program{
		public static void Main(string[] args){
			
		}
	}
}