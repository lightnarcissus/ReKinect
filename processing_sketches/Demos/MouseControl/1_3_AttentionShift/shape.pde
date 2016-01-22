// this is the main asset for the shape game

class Shape1
{
  ArrayList<PVector> points;
  float scale = 250.;
  float fuzziness = 100.;
  float transx, transy;
  int hitting;


  int score;
  boolean[] hits = new boolean[0];
  boolean won, lose;

  Shape1(float[] _points)
  {
    points = new ArrayList<PVector>();


    for (int i = 0; i<_points.length; i+=2)
    {
      PVector foo = new PVector(_points[i], _points[i+1]);
      points.add(foo);
    }
    hits = new boolean[points.size()];
    for (int i = 0; i<hits.length; i++)
    {
      hits[i] = false;
    }
    transx = 0;
    transy = 0;
    hitting = -1;
    score = 0;
    won = false;
    lose = false;
  }

  void draw(float _x, float _y)
  {
    transx = _x;
    transy = _y;
    //resetMatrix();
    translate(transx, transy, 0);
    
    //stroke(255,10);
    //strokeWeight(2);
    //beginShape();
    //for (int i = 0; i<points.size (); i++)
    //{
    // vertex(points.get(i).x*scale, points.get(i).y*scale);
    //}
    //vertex(points.get(0).x*scale, points.get(0).y*scale);
    //endShape();
    
    //Draw Markers
   
     for (int i = 0; i<2; i++){
      stroke(255,255,0, 170);
      noFill();
      ellipse(points.get(i).x*scale, points.get(i).y*scale, 40, 40);
      
      fill(255,255,0,170);
      textSize(30);
      text(i+1, points.get(i).x*scale, points.get(i).y*scale+10);
    }
     
    if (hitting>-1)
    {
      //fill(200,50,0,100);
      fill(255,210,210,50);
      noStroke();
      ellipse(points.get(hitting).x*scale, points.get(hitting).y*scale, fuzziness+20, fuzziness+20);
    }
    
    //Draw Original 
    stroke(255,255,0,170);
    strokeWeight(6);
    noFill();
    beginShape();
    for (int i = 0; i<points.size (); i++)
    {
     vertex(width/2.5+points.get(i).x*scale/3.5, -height/2.7+points.get(i).y*scale/3.5);
    }
    vertex(width/2.5+points.get(0).x*scale/3.5, -height/2.7+points.get(0).y*scale/3.5);
    endShape();
    }
  

  void init()
  {
    score = 0;
    for (int i = 0; i<hits.length; i++)
    {
      hits[i] = false;
    }
    won = false;
    lose = false;
  }
  
  int detect(float _x, float _y)
  {
    int detect = -1;
    float hx =_x-transx;
    float hy =_y-transy;
    float closest = 1000000.; // idiotic
    for (int i = 0; i<points.size (); i++)
    {
      float dist = 0.0;
      dist += (points.get(i).x*scale-hx)*(points.get(i).x*scale-hx);
      dist += (points.get(i).y*scale-hy)*(points.get(i).y*scale-hy);
      dist = sqrt(dist);
      if (dist<fuzziness) {
        if(dist<closest) {
          detect = i;
          closest = dist;
        }
      }
    }
    hitting = detect;
    if (hitting>-1) {
      if (hits[hitting]==false) {
        score++;
        hits[hitting] = true;
      }
      
      
      if(score>=points.size() && hitting == 0) {  won= true;  }
      
      if(score<points.size() && score > 1 && hitting == 0){ lose = true; }
      
    }
  
    return(hitting);
  }
}

//
// THIS IS THE INITIALIZING FUNCTION
//

void initshapes1()
{
  shape1 = new float[] {
     0, -1, 1, 0, 1, 1, -1, 1, -1, 0
  };
  
  shapelist1[0] = new Shape1(shape1);
  
  shape1 = new float[] {
    0, -1.5, 2, 0, 1, 0, 1, 1.5, -1, 1.5, -1, 0,  -2, 0
  };
  shapelist1[1] = new Shape1(shape1);
}