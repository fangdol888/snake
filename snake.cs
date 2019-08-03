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
	public class Snake{
		List<Point> body = new List<Point>(); //몸
		enum direction{up,down,left,right};//방향설정
		int dir= direction.right; //방향변수
		
		public snake(){ //초기화
			body[0].setPoint();
			
		}
	}
	
	public class Map{
		public static int x = 40, y=24; //맵 크기
		
		public Map(){
			
		}
	}
	public class Program{
		public static void Main(string[] args){
			
		}
	}
}