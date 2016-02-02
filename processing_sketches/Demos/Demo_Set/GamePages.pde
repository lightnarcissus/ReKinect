
void drawGame1Page() {

  fill(20);
  rectMode(CORNER);
  rect(0, 0, width/4, 140);
  //stroke(255);
  rect(width-450, height-400, 450, 400);
  //rect(0, 0, width, height);
  //------------------------GAME1 PAGE---S--------------------------------------
  pushMatrix();
  translate(width/2, height/2, 0);  
  //BACKGROUND **************************
  fill(20,10);
  rectMode(CENTER);
  rect(-500, 140, width, height);
  ///////////////////////////////////////

  stroke(255);
  strokeWeight(2.); 
  noFill();


  tint(125, 50);
  if (circleMode) {
    //image(GuideCircle, -200-GuideCircle.width*1.68/2, -GuideCircle.height*1.68/2, GuideCircle.width*1.68, GuideCircle.height*1.68);
    image(GuideCircle, -200-GuideCircle.width*1.5/2, -GuideCircle.height*1.5/2, GuideCircle.width*1.5, GuideCircle.height*1.5);
  } else if (rectMode) {
    image(GuideRect, -200-GuideRect.width*0.8/2, -GuideRect.height*0.8/2, GuideRect.width*0.8, GuideRect.height*0.8);
  }

  fill(255);
  noStroke();
  translate(-width/2, -height/2, 0); 
   //***ARM MOVE
  //ellipse(mouseX, mouseY, dist/2, dist/2);
  //ellipse(mouseX, mouseY, 50, 50);
  if (mousePressed == true) {
    stroke(12, 255, 50);
    strokeWeight(50);
    line(mouseX, mouseY, pmouseX, pmouseY);
   // checkMatch();
    //checkMatch(joints, KinectPV2.JointType_HandRight);
   /////////////////////////////////////////////////////////////////////*******************************************************************************************
  }

  popMatrix();
 
 /*
 //RESET BUTTON//
 noStroke();
  fill(125);
  rect(width-50-200,height-200,400,100);

textSize(70);
textAlign(CENTER);
fill(255);
text("RESET", width-50-200,height-180);
 */

  drawMenu();

  //  stroke(11,153,0);
  //  strokeWeight(8);
  //  noFill();
  //  ellipse(startX, startY, 55, 55);
 // image(Start, startX-Start.width*0.15, startY-Start.height*0.3, Start.width*0.3, Start.height*0.3);
 
  tint(255);
  image(Watch, width-170, height-400, 100, 100 );
  
  timer.DisplayTime() ;
  toggleDone();
  resetButton();
  //reward();
  reward();
  //------------------------GAME1 PAGE---E--------------------------------------
}


/////////////////////////////////////////////////////////////////////////////////


void drawGame2Page(){  //LUKE DRAWING GAME 2
  fill(20,40);
  noStroke();
  rect(width/2,height/2,width,height);
  
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
    noFill();
    rect(width/2,height/2,width,height);
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
  text("points: " + d, 50, 70);  
  text("score: " + shapelist[whichshape].score, 50, 135);
  
  reward_DrawingGame2();
  
  if (mousePressed == true) {
    stroke(12, 255, 50);
    strokeWeight(50);
    //line(mouseX, mouseY, pmouseX, pmouseY);
    ellipse(mouseX,mouseY,50,50);
   // checkMatch();
    //checkMatch(joints, KinectPV2.JointType_HandRight);
   /////////////////////////////////////////////////////////////////////*******************************************************************************************
  }
   
  tint(255);
  image(Watch, width-170, height-400, 100, 100 );
  
  timer.DisplayTime() ;
  toggleDone();
  resetButton();
  //reward();

}

void drawGame3Page(){
  fill(20,50);
  noStroke();
  rect(width/2,height/2,width,height);
  
  
  pushMatrix();
  //resetMatrix();
  translate(0, 0, 0);

  noFill();
  stroke(255);
  strokeWeight(5.);
  shapelist1[whichshape].draw(width/2, height/2);
  int d = shapelist1[whichshape].detect(mouseX, mouseY);
  resetMatrix();
  
  popMatrix();
  
  if (shapelist1[whichshape].won) {
    fill(125,40);
    noStroke();
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
    nextshape1();
    timer.restart();
    }
  }   
    
    if (shapelist1[whichshape].lose) {
    fill(125,50);
    noStroke();
    rectMode(CORNER);
    rect(0,0,width,height);
    fill(255);
    textAlign(CENTER);
    textSize(90);
    text("YOU LOSE!", width/2, height/2);
    textSize(30);
    text("Starting Over ....", width/2, height/2 + 40);
    
    tinyTimer ++ ; 
    println(tinyTimer);
    
    if(tinyTimer == 45){
     shapelist1[whichshape].init();
    tinyTimer = 0;
    timer.restart();
    }  
    }
  
  fill(255);
  textSize(60);
  textAlign(LEFT);
  //d= d+1;
  text("points: " + d, 50, 70);  
  text("score: " + shapelist1[whichshape].score, 50, 135);

image(Watch, width-170, height-400, 100, 100 );
  timer.DisplayTime() ;

  toggleDone();
  reward_DrawingGame2();
  
  resetButton();
  
   if (mousePressed == true) {
    stroke(12, 255, 50);
    strokeWeight(50);
    //line(mouseX, mouseY, pmouseX, pmouseY);
    ellipse(mouseX,mouseY,50,50);
   // checkMatch();
    //checkMatch(joints, KinectPV2.JointType_HandRight);
   /////////////////////////////////////////////////////////////////////*******************************************************************************************
  }

}

void drawGame4Page(){
 pushMatrix();
  translate(width/2, height/2, 5);  
  //BACKGROUND **************************
  fill(255,10);
  rectMode(CENTER);
  rect(0, 0, width, height);
  
  popMatrix();
println("DRAWING 4!");
}