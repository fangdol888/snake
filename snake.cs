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
		
		public static bool operator==(Point p1, Point p2)
		{
			if(p1.x==p2.x && p1.y==p2.y) return true;
			else return false;
		}
		public static bool operator!=(Point p1, Point p2)
		{
			if(p1.x!=p2.x || p1.y!=p2.y) return true;
			else return false;
		}
		public static Point operator+(Point p1, Point p2)
		{
			Point res = p1;
			res.x += p2.x;
			res.y += p2.y;

			return res;
		}
	}
	public enum direction{up,down,left,right};//방향 열거
	public enum S{ up,down,left,right, body, wall, pray}; //모양이름설정
	
	public class Snake{
		public List<Point> body = new List<Point>(); //몸
		int nFrame = 10;
		int nStay;
		
		public static string[] shape = {"▲" ,"▼","◀","▶","◆","▣","◈"};//모양
		int dir= (int)direction.right; //방향변수
		int len = 3; //초기 길이
		int dx=0, dy=0;
		
		public Snake(){ //초기화
			for(int i =0; i < len ; i++){				
				body.Add(new Point(Map.x/2,12+i)); //뱀 초기 위치
			}
			nStay = nFrame;
			dir = (int)direction.up;
		}
		
		public void eat(Point pray){//다음 위치가 먹이일때
			body.Insert(0,new Point(pray.x,pray.y)); //길이 길어짐
			len++; //길이 늘어남
		}
		
		public bool countFrame(){
			if(nFrame > 0){
				nFrame--;
				return false;
			}else{
				nFrame = nStay-1;
				return true;
			}
		}
		
		public bool move(int dir){//이동 후 종료 결정
			this.dir = dir;
			
			
			dx=0;
			dy=0;
			
			switch((direction)dir){ //방향에 따른 머리 이동 위치 확인
				case direction.up:
				dy=-1;
				break;
				case direction.down:
				dy=1;
				break;
				case direction.left:
				dx=-1;
				break;
				case direction.right:
				dx=1;
				break;
			}
			
			if(body[0].x+dx == Map.pray.x && body[0].y+dy == Map.pray.y  ) //먹이 충돌?
			{
				eat(Map.pray);
				
				Random r = new Random();
				int rx,ry;
				
				Map.score++;
				
				for(int i=0;i <body.Count;i++){
					if(body[i] == Map.pray){
						rx = r.Next(1,Map.x-1);
						ry = r.Next(1,Map.y-1);
						Map.pray.setPoint(rx,ry);
						i = -1;
					}
				}
			}
			else{ //아님 그냥 이동
				printPoint(body[len-1].x,body[len-1].y, "  "); //delete trail
			
				for(int i = body.Count-1;i > 0; i--){
					body[i].setPoint(body[i-1].x,body[i-1].y);
				}
			
				body[0].x+=dx; //그 뱡향으로 머리 이동
				body[0].y+=dy;
			}
			
			printBody();
			
			for(int i = 1; i < body.Count; i++){
				if(body[i] == body[0]){//몸 충돌
					return false;//게임오버
				}
			}
			if(body[0].x == 0 || body[0].x == Map.x-1 || body[0].y == 0 || body[0].y == Map.y-1) return false; // 벽 충돌 
			
			return true;
		}
		
		public Point getHead(){return body[0];} //머리 위치 반환
		public string headShape(int dir){return shape[dir];}
		
		public void printBody(){
			printPoint(body[0].x,body[0].y, shape[dir]);
			for(int i =1 ; i < body.Count;i++){
				printPoint(body[i].x,body[i].y, shape[(int)S.body]);
			}
		}
		public void printPoint(int x,int y, string shape){ //그 위치에 출력
			Console.SetCursorPosition(x*2,y);
			Console.Write(shape);
		}
	}
	
	public class Map{
		public static int x = 20, y=24; //맵 크기
		Snake snake = new Snake();
		public static Point pray; //먹이 위치
		public static int score = 0;
		Random r = new Random();
		
		public Map(){
			int rx = r.Next(1,x-1);
			int ry = r.Next(1,y-1);
			pray = new Point(rx,ry); //초기 먹이 위치
			for(int i=0;i <snake.body.Count;i++){
				if(snake.body[i] == pray){
					rx = r.Next(1,x-1);
					ry = r.Next(1,y-1);
					pray.setPoint(rx,ry);
					i = -1;
				}
			}
		}
		
		public void start(){
			Snake snake = new Snake();
			int dir = (int)direction.up; //초기 방향
			char input;
			int frame = 60;
			drawMap(); //초기 맵 그리기
			snake.printBody();
			drawPray();
			while(true){
				
				//키 입력 시 방향 전환
				if(Console.KeyAvailable == true){
					input = Console.ReadKey(true).KeyChar;
					
					//방향 결정
					switch(Char.ToLower(input)){
						case 'w':
						dir = (int)direction.up;
						break;
						case 's':
						dir = (int)direction.down;
						break;
						case 'a':
						dir = (int)direction.left;
						break;
						case 'd':
						dir = (int)direction.right;
						break;
						case 'q':
						return;//종료
						case 'p':
						while(Console.ReadKey(true).KeyChar != 'p'){}
						break;
					}
				}
				
				//이동
				if(snake.countFrame()){ //움직이는데 성공 했나?
					if(snake.move(dir)){
						drawStatus();
						drawPray();
					}
					else{//벽이나 몸 충돌시
					//게임오버 출력
					// 나가기
					break;
					}
				}
				
				
				
				//딜레이
				Thread.Sleep(1000/frame);
			}
		}
		
		public void drawMap(){ //벽 그리기
			for(int i= 0 ; i < Map.x;i++){
				printPoint(i,0,Snake.shape[(int)S.wall]);
			}
			
			for(int i=1;i< Map.y-1;i++){
				printPoint(0,i,Snake.shape[(int)S.wall]);
				printPoint(Map.x-1,i,Snake.shape[(int)S.wall]);
			}
			
			for(int i= 0 ; i< Map.x;i++){
				printPoint(i,Map.y-1,Snake.shape[(int)S.wall]);
			}
			drawStatus();
		}
		public void drawPray(){
			printPoint(pray.x,pray.y,Snake.shape[(int)S.pray]);
		}
		public void drawStatus(){
			Console.SetCursorPosition(0,24);
			Console.Write("Score: {0}", score);
		}
		public void printPoint(int x,int y, string shape){ //그 위치에 출력
			Console.SetCursorPosition(x*2,y);
			Console.Write(shape);
		}
	}
	
	public class Program{
		public static void Main(string[] args){
			Console.CursorVisible = false; //커서 숨기기
			Map m = new Map();
			m.start();
		}
	}
}