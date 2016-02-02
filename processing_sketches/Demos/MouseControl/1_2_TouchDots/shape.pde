// this is the main asset for the shape game

class Shape
{
  ArrayList<PVector> points;
  float scale = 250.;
  float fuzziness = 80.;
  float transx, transy;
  int hitting;


  int score;
  boolean[] hits = new boolean[0];
  boolean won;

  Shape(float[] _points)
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
  }

  void draw(float _x, float _y)
  {
    transx = _x;
    transy = _y;
    //resetMatrix();
    translate(transx, transy, 0);
    
    stroke(255,10);
    strokeWeight(2);
    beginShape();
    for (int i = 0; i<points.size (); i++)
    {
     vertex(points.get(i).x*scale, points.get(i).y*scale);
    }
    vertex(points.get(0).x*scale, points.get(0).y*scale);
    endShape();
    
    //Draw Markers
    stroke(20,125,105);
     for (int i = 0; i<points.size (); i++){
      ellipse(points.get(i).x*scale, points.get(i).y*scale, 40, 40);
    }
     
    if (hitting>-1)
    {
      fill(200,50,0,100);
      noStroke();
      ellipse(points.get(hitting).x*scale, points.get(hitting).y*scale, fuzziness+20, fuzziness+20);
    }
  }

  void init()
  {
    score = 0;
    for (int i = 0; i<hits.length; i++)
    {
      hits[i] = false;
    }
    won = false;
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
      if(score>=points.size()) won=true;
    }

    return(hitting);
  }
}

//
// THIS IS THE INITIALIZING FUNCTION
//

void initshapes()
{
  shape = new float[] {
    0, -1, 1, 1, -1, 1
  };
  shapelist[0] = new Shape(shape);
  shape = new float[] {
    -1, -1, 1, -1, 1, 1, -1, 1
  };
  shapelist[1] = new Shape(shape);
  
  shape = new float[40];
  for (int i = 0; i<40; i+=2)
  {
    float rad = map(i, 0, 20, 0, TWO_PI);
    float x = sin(rad);
    float y = cos(rad);
    shape[i] = x;
    shape[i+1] = y;
  }
  shapelist[2] = new Shape(shape);
  
  shape = new float[] {
    0, -1, 1, -1, 0, 1, -1, 1
  };
  
  shapelist[3] = new Shape(shape);
  
    shape = new float[] {
     -1, 0, 0, -1, 1, 0, 1, 1, -1, 1
  };
  
  shapelist[4] = new Shape(shape);
  
  shape = new float[] {
     -2, 0, 0, -1.5, 2, 0, 1, 0, 1, 1.5, -1, 1.5, -1, 0
  };
  shapelist[5] = new Shape(shape);
}