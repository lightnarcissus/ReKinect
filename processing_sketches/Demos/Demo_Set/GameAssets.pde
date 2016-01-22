void nextshape()
{
  whichshape = (whichshape+1) % shapelist.length; 
  shapelist[whichshape].init();
  tinyTimer = 0;
}

void nextshape1()
{
  whichshape = (whichshape+1) % shapelist1.length; 
  shapelist1[whichshape].init();
  tinyTimer = 0;
}

void toggleDone() {
}

void drawMenu() {

  textAlign(LEFT);
  textSize(60);
  fill(255);
  text("SCORE: " + score, 50, 120+15);
  text("START: " + int(startX) + " , " + int(startY), 50, 180+10+15);

}


void checkMatch(KJoint[] joints, int jointType1) {
//void checkMatch() {

  for (int i = 0; i < points.length; i++) {
    points[i] = new PVector(r*cos(PI*i/16), r*sin(PI*i/16));
    // println("x: " + points[i].x + "    y: " + points[i].y);

    //if (mouseX > -200+width/2 + points[i].x - 20 && mouseX < -200+width/2 + points[i].x + 20 
    // && mouseY > height/2 + points[i].y - 20 && mouseY < height/2 + points[i].y + 20) {

      /////////////joints[KinectPV2.JointType_HandRight].getX()
      
    if (joints[jointType1].getX() > width/2 + points[i].x - 20 && joints[jointType1].getX() < width/2 + points[i].x + 20 
       && joints[jointType1].getY() > height/2 + points[i].y - 20 && joints[jointType1].getY() < height/2 + points[i].y + 20) {
       
      score++;
      
      stroke(12, 255, 80);
      strokeWeight(4);
      noFill();
      //ellipse(-200+width/2 + points[i].x, height/2 + points[i].y, 65, 65);
      //if(drawEllipse){
        
        //DEBUG!
       // if(debugModeOn){
      ellipse(-200+width/2 + points[i].x, height/2 + points[i].y, 50, 50);
     // }  
  }
  }

  //scale++;
}

void reward() {
  if (score==20) {
    fill(255);
    textAlign(CENTER);
    textSize(200);
    text("+20!", width/2, height/2);
  }
  if (score==50) {
    // image(Done, (width-Done.width)/2, (height-Done.height)/2);
    fill(255);
    textAlign(CENTER);
    textSize(200);
    text("+50!", width/2, height/2);
  }
}

void reward_DrawingGame2() {
  if (score==3) {
    fill(255);
    textAlign(CENTER);
    textSize(200);
    text("+3!", width/2, height/2);
  }
  if (score==6) {
    fill(255);
    textAlign(CENTER);
    textSize(200);
    text("+6!", width/2, height/2);
  }
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
  text("reset", width-250, height-180);
}