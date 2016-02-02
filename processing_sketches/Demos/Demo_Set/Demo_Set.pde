//track, timer, corner point detection

//LUKE-S
Shape[] shapelist = new Shape[4];
float[] shape = new float[0];

Shape1[] shapelist1 = new Shape1[2];
float[] shape1 = new float[0];


int whichshape = 0;
//LUKE-E

//CLAIRE-S

int picnum = 4; //4, 6
//int a=3;
int b=350;
Pic[] pics; 
Pic[] pics2;

int startTime;
int x;
int f;
int picsize = 100;
int whichoneamimoving = -1;
int match=0;
float x1, y1, x2, y2=0;
int c1, c2, c3;
int num;
boolean init1=false;

//CLAIRE-E

int tinyTimer;

//Toggle Pages
boolean frontOn = true;
boolean menuOn, game1On, game2On, game3On, game4On = false;
boolean inst1On, inst2On, inst3On, inst4On = false;
boolean pageClearOn = false;

//FRONT PAGE
String text1 = " ";
PFont font1, font2;

//MENU PAGE
PImage menu1, menu2, menu3;

//INSTRUCTION PAGE

int r = 360;
float angle = 0;
int[][] testPoints;
PVector[] points;

boolean isDone = false;
PImage Done;
PImage Watch;
PImage Bg;
PImage GuideCircle;
PImage GuideRect;
PImage Start;

float scale =0.0;
float startX, startY;

boolean circleMode = true;
boolean rectMode = false;

int score ;

//Splash Page
PImage title1, name, arrow, probSide, left, right;
PImage game1_inst;

Timer timer ;

//////////////////KINECT SENSING----------S//////////////////
import KinectPV2.KJoint;
import KinectPV2.*;

KinectPV2 kinect;

boolean KinectDraw = false;
////////////////////KINECT SENSING----------E////////////////


void setup() {
  // size(1000, 800);
  size(displayWidth, displayHeight, P3D);
  background(20);
  noStroke();
  smooth();

  initshapes();
  initshapes1();
  score = 0;
  tinyTimer = 0;

 startTime = millis();
  
  font1 = loadFont("AmericanTypewriter-48.vlw");
  font2 = createFont("Helvetica-Bold", 50);

  testPoints = new int[33][33]; 
  points = new PVector[33];
  // First []: each point's x position 
  // Second []: each point's y position   

  //FRONT PAGE
  title1 = loadImage("newImgs/title1.png");
  //title2 = loadImage("title2.png");
  //title3 = loadImage("title3.png");
  name = loadImage("newImgs/name.png");
  arrow = loadImage("arrow.png");
  probSide = loadImage("newImgs/probSide.png");
  left = loadImage("newImgs/left.png");
  right = loadImage("newImgs/right.png");
  
  //MENU PAGE
  menu1 = loadImage("menu1.png");
  menu2 = loadImage("menu2.png");
  menu3 = loadImage("menu3.png");

  //INSTRUCTION PAGE
  game1_inst = loadImage("game1_inst.png");

  Done = loadImage("welldone.gif");
  Watch = loadImage("watch1w.png");
  Bg = loadImage("bg.jpg");
  GuideCircle = loadImage("dashed_wcircle.png");
  GuideRect = loadImage("guildRect1.png");
  Start = loadImage("starticon.png");

  //timer = new Timer(width-250, 120) ; // make the display at location (10,60)
  timer = new Timer(width-300, height-300) ;
  timer.start() ;

  frameRate(30);
  
  frontOn = true;
  menuOn =  game1On = game2On = game3On = game4On = inst1On = inst2On = inst3On = inst4On = false;

  
  //////////////////KINECT SENSING----------S//////////////////

  kinect = new KinectPV2(this);

  kinect.enableSkeletonColorMap(true);
  kinect.enableColorImg(true);

  kinect.init();

  ////////////////////KINECT SENSING----------E////////////////
  
}

void draw() {
  noStroke();

  if (frontOn) {
    drawFrontPage();
  } else if (menuOn) {
    drawMenuPage();
  } else if (inst1On) {
    drawInst1Page();
  }  else if (inst2On) {
    drawInst2Page();
  }
  
  if (game1On) {
    inst1On = false;
    pageClear();
    drawGame1Page();
  } 
  
  if (game2On) {
        inst1On = false;
    pageClear();
    drawGame2Page();
  } 
  
  if (game3On) {
        inst1On = false;
    pageClear();
    drawGame3Page();
  } 
  
  if(game4On) {
    inst2On = false;
    pageClear();
    drawGame4Page();
  } 
 
  //if(KinectDraw) {
 
    ///////////////////KINECT SENSING----------S//////////////////
    pushMatrix();
    translate(0,200,6);
     
    ArrayList<KSkeleton> skeletonArray =  kinect.getSkeletonColorMap();

    //individual JOINTS
    for (int i = 0; i < skeletonArray.size(); i++) {
      KSkeleton skeleton = (KSkeleton) skeletonArray.get(i);
      if (skeleton.isTracked()) {
        KJoint[] joints = skeleton.getJoints();

        color col  = skeleton.getIndexColor();
        fill(col);
        stroke(col);
        //drawBody(joints);

        //draw different color for each hand state
        drawHandState(joints[KinectPV2.JointType_HandRight]);
        //drawHandState(joints[KinectPV2.JointType_HandLeft]);
       
      if(game1On){  
        checkMatch(joints, KinectPV2.JointType_HandRight); 
      }
        //println(  joints[KinectPV2.JointType_HandRight].getX(), "    ", joints[KinectPV2.JointType_HandRight].getY(), "    ", joints[KinectPV2.JointType_HandRight].getZ()  );
      }
    }
    popMatrix();
    
    //////////////////KINECT SENSING----------E//////////////////
    
 //}

  //fill(20);
  //rect(0, 0, width, 140);
  //fill(20, 8);
  //rect(0, 140, width, height-140);
  //  tint(255, 127);
  //  aimage(Bg,0,0, width, height);
  //  float dist = abs((mouseX-pmouseX)*(mouseX-pmouseX)) + abs((mouseY-pmouseY)*(mouseY-pmouseY));
  //  println(dist);

  /*
  pushMatrix();
   
   translate(width/2, height/2);
   stroke(255);
   strokeWeight(2.); 
   noFill();
   // ellipse(0, 0, r*2, r*2);
   
   tint(125,10);
   if(circleMode){
   image(GuideCircle, -GuideCircle.width*1.68/2, -GuideCircle.height*1.68/2, GuideCircle.width*1.68, GuideCircle.height*1.68);
   } else if(rectMode){
   image(GuideRect, -GuideRect.width*0.8/2, -GuideRect.height*0.8/2, GuideRect.width*0.8, GuideRect.height*0.8);
   }
   
   fill(255);
   noStroke();
   ellipse(0, -r, 20, 20);
   
   popMatrix();
   
   //***ARM MOVE
   //ellipse(mouseX, mouseY, dist/2, dist/2);
   //ellipse(mouseX, mouseY, 50, 50);
   if (mousePressed == true) {
   stroke(12, 255, 50);
   strokeWeight(50);
   line(mouseX, mouseY, pmouseX, pmouseY);
   checkMatch();
   }
   
   tint(255);
   image(Watch, width-150, 50, 100, 100 );
   
   drawMenu();
   
   //  stroke(11,153,0);
   //  strokeWeight(8);
   //  noFill();
   //  ellipse(startX, startY, 55, 55);
   image(Start, startX-Start.width*0.15, startY-Start.height*0.3, Start.width*0.3, Start.height*0.3);
   
   timer.DisplayTime() ;
   
   toggleDone();
   
   */
 
}

void keyPressed() {
  if (key == BACKSPACE) {
    if (text1.length() > 0) {
      text1 = text1.substring(0, text1.length() - 1);
    }
  } else if (key == '1') {
    
    game1On = true;
    game2On = game3On = game4On = false;
    
  } else if (key == '2') {
     game2On = true;
     game1On = game3On = game4On = false;
  } else if (key == '3') {
     game3On = true;
     game1On = game2On = game4On = false;
  } else if (key == ENTER) {
    if (frontOn) {
      frontOn = false;
      menuOn = true;
      game1On = game2On = game3On = false;
    } else if (menuOn) {
      frontOn = false;
      menuOn = false;
      game1On = game2On = game3On = false;
    }
    //return; //text(text1, 0, 40+25, width, height);
  } else {
    text1 += key;
  }
}

void keyReleased()
{
  if ((key == 's') || (key == 'S') )
  {
    timer.restart() ;
    println("reset") ;
  }
  if ((key == 'p') || (key == 'P') )
  {
    timer.pause() ;
    println("pause") ;
  }
  if ((key == 'c') || (key == 'C') )
  {
    timer.continueRunning() ;
    println("continue") ;
  }
  if ((key == 'd') || (key == 'D') )
  {
    // image(Done, (width-Done.width)/2, (height-Done.height)/2);
    image(kinect.getColorImage(), 0, 0, width, height);
  }
  if ((key == 'p') || (key == 'P') )
  {
    pushMatrix();
    translate(width/2, height/2);
    for (int i = 0; i < 33; i++) {
      fill(0, 0, 255);
      ellipse(r*cos(PI*i/16), r*sin(PI*i/16), 10, 10);
    }
    popMatrix();
  }
  if ((key == 'e') || (key == 'E') )
  {
    circleMode = true;
    rectMode = false;
  }
  if ((key == 'r') || (key == 'R') )
  {
    circleMode = false;
    rectMode = true;
  }
  if ((key == 'k') || (key == 'K') )
  {
    KinectDraw = true;
  }
    if ((key == 'n') || (key == 'N') )
  {
    //KinectDraw = true;
    nextshape();
  }
}

void mouseClicked(MouseEvent event) {

  //FRONT PAGE
  if (frontOn) {
    if (mouseX>1112 && mouseY>783 && mouseX <1112+arrow.width && mouseY < 783+arrow.height) {
      frontOn = false;
      menuOn = true;
      game1On = game2On = game3On = false;
      println("HEY!!");
    }
  }

  //MENU SELECT
  if (menuOn) {
    if (mouseX>width/2-600-270 && mouseX<width/2-600+270 && mouseY>height/2-100-270 && mouseY<height/2-100+270) {
      inst1On = true;
      frontOn = menuOn = game1On = game2On = game3On = false;
      inst2On = inst3On = false;
    } else if (mouseX>width/2-270 && mouseX<width/2+270 && mouseY>height/2-100-270 && mouseY<height/2-100+270) {
      inst2On = true;
      frontOn = menuOn = game1On = game2On = game3On = false;
      inst1On = inst3On = false;
    } else if (mouseX>width/2+600-270 && mouseX<width/2+600+270 && mouseY>height/2-100-270 && mouseY<height/2-100+270) {
      inst3On = true;
      frontOn = menuOn = game1On = game2On = game3On = false;
      inst1On = inst2On = false;
    }
  }

  //START GAME
  if (inst1On) {
    if (mouseX > width/2+370+150-300 && mouseX < width/2+370+150 + 300 && mouseY > height/2+ 450 - 100 && mouseY < height/2+450 +100) {
      pageClearOn = true;
      game1On = true;
      frontOn = menuOn = game2On = game3On = game4On = false;
      inst1On = inst2On = inst3On = false;
    }
  }
  
  if (inst2On) {
    if (mouseX > width/2+370+150-300 && mouseX < width/2+370+150 + 300 && mouseY > height/2+ 450 - 100 && mouseY < height/2+450 +100) {
      pageClearOn = true;
      game4On = true;
      frontOn = menuOn = game1On = game2On = game3On = false;
      inst1On = inst3On = false;
      
      println("4 ONONONON!");
    }
  }
////////////////////////////////////////////////

if(game2On){
if(mouseX > width-250-175 && mouseX < width-250+175 && mouseY > height-200-50 && mouseY < height-200+50){
    shapelist[whichshape].init();
    tinyTimer = 0;
    score = 0;
    timer.restart();
  }
}

if(game3On){
if(mouseX > width-250-175 && mouseX < width-250+175 && mouseY > height-200-50 && mouseY < height-200+50){
    shapelist1[whichshape].init();
    tinyTimer = 0;
    score = 0;
    timer.restart();
  }
}

  if(game1On){
    
  if(mouseX> width-450 &&mouseX < width-50 && mouseY > height-300 && mouseY < height-200){
    noStroke();
    timer.restart() ;
    score = 0;
} else {
  startX = mouseX;
  startY = mouseY;
}
  }

  //-----------------------------------------------------------------
 
  
}