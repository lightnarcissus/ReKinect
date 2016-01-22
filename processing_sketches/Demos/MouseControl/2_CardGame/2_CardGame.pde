import KinectPV2.KJoint;
import KinectPV2.*;

KinectPV2 kinect;

PFont font1;

int prevState, currState;

////////////////////////
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

void setup() {
  smooth();
  size(displayWidth, displayHeight,P3D);
  startTime = millis();
  
  kinect = new KinectPV2(this);

  //kinect.enableSkeletonColorMap(true);
  kinect.enableSkeleton3DMap(true);
  
  kinect.enableColorImg(true);

  kinect.init();

  font1 = createFont("Helvetica-Bold", 150);
  
  
  initcards();
}


void draw() { 
  fill(20,40);
  rectMode(CENTER);
  noStroke();
  rect(width/2,height/2,width,height);
 translate(0,0,0);
  //////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
  //image(kinect.getColorImage(), 0, 0, width, height);

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

      currState = joints[KinectPV2.JointType_HandRight].getState();

    if(prevState == 2 && int(joints[KinectPV2.JointType_HandRight].getY()-joints[KinectPV2.JointType_SpineMid].getY()) < 30){
      if(prevState != currState){
        println("STATE CHANGED!");
        println("  ");
        println("  ");
      }
    }
   
      drawBone(joints, KinectPV2.JointType_SpineShoulder, KinectPV2.JointType_SpineMid);
      drawBone(joints, KinectPV2.JointType_SpineMid, KinectPV2.JointType_SpineBase);

      drawJoint(joints, KinectPV2.JointType_SpineShoulder);
      drawJoint(joints, KinectPV2.JointType_SpineMid);
      drawJoint(joints, KinectPV2.JointType_SpineBase);

      drawJoint(joints, KinectPV2.JointType_HipRight);
      drawJoint(joints, KinectPV2.JointType_HipLeft);

      int tiltAmount = int(joints[KinectPV2.JointType_SpineShoulder].getX() - joints[KinectPV2.JointType_SpineBase].getX());

      float stretchAmount = joints[KinectPV2.JointType_HipRight].getZ();
      textSize(100);
     // text(stretchAmount, width/2, height/2);

      //if (abs(tiltAmount)<40 && abs(tiltAmount)>=0) { 
      //  return;
      //} 

      if ((abs(tiltAmount)<110 && abs(tiltAmount)>=40)) { 
        //fill(255,50,0,100);
        //rect(0,0,width,height);
        fill(0);
        textAlign(CENTER);
        textSize(150);
        text("Correct Your Balance", width/2, height/2);
      }
      if (abs(tiltAmount)>=110) { 
        fill(255, 0, 0, 200);
        rect(width/2, height/2, width, height);
        fill(255);
        textAlign(CENTER);
        textSize(150);
        text("Correct Your Balance", width/2, height/2);
      } 

      drawHandState(joints[KinectPV2.JointType_HandRight]);
      drawHandState(joints[KinectPV2.JointType_HandLeft]);
      //handState(joints[KinectPV2.JointType_HandRight].getState());

      textAlign(CENTER);
      fill(255, 0, 255);
      textSize(150);
      //text(joints[KinectPV2.JointType_HandRight].getState(), width/2, height/2);
     // text(int(joints[KinectPV2.JointType_HandRight].getY()-joints[KinectPV2.JointType_SpineMid].getY()), width/2, height/2); // < 30
    }
  }
  
  prevState = currState;
  
  //////////////////////////////////////////////////////////////////
  //////////////////////////////////////////////////////////////////

  for (int i = 0; i < pics.length; i++)
  { pics[i].drawsneaky(); }
  for (int i = 0; i < pics2.length; i++)
  { pics2[i].drawsneaky(); }
  
  int elapsed = millis() - startTime;
  //println(int(elapsed) / 1000 + " seconds elapsed");
//  textSize(45);
//  text("score:", width-300, 150);
//  text(match, width-160, 150);
//
//  text("time:", width-300, height-100);
//  text(int(elapsed) / 1000, width-160, height-100);
} 

void initcards()
{
  int[] order = new int[picnum];
  
  order = shuffle(picnum);
  //println(order);

   pics = new Pic[picnum];
   pics2 = new Pic[picnum];
  
for (int i=0; i<pics.length; i++) {
    int x = i%2 * (width/6) + width/3;
    int y = i/2 * (height/6) + b;
    pics[i] = new Pic(x, y, order[i], c1, c2, c3);
  }

  order = shuffle(picnum);
  for (int i=0; i<pics2.length; i++) {
    int x = i%2 * (width/6) + width/3 + 140;
    int y = i/2 * (height/6)+b;
    pics2[i] = new Pic(x, y, order[i], c1, c2, c3);
  }
}

void keyPressed()
{
  if(key == '1'){
   picnum = 4;
   b=350;

   
   initcards();
  }
  if(key == '2'){
   picnum = 6;
   b=250;
   
   initcards();
  }
  if(key == '3'){
   picnum = 10;
   
   b=70;
   initcards();
  }

}

void mousePressed()
{
  whichoneamimoving = -1;
  
  for (int j = pics2.length-1; j>=0; j--)
  { 
    if (mouseX>=pics2[j].locationX&&mouseX<(pics2[j].locationX+picsize)&&mouseY>=pics2[j].locationY&&mouseY<(pics2[j].locationY+picsize))
    {

      num=j;
      whichoneamimoving = j;
      pics2[j].tint=true;
      pics2[j].col=255;
      pics2[j].col2=215;
      pics2[j].col3=0;

      //println(pics2[j].col + ""+ "COL!!!");  
      break;
    }
  }

  if (whichoneamimoving==-1) {

    for (int i = pics.length-1; i>=0; i--)
    {
      if (mouseX>=pics[i].locationX&&mouseX<(pics[i].locationX+picsize)&&mouseY>=pics[i].locationY&&mouseY<(pics[i].locationY+picsize))
      {
        whichoneamimoving = i+pics.length;
        num=i;
        pics[i].tint=true;
        pics[i].col=255;
        pics[i].col2=215;
        pics[i].col3=0;

        break;
      }
    }
  }
}

void mouseDragged()
{
  if (whichoneamimoving>-1&&whichoneamimoving<pics.length)
  {
    pics2[whichoneamimoving].locationX+=(mouseX-pmouseX);
    pics2[whichoneamimoving].locationY+=(mouseY-pmouseY);
  } else if (whichoneamimoving>-1&&whichoneamimoving>=pics.length)
  {
    pics[whichoneamimoving-pics.length].locationX+=(mouseX-pmouseX);
    pics[whichoneamimoving-pics.length].locationY+=(mouseY-pmouseY);
  }
}

void mouseReleased()
{
  whichoneamimoving = -1;
  checkcollisions();
  pics[num].tint=false;
  pics2[num].tint=false;
}

void checkcollisions()
{
  match = 0;
  //println(match +""+"match!!!");
  //println("checking collisions...");
  for (int i = 0; i<pics.length; i++)
  {
    pics[i].match=0;
    pics2[i].match=0; // assume they're not touching yet
  }
  for (int i = 0; i<pics.length; i++)
  {
    for (int j = 0; j<pics2.length; j++)
    {
      if (pics[i].whichcard==pics2[j].whichcard) {
        if ((pics[i].locationX+picsize)>=pics2[j].locationX&&pics[i].locationX<(pics2[j].locationX+picsize)&&(pics[i].locationY+picsize)>=pics2[j].locationY&&pics[i].locationY<(pics2[j].locationY+picsize))
        {
          //println(i + " is touching " + j);
          pics[i].match=1;
          pics2[j].match=1;
          match++;
        }
      }
    }
  }
  if (match==pics.length) {

    //draw();
    //keyReleased();
    startTime = millis();
    //println("done!!");
  }
}

//DRAW BODY
void drawBody(KJoint[] joints) {
  drawBone(joints, KinectPV2.JointType_Head, KinectPV2.JointType_Neck);
  drawBone(joints, KinectPV2.JointType_Neck, KinectPV2.JointType_SpineShoulder);
  drawBone(joints, KinectPV2.JointType_SpineShoulder, KinectPV2.JointType_SpineMid);
  drawBone(joints, KinectPV2.JointType_SpineMid, KinectPV2.JointType_SpineBase);
  drawBone(joints, KinectPV2.JointType_SpineShoulder, KinectPV2.JointType_ShoulderRight);
  drawBone(joints, KinectPV2.JointType_SpineShoulder, KinectPV2.JointType_ShoulderLeft);
  drawBone(joints, KinectPV2.JointType_SpineBase, KinectPV2.JointType_HipRight);
  drawBone(joints, KinectPV2.JointType_SpineBase, KinectPV2.JointType_HipLeft);

  // Right Arm
  drawBone(joints, KinectPV2.JointType_ShoulderRight, KinectPV2.JointType_ElbowRight);
  drawBone(joints, KinectPV2.JointType_ElbowRight, KinectPV2.JointType_WristRight);
  drawBone(joints, KinectPV2.JointType_WristRight, KinectPV2.JointType_HandRight);
  drawBone(joints, KinectPV2.JointType_HandRight, KinectPV2.JointType_HandTipRight);
  drawBone(joints, KinectPV2.JointType_WristRight, KinectPV2.JointType_ThumbRight);

  // Left Arm
  drawBone(joints, KinectPV2.JointType_ShoulderLeft, KinectPV2.JointType_ElbowLeft);
  drawBone(joints, KinectPV2.JointType_ElbowLeft, KinectPV2.JointType_WristLeft);
  drawBone(joints, KinectPV2.JointType_WristLeft, KinectPV2.JointType_HandLeft);
  drawBone(joints, KinectPV2.JointType_HandLeft, KinectPV2.JointType_HandTipLeft);
  drawBone(joints, KinectPV2.JointType_WristLeft, KinectPV2.JointType_ThumbLeft);

  // Right Leg
  drawBone(joints, KinectPV2.JointType_HipRight, KinectPV2.JointType_KneeRight);
  drawBone(joints, KinectPV2.JointType_KneeRight, KinectPV2.JointType_AnkleRight);
  drawBone(joints, KinectPV2.JointType_AnkleRight, KinectPV2.JointType_FootRight);

  // Left Leg
  drawBone(joints, KinectPV2.JointType_HipLeft, KinectPV2.JointType_KneeLeft);
  drawBone(joints, KinectPV2.JointType_KneeLeft, KinectPV2.JointType_AnkleLeft);
  drawBone(joints, KinectPV2.JointType_AnkleLeft, KinectPV2.JointType_FootLeft);

  drawJoint(joints, KinectPV2.JointType_HandTipLeft);
  drawJoint(joints, KinectPV2.JointType_HandTipRight);
  drawJoint(joints, KinectPV2.JointType_FootLeft);
  drawJoint(joints, KinectPV2.JointType_FootRight);

  drawJoint(joints, KinectPV2.JointType_ThumbLeft);
  drawJoint(joints, KinectPV2.JointType_ThumbRight);

  drawJoint(joints, KinectPV2.JointType_Head);
}

//draw joint
void drawJoint(KJoint[] joints, int jointType) {
  pushMatrix();
  translate(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());
  ellipse(0, 0, 25, 25);
  popMatrix();
}

//draw bone
void drawBone(KJoint[] joints, int jointType1, int jointType2) {
  pushMatrix();
  translate(joints[jointType1].getX(), joints[jointType1].getY(), joints[jointType1].getZ());
  ellipse(0, 0, 25, 25);
  popMatrix();
  line(joints[jointType1].getX(), joints[jointType1].getY(), joints[jointType1].getZ(), joints[jointType2].getX(), joints[jointType2].getY(), joints[jointType2].getZ());
}

//draw hand state
void drawHandState(KJoint joint) {
  noStroke();
  handState(joint.getState());
  pushMatrix();
  translate(joint.getX(), joint.getY(), joint.getZ());
  ellipse(0, 0, 20, 20);
  popMatrix();
}

void handState(int handState) {
  switch(handState) {
  case KinectPV2.HandState_Open: // 2
    fill(255, 0, 0);
    break;

  case KinectPV2.HandState_Closed:  // 3
    fill(255, 255, 102);
    
    break;

  case KinectPV2.HandState_Lasso:  // 4
    fill(0, 0, 255);
    break;
  case KinectPV2.HandState_NotTracked: // 0
    fill(255, 255, 255);
    break;
  }
}