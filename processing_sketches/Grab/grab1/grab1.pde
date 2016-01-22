import KinectPV2.KJoint; //<>//
import KinectPV2.*;

KinectPV2 kinect;

PFont font1;
PVector bar;
PVector[] newPos = new PVector[2];

int prevState, currState;
int timer;
int[] timers = new int[2];

float rx, ry, rw, rh;
float[] rxx = new float[4];
float[] ryy = new float[4];

float stretchAmount, stretchThreshold;
float[] stretchAmountArray = new float[2];
float dist;

boolean RightOn, LeftOn;

void setup() {
  size(1920, 1080, P3D);

  kinect = new KinectPV2(this);

  kinect.enableSkeleton3DMap(true);

  //kinect.enableSkeletonColorMap(true);

  kinect.enableColorImg(true);

  kinect.init();

  font1 = createFont("Helvetica-Bold", 150);

  bar = new PVector (0, 0, 0);

  rx = width/2;
  ry = height/2;
  rw = 200;
  rh = 200;
 
  rxx = new float[] {50+rw/2, 50+rw/2+width/4, 50+rw/2+width/2, 50+rw/2+3*width/4};
  ryy = new float[] {height/2+random(0,200), height/2+random(0,200), height/2+random(0,200), height/2+random(0,200)};
  timer = 0;
  stretchThreshold = 0.4;

  for (int i = 0; i < 2; i++) { 
    newPos[i] = new PVector(0, 0, 0);
    stretchAmountArray[i] = i;
    timers[i] = 0;
  }

  textAlign(CENTER);
    rectMode(CENTER);

  RightOn = true;
  LeftOn = false;
}

void draw() {
  background(50);

 image(kinect.getColorImage(), 0, 0, width, height);
 fill(0,200);
 rect(width/2,height/2,width, height);

  buttons();

  ArrayList<KSkeleton> skeletonArray =  kinect.getSkeleton3d();

  //individual JOINTS
  for (int i = 0; i < skeletonArray.size(); i++) {
    KSkeleton skeleton = (KSkeleton) skeletonArray.get(i);
    if (skeleton.isTracked()) {
      KJoint[] joints = skeleton.getJoints();
      color col  = skeleton.getIndexColor();
      fill(col);
      stroke(col);

      //----------------------------------------------------------------------Draw Skeleton
      //drawBody(joints);
      //   pushMatrix();
      //   translate(0,0,20);

      drawBone(joints, KinectPV2.JointType_SpineShoulder, KinectPV2.JointType_SpineMid);
      drawBone(joints, KinectPV2.JointType_SpineMid, KinectPV2.JointType_SpineBase);

      //drawJoint(joints, KinectPV2.JointType_SpineShoulder);
      //drawJoint(joints, KinectPV2.JointType_SpineMid);
      //drawJoint(joints, KinectPV2.JointType_SpineBase);

      //drawJoint(joints, KinectPV2.JointType_HipRight);
      //drawJoint(joints, KinectPV2.JointType_HipLeft);


      drawHandState(joints[KinectPV2.JointType_HandRight]);
      drawHandState(joints[KinectPV2.JointType_HandLeft]);
      //handState(joints[KinectPV2.JointType_HandRight].getState());

      //----------------------------------------------------------------------State Change
      currState = joints[KinectPV2.JointType_HandRight].getState();

      if (prevState == 2 && int(joints[KinectPV2.JointType_HandRight].getY()-joints[KinectPV2.JointType_SpineMid].getY()) < 30) {
        if (prevState != currState) {
          println("STATE CHANGED!");
          println("  ");
          println("  ");
        }
      }

      int tiltAmount = int(joints[KinectPV2.JointType_SpineShoulder].getX() - joints[KinectPV2.JointType_SpineBase].getX());

      if ((abs(tiltAmount)<110 && abs(tiltAmount)>=40)) { 
        //fill(255,50,0,100);
        //rect(0,0,width,height);
        fill(0);
        textSize(150);
        text("Correct Your Balance", width/2, height/2);
      }
      if (abs(tiltAmount)>=110) { 
        fill(255, 0, 0, 200);
        rect(0, 0, width, height);
        fill(0);
        textSize(150);
        text("Correct Your Balance", width/2, height/2);
      } 

      //----------------------------------------------------------------------Arm Stretch To Grab   
      //WHICHSIDE
      if (RightOn && LeftOn == false) {
        mapPoint(joints, KinectPV2.JointType_HandRight);
        // stretchAmount = joints[8].getZ()-joints[11].getZ(); // SholderRight - HandRight  
        // stretchAmount = sqrt( sq(joints[8].getX()-joints[11].getX()) + sq(joints[8].getY()-joints[11].getY()) + sq(joints[8].getZ()-joints[11].getZ()) );
      }
      if (LeftOn && RightOn == false) {
        mapPoint(joints, KinectPV2.JointType_HandLeft);
      }

      if(RightOn && LeftOn){ 
        mapPoint2(joints, KinectPV2.JointType_HandRight, KinectPV2.JointType_HandLeft);
      }
    }
  }

  pushMatrix();
  translate(0, 0, 0);
  rect(rx, ry, rw, rh);
  popMatrix();
  
  for(int o = 0; o <4; o++){
  pushMatrix();
  translate(0, 0, 0);
  rect(rxx[o], ryy[o], rw, rh);
  popMatrix();
  }
  

if(RightOn&&LeftOn){
  fill(0);
  noStroke();
  
  for(int l = 0; l < 2; l++){
  pushMatrix();
  translate(newPos[l].x, newPos[l].y, newPos[l].z);
  ellipse(0, 0, 35, 35);
  popMatrix();
  }
  } 
else{
  pushMatrix();
  fill(0);
  noStroke();
  //translate(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());
  translate(newPos[0].x, newPos[0].y, newPos[0].z);
  ellipse(0, 0, 35, 35);
  popMatrix();
}

  prevState = currState;

  //////////////////////////////////////////////////////////////////
  //////////////////////////////////////////////////////////////////
}

void mapPoint(KJoint[] joints, int jointType) {

  bar = new PVector(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());
  newPos[0] = kinect.MapCameraPointToColorSpace(bar);
  stretchAmount = sqrt( sq(joints[jointType-3].getX()-joints[jointType].getX()) + sq(joints[jointType-3].getY()-joints[jointType].getY()) + sq(joints[jointType-3].getZ()-joints[jointType].getZ()) );

  //fill(255, 0, 0);
  //textSize(100);
  //text("Z: " + stretchAmount, width/2, height-300);

  fill(0, 0, 255);

  if (stretchAmount > stretchThreshold) {
    //fill(0, 255, 0);
    //textSize(200);
    //text("GRAB!", width/2, height/2);

    if (newPos[0].x > rx-rw/2 && newPos[0].x < rx+rw/2 && newPos[0].y > ry-rh/2 && newPos[0].y < ry+rh/2 ) {
      timer++;
     
     if(timer <= 20){
     fill(255);
     textSize(25);
     text("1",rx, ry - rh/2-15);
     } else if(timer <= 40){
     fill(255);
     textSize(25);
     text("2",rx, ry - rh/2-15);
     } else if(timer <= 60){
     fill(255);
     textSize(25);
     text("3",rx, ry - rh/2-15);
     }
      else if (timer > 60) {
        rx = newPos[0].x;
        ry = newPos[0].y;
       // fill(92,92,255);
        stroke(255);
        strokeWeight(10);
      }
     
     fill(92,92,255);
     
      //textSize(250);
      //fill(255, 255, 0);
      //text("HIT"+timer, width/2, height/2-300);
    } else {
      timer = 0;
      fill(0, 255, 0);
      noStroke();
    }
  } else {
    timer = 0;
    fill(0, 0, 255);
    noStroke();
  }
}

void mapPoint2(KJoint[] joints, int jointType1, int jointType2) {

  PVector bar1 = new PVector(joints[jointType1].getX(), joints[jointType1].getY(), joints[jointType1].getZ());
  PVector bar2 = new PVector(joints[jointType2].getX(), joints[jointType2].getY(), joints[jointType2].getZ()); 

  newPos[0] = kinect.MapCameraPointToColorSpace(bar1);
  newPos[1] = kinect.MapCameraPointToColorSpace(bar2);

  stretchAmountArray[0] = sqrt( sq(joints[jointType1-3].getX()-joints[jointType1].getX()) + sq(joints[jointType1-3].getY()-joints[jointType1].getY()) + sq(joints[jointType1-3].getZ()-joints[jointType1].getZ()) );
  stretchAmountArray[1] = sqrt( sq(joints[jointType2-3].getX()-joints[jointType2].getX()) + sq(joints[jointType2-3].getY()-joints[jointType2].getY()) + sq(joints[jointType2-3].getZ()-joints[jointType2].getZ()) );

  for (int m = 0; m < 2; m ++) {
    for(int n = 0; n < 4; n ++){
   if (stretchAmountArray[m]>stretchThreshold && newPos[m].x > rxx[n]-rw/2 && newPos[m].x < rxx[n]+rw/2 && newPos[m].y > ryy[n]-rh/2 && newPos[m].y < ryy[n]+rh/2) {

     timers[m]++;

     if(timers[m] <= 20){
     fill(255);
     textSize(25);
     text("1",rxx[n], ryy[n] - rh/2-15);
     } else if(timers[m] <= 40){
     fill(255);
     textSize(25);
     text("2",rxx[n], ryy[n] - rh/2-15);
     } else if(timers[m] <= 60){
     fill(255);
     textSize(25);
     text("3",rxx[n], ryy[n] - rh/2-15);
     }
     else if (timers[m] > 60) {
       rxx[n] = newPos[m].x;
       ryy[n] = newPos[m].y;
     //  fill(92,92,255);
       stroke(255);
       strokeWeight(10);
     }
     fill(92,92,255);
   } else {
     timers[m] = 0;
     fill(0, 0, 255);
   }
  }
  }
}