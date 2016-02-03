#pragma once

//#include "ofxKinect.h"
#include "ofMain.h"
#include "ofxKinectCommonBridge.h"
class KinectManager{

public:
	void setup();
	void update();
	void draw();
	void synthPlay();
	void mousePressed(int,int,int);
	void keyPressed(int);
	void sendCoordinates();

	//declaring variable for Kinect
	//ofxKinect kinect;
	ofVec3f prevLeftPos;
	ofVec3f leftPos;
	ofVec3f prevRightPos;
	ofVec3f rightPos;
	ofxKinectCommonBridge kinect;
	ofShader yuvRGBShader;
	ofPlanePrimitive plane;


	//fonts
	ofTrueTypeFont  franklinBook14;
	ofTrueTypeFont	verdana14;
	ofTrueTypeFont	verdana30;
	ofTrueTypeFont	verdana60;

	bool bFirst;
	string typeStr;
	float hue = fmodf(ofGetElapsedTimef() * 80, 255);
	//// adding videoGrabber camera input///////////////////
	ofVideoGrabber 		vidGrabber;
	unsigned char * 	videoInverted;
	ofTexture			videoTexture;
	int 				camWidth;
	int 				camHeight;

	int oldalpha;
	int newalpha;

	bool raisingArms;

	bool showPos_Head = false;
	bool showPos_LHand_Shoulder = false;
	bool showAngle_Arm_Forearm = false;
	bool showAngle_Arm_Spine = false;
	bool showPos_Neck_Hip = false;

	ofSoundPlayer  synth;


	int readyforplay;
};