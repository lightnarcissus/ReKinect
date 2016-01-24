#pragma once

#include "ofxKinect.h"
#include "ofMain.h"
class KinectManager{

public:
	void setup();
	void draw();

	//declaring variable for Kinect
	ofxKinect kinect;
};