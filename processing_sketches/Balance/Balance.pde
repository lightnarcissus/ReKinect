import KinectPV2.KJoint;
import KinectPV2.*;

KinectPV2 kinect;

PFont font1;
PVector bar, bar2;

int prevState, currState;

void setup() {
  size(1920, 1080, P3D);

  kinect = new KinectPV2(this);

  kinect.enableSkeleton3DMap(true);

  kinect.enableColorImg(true);

  //kinect.enableSkeleton3DMap(true);
  
  kinect.init();

  font1 = createFont("Helvetica-Bold", 150);
  
  bar = new PVector (0,0,0);
  bar2 = new PVector (20, 10, 2);
}

void draw() {
  background(0);

//////////////////////////////////////////////////////////////////

  image(kinect.getColorImage(), 0, 0, width, height);

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

      currState = joints[KinectPV2.JointType_HandRight].getState();

    if(prevState == 2 && int(joints[KinectPV2.JointType_HandRight].getY()-joints[KinectPV2.JointType_SpineMid].getY()) < 30){
      if(prevState != currState){
        println("STATE CHANGED!");
        println("  ");
        println("  ");
      }
    }
    
     mapPoint(joints,KinectPV2.JointType_HandLeft);

     
     //println(kinect.MapCameraPointToColorSpace(bar2));
     
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
      //text(stretchAmount, width/2, height/2);

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
        rect(0, 0, width, height);
        fill(0);
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
      text((joints[KinectPV2.JointType_HandRight].getZ()-joints[KinectPV2.JointType_SpineMid].getZ()), width/2, height/2); // < 30
    }
  }
  
  prevState = currState;
  
  //////////////////////////////////////////////////////////////////
  //////////////////////////////////////////////////////////////////
}

void mapPoint(KJoint[] joints, int jointType) {
  pushMatrix();
 
 // println(KinectPV2.MapCameraPointToColorSpace(bar));
 
  bar.x = joints[jointType].getX();
  bar.y = joints[jointType].getY();
  bar.z = joints[jointType].getZ();
 
  PVector newPos = kinect.MapCameraPointToColorSpace(bar);
  //translate(joints[jointType].getX(), joints[jointType].getY(), joints[jointType].getZ());
  println(newPos);
  translate(newPos.x,newPos.y, newPos.z);
  fill(random(255));
  ellipse(0, 0, 50, 50);
  popMatrix();
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
 
 // println(KinectPV2.MapCameraPointToColorSpace(bar));
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