
Timer timer ;

//LUKE-S
Shape[] shapelist = new Shape[6];
float[] shape = new float[0];
int whichshape = 0;
//LUKE-E

int score;
int r = 360;
PImage Watch;

int tinyTimer;

void setup() {
  size(displayWidth, displayHeight, P3D);
  //size(1000, 800, P3D);
  background(20);
  noStroke();
  smooth();

  initshapes();
  score = 0;
  tinyTimer = 0;

  Watch = loadImage("watch1w.png");

  timer = new Timer(width-300, height-300) ;
  timer.start() ;

  frameRate(30);
}

void draw() {
  //drawGamePage(); 
  drawLUKEGamePage();

  image(Watch, width-170, height-400, 100, 100 );
  timer.DisplayTime() ;

  toggleDone();
  reward_DrawingGame2();
  
  resetButton();
  
}

void resetButton(){
 //RESET BUTTON//
  noStroke();
  if(mouseX > width-250-175 && mouseX < width-250+175 && mouseY > height-200-50 && mouseY < height-200+50){
    fill(125);
  } else{
    fill(80);
  }
  rectMode(CENTER);
  rect(width-250, height-200, 350, 100);

  textSize(60);
  textAlign(CENTER);
  fill(255);
  text("RESET", width-250, height-180);
}

void mouseClicked()
{
  if(mouseX > width-250-175 && mouseX < width-250+175 && mouseY > height-200-50 && mouseY < height-200+50){
    shapelist[whichshape].init();
    tinyTimer = 0;
    score = 0;
    timer.restart();
  }
}

void keyReleased()
{
  nextshape();
}

void nextshape()
{
  whichshape = (whichshape+1) % shapelist.length; 
  shapelist[whichshape].init();
  tinyTimer = 0;
}

void drawLUKEGamePage() {
  background(20);
  pushMatrix();
  //resetMatrix();
  translate(0, 0, 0);

  noFill();
  stroke(255);
  strokeWeight(5.);
  shapelist[whichshape].draw(width/2, height/2);
  int d = shapelist[whichshape].detect(mouseX, mouseY);
  resetMatrix();
  
  popMatrix();
  
  if (shapelist[whichshape].won) {
    fill(125,50);
    rectMode(CORNER);
    rect(0,0,width,height);
    fill(255);
    textAlign(CENTER);
    textSize(90);
    text("YOU WIN!", width/2, height/2);
    textSize(30);
    text("Moving To The Next Round ....", width/2, height/2 + 40);
    
    tinyTimer ++ ; 
    println(tinyTimer);
    
    if(tinyTimer == 45){
    nextshape();
    timer.restart();
    }
  }
  
  fill(255);
  textSize(60);
  textAlign(LEFT);
  d= d+1;
  text("Point#: " + d, 50, 70);  
  text("SCORE: " + shapelist[whichshape].score, 50, 135);
}

void drawGamePage() {

  fill(20);
  rectMode(CORNER);
  rect(0, 0, width/4, 140);
  rect(width-450, height-400, 450, 400);

  pushMatrix();
  translate(width/2, height/2, 5);  

  //BACKGROUND **************************
  fill(20, 10);
  rectMode(CENTER);
  rect(-500, 140, width, height);

  stroke(255);
  strokeWeight(2.); 
  noFill();

  translate(-width/2, -height/2, 0); 
  //***ARM MOVE
  if (mousePressed == true) {
    stroke(12, 255, 50);
    strokeWeight(50);
    line(mouseX, mouseY, pmouseX, pmouseY);
  }

  popMatrix();

  //RESET BUTTON//
  noStroke();
  fill(125);
  rect(width-50-200, height-200, 400, 100);

  textSize(70);
  textAlign(CENTER);
  fill(255);
  text("RESET", width-50-200, height-180);

  tint(255);
  image(Watch, width-170, height-400, 100, 100 );

  drawMenu();

  timer.DisplayTime() ;

  toggleDone();
  //reward();
}