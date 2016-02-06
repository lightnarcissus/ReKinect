import KinectPV2.KJoint; //<>//
import KinectPV2.*;

KinectPV2 kinect;

float zVal = 300;
float rotX = PI;

PFont font1;
PVector bar;
PVector[] newPos;

int prevState, currState;
int timer;

float rx, ry, rw, rh;
float[] rxx;

float stretchAmount, stretchThreshold;
float dist;

//
boolean grabOn = false;

void setup() {
  size(1920, 1080, P3D);

  kinect = new KinectPV2(this);

  kinect.enableSkeleton3DMap(true);

  //kinect.enableSkeletonColorMap(true);
  
  kinect.enableColorImg(true);

  kinect.init();

  font1 = createFont("Helvetica-Bold", 150);

  bar = new PVector (0, 0, 0);

  rxx = new float[4];

  rx = width/2;
  ry = height/2;
  rw = 200;
  rh = 200;

  timer = 0;
  stretchThreshold = 0.2;
  newPos = new PVector[2];
  for(int i = 0; i < 2; i++){ newPos[i] = new PVector(0,0,0); }
  
  textAlign(CENTER);
}

void draw() {
  background(0);

  image(kinect.getColorImage(), 0, 0, width, height);

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
      //drawBody(joints);

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
   
     // stretchAmount = joints[8].getZ()-joints[11].getZ(); // SholderRight - HandRight  
      stretchAmount = sqrt( sq(joints[8].getX()-joints[11].getX()) + sq(joints[8].getY()-joints[11].getY()) + sq(joints[8].getZ()-joints[11].getZ()) );
      
      fill(255, 0, 0);
      textSize(100);
      text("Z: " + stretchAmount, width/2, height-300);
     // text("D: " + dist, width/2, height-180);

      //if (stretchAmount > stretchThreshold) {
      //  textSize(200);
      //  text("GRAB!", width/2, height/2);
      //  mapPoint(joints, KinectPV2.JointType_HandRight);
      //}
      /////////////////////////////////////////////////////
      //   pushMatrix();
      //   translate(0,0,20);
   
      drawBone(joints, KinectPV2.JointType_SpineShoulder, KinectPV2.JointType_SpineMid);
      drawBone(joints, KinectPV2.JointType_SpineMid, KinectPV2.JointType_SpineBase);

      drawJoint(joints, KinectPV2.JointType_SpineShoulder);
      drawJoint(joints, KinectPV2.JointType_SpineMid);
      drawJoint(joints, KinectPV2.JointType_SpineBase);

      drawJoint(joints, KinectPV2.JointType_HipRight);
      drawJoint(joints, KinectPV2.JointType_HipLeft);

      drawHandState(joints[KinectPV2.JointType_HandRight]);
      //drawHandState(joints[KinectPV2.JointType_HandLeft]);
      //handState(joints[KinectPV2.JointType_HandRight].getState());

      //fill(255, 0, 255);
      //textSize(150);
      ////text(joints[KinectPV2.JointType_HandRight].getState(), width/2, height/2);
      //text((joints[KinectPV2.JointType_HandRight].getZ()-joints[KinectPV2.JointType_SpineMid].getZ()), width/2, height/2); // < 30


      //OBJECT TO MOVE
      mapPoint(joints, KinectPV2.JointType_HandRight);
      
      //mapPoint(joints, KinectPV2.JointType_HandLeft);
      
      fill(0, 0, 255);

      if (stretchAmount > stretchThreshold) {
        fill(0,255,0);
        textSize(200);
        text("GRAB!", width/2, height/2);
       
        if (newPos[0].x > rx-rw/2 && newPos[0].x < rx+rw/2 && newPos[0].y > ry-rh/2 && newPos[0].y < ry+rh/2 ) {
          /*
          if(timer<=20){
            fill(255);
            textSize(50);
            text("1", newPos.x, newPos.y-50);
          }else if(timer<=40){
            fill(255);
            textSize(50);
            text("2", newPos.x, newPos.y-50);
          }else if(timer<=60){
            fill(255);
            textSize(50);
            text("3", newPos.x, newPos.y-50);
          } else {                        
          */  
       //   if (timer > 60) {
            if(grabOn){
            rx = newPos[0].x;
            ry = newPos[0].y;
            stroke(255);
            strokeWeight(10);
          }
          
          textSize(250);
          fill(255, 255, 0);
          text("HIT"+timer, width/2, height/2-300);
         // text(timer, width/2 + 400, height/2-300);
          timer++;
          
        } else {
          timer = 0;
          fill(0, 255, 0);
          noStroke();
        }
  
    } else {
        timer = 0;
      }
    }
  }
  
  pushMatrix();
  translate(0, 0, 0);
  rectMode(CENTER);
  rect(rx, ry, rw, rh);
  popMatrix();
  
  pushMatrix();
  //translate(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());
  translate(newPos[0].x, newPos[0].y, newPos[0].z);
  fill(0);
  noStroke();
  ellipse(0, 0, 50, 50);
  popMatrix();
  
  prevState = currState;

  //////////////////////////////////////////////////////////////////
  //////////////////////////////////////////////////////////////////

}

void mapPoint(KJoint[] joints, int jointType) {
  
  //bar.x = joints[jointType].getX();
  //bar.y = joints[jointType].getY();
  //bar.z = joints[jointType].getZ();
  bar = new PVector(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());

  //if(jointType == 11){
  newPos[0] = kinect.MapCameraPointToColorSpace(bar);
  //}
  //if(jointType == 7){
  //newPos[1] = kinect.MapCameraPointToColorSpace(bar);
  //}
  
 
  //pushMatrix();
  ////translate(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());
  //translate(newPos.x, newPos.y, newPos.z);
  //fill(random(255));
  //noStroke();
  //ellipse(0, 0, 50, 50);
  //popMatrix();
}