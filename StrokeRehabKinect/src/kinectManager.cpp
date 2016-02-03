
#include "KinectManager.h"
void KinectManager::setup()
{
	kinect.initSensor();
	kinect.initColorStream(true);
	kinect.initDepthStream(true);
	kinect.initBodyIndexStream();
	kinect.initSkeletonStream(true);

	//simple start
	kinect.start();
	ofDisableAlphaBlending(); //Kinect alpha channel is default 0;

							  /////////////////// video input arguments////
	camWidth = 1920;	// try to grab at this size. 
	camHeight = 1080;
	//we can now get back a list of devices. 
	vector<ofVideoDevice> devices = vidGrabber.listDevices();

	for (int i = 0; i < devices.size(); i++) {
		cout << devices[i].id << ": " << devices[i].deviceName;
		if (devices[i].bAvailable) {
			cout << endl;
		}
		else {
			cout << " - unavailable " << endl;
		}
	}
	////////////////////////////////////////////////////////////
	vidGrabber.setDeviceID(0);
	vidGrabber.setDesiredFrameRate(40);
	vidGrabber.initGrabber(camWidth, camHeight);

	videoInverted = new unsigned char[camWidth*camHeight * 3];
	videoTexture.allocate(camWidth, camHeight, GL_RGB);
	ofSetVerticalSync(true);

	///////////////// load images//


	ofTrueTypeFont::setGlobalDpi(72);

	//verdana14.loadFont("verdana.ttf", 25, true, true);
	//verdana14.setLineHeight(24.0f);
	//verdana14.setLetterSpacing(1.037);


	verdana14.loadFont("verdana.ttf", 200, true, true);
	verdana14.setLineHeight(190.0f);
	verdana14.setLetterSpacing(1.037);


	verdana30.loadFont("verdana.ttf", 35, true, true);
	verdana30.setLineHeight(34.0f);
	verdana30.setLetterSpacing(1.035);



	synth.loadSound("sounds/synth2.wav");
	synth.setVolume(0.75f);

	ofSetWindowTitle("testPrototype");

	readyforplay = 0;

}

void KinectManager::update()
{
	kinect.update();
//	ofBackground(100, 100, 100);
	//ofSoundUpdate();

	vidGrabber.update();

	if (vidGrabber.isFrameNew()) {
		int totalPixels = camWidth*camHeight * 3;
		unsigned char * pixels = vidGrabber.getPixels();
		for (int i = 0; i < totalPixels; i++) {
			videoInverted[i] = 255 - pixels[i];
		}
		videoTexture.loadData(videoInverted, camWidth, camHeight, GL_RGB);
	}
	vector<Kv2Skeleton> mySkeletons = kinect.getSkeletons();
	{
		int skeleton_count = 0;
		for (int i = 0; i < mySkeletons.size(); i++)
		{
			if (mySkeletons[i].tracked) {

				cout << "Drawing left hand of skeleton " << i << "\n";
				ofVec3f left_hand_pos = mySkeletons[i].joints[JointType_HandLeft].getPosition();
				prevLeftPos = leftPos;
				leftPos = left_hand_pos;
			//	leftPos -= prevLeftPos;
				leftPos.normalize();
				ofVec3f right_hand_pos = mySkeletons[i].joints[JointType_HandRight].getPosition();
				prevRightPos = rightPos;
				rightPos = right_hand_pos;
			//	rightPos -= prevRightPos;
				rightPos.normalize();
				/*		ofVec3f head_pos = mySkeletons[i].joints[JointType_Head].getPosition();
				ofVec3f neck_pos = mySkeletons[i].joints[JointType_Neck].getPosition();
				ofVec3f hipleft_pos = mySkeletons[i].joints[JointType_HipLeft].getPosition();
				ofVec3f spinemid_pos = mySkeletons[i].joints[JointType_SpineMid].getPosition();
				ofVec3f spinebase_pos = mySkeletons[i].joints[JointType_SpineBase].getPosition();
				ofVec3f shoulderleft_pos = mySkeletons[i].joints[JointType_ShoulderLeft].getPosition();
				ofVec3f shoulderright_pos = mySkeletons[i].joints[JointType_ShoulderRight].getPosition();
				ofVec3f elbow_left_pos = mySkeletons[i].joints[JointType_ElbowLeft].getPosition();
				ofVec3f elbow_right_pos = mySkeletons[i].joints[JointType_ElbowRight].getPosition();
				ofVec3f left_knee_pos = mySkeletons[i].joints[JointType_KneeLeft].getPosition();
				ofVec3f right_knee_pos = mySkeletons[i].joints[JointType_KneeRight].getPosition();
				ofVec3f left_elbow_pos = mySkeletons[i].joints[JointType_ElbowLeft].getPosition();
				ofVec3f right_elbow_pos = mySkeletons[i].joints[JointType_ElbowRight].getPosition();

*/
				//cout << "  x: " << left_hand_pos.x << "  y: " << left_hand_pos.y << "  z: " << left_hand_pos.z << "\n";


				/* -------------------------------------------KEEP THIS PART --------------------------------------------
				ofSetColor(0,80,255);

				verdana30.drawString("lHand X    " + ofToString(left_hand_pos.x), ofGetWindowWidth() - 400, 10 + 90 * i);
				verdana30.drawString("lHand Y    " + ofToString(left_hand_pos.y), ofGetWindowWidth() - 400, 35 + 90 * i);
				verdana30.drawString("lHand Z    " + ofToString(left_hand_pos.z), ofGetWindowWidth() - 400, 60 + 90 * i);
				//	verdana30.drawString("lHand Z    " + ofToString(left_knee_pos.z), ofGetWindowWidth() - 400, 75 + 90 * i);

				verdana14.drawString("rHand X    " + ofToString(right_hand_pos.x), ofGetWindowWidth() - 400, 110 + 90 * i);
				verdana14.drawString("rHand Y    " + ofToString(right_hand_pos.y), ofGetWindowWidth() - 400, 135 + 90 * i);
				verdana14.drawString("rHand Z    " + ofToString(right_hand_pos.z), ofGetWindowWidth() - 400, 160 + 90 * i);
				//	verdana30.drawString("rHand Z    " + ofToString(right_knee_pos.z), ofGetWindowWidth() - 400, 250 + 90 * i);


				-------------------------------------------KEEP THIS PART -------------------------------------------- */

				/////////////////////////////////////////////////////////////////////
				//if (left_hand_pos.y>head_pos.y) ofSetColor(255,0,0);
				//else  ofSetColor(0,255, 0);
	/*			ofVec3f left_hand_shoulder = left_hand_pos - shoulderleft_pos;	// this is a vector point from shoulder to hand
				ofVec3f right_hand_shoulder = right_hand_pos - shoulderright_pos;	// this is a vector point from shoulder to hand

				ofVec3f left_elbow_shoulder = elbow_left_pos - shoulderleft_pos;	// this is a vector point from left shoulder to elbow
				ofVec3f right_elbow_shoulder = elbow_right_pos - shoulderright_pos;	// this is a vector point from right shoulder to elbow




				ofVec3f neck_hip = spinebase_pos - neck_pos;	// this is a vector point from neck_pos to spinebase_pos


				ofVec3f right_hand_elbow = right_hand_pos - elbow_right_pos;	// this is a vector point from shoulder to hand
				ofVec3f left_hand_elbow = left_hand_pos - elbow_left_pos;	// this is a vector point from shoulder to hand

				ofVec3f left_shoulder_elbow = shoulderleft_pos - elbow_left_pos;	// this is a vector point from shoulder to hand
				ofVec3f right_shoulder_elbow = shoulderright_pos - elbow_right_pos;	// this is a vector point from shoulder to hand

				ofVec3f left_shoulder_R_L = shoulderright_pos - shoulderleft_pos;	// this is a vector point from shoulder to hand
				ofVec3f right_shoulder_L_R = shoulderleft_pos - shoulderright_pos;	// this is a vector point from shoulder to hand


				float left_angle_hand_shoulder_with_neck_hip = left_hand_shoulder.angle(neck_hip);
				float right_angle_hand_shoulder_with_neck_hip = right_hand_shoulder.angle(neck_hip);

				float left_angle_elbow_shoulder_with_neck_hip = left_elbow_shoulder.angle(neck_hip);
				float right_angle_elbow_shoulder_with_neck_hip = right_elbow_shoulder.angle(neck_hip);


				float left_shoulder_hand_elbow_with_left_hand_pos = left_hand_elbow.angle(left_shoulder_elbow);
				float right_shoulder_hand_elbow_with_right_hand_pos = right_hand_elbow.angle(right_shoulder_elbow);

				float left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos = right_shoulder_L_R.angle(right_hand_elbow);
				float right_shoulder_left_shoulder_hand_elbow_with_left_hand_pos = left_shoulder_R_L.angle(left_hand_elbow);

				/*
				ofEnableAlphaBlending();
				if (left_shoulder_hand_elbow_with_left_hand_pos > 30) {
				newalpha = oldalpha + 1;
				if (newalpha > 255) { newalpha = 255; }
				ofSetColor(255, 255, 255, newalpha);
				oldalpha = newalpha;
				}
				else{ ofSetColor(255, 255, 255, 0); }
				fakeStage.draw(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
				ofDisableAlphaBlending();
				*/

			/*	ofSetColor(255);
				if (left_hand_pos.y>spinebase_pos.y && left_hand_pos.y<spinemid_pos.y && left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos<60 && left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos>30 &&
					right_hand_pos.y>spinebase_pos.y && right_hand_pos.y < spinemid_pos.y && right_shoulder_left_shoulder_hand_elbow_with_left_hand_pos < 60 && right_shoulder_left_shoulder_hand_elbow_with_left_hand_pos>30) {
					ofEnableAlphaBlending();
					float wave = sin(ofGetElapsedTimef());
					//	mCenter.draw(ofGetWidth() / 2 - mCenter.width / 2, wave / 1000 * (ofGetHeight() / 2 - mCenter.height / 2 - 80));
					ofSetColor(10, 200, 133);
					verdana30.drawString("Contrabass!", 950, 400);
				}

				else if (left_hand_pos.y > shoulderleft_pos.y && right_hand_pos.y > shoulderright_pos.y &&left_hand_pos.z < left_knee_pos.z && right_hand_pos.z < left_knee_pos.z && right_shoulder_hand_elbow_with_right_hand_pos > 130 && left_shoulder_hand_elbow_with_left_hand_pos > 130 && left_knee_pos.z < right_knee_pos.z) {
					//ofSetColor(0, 255, 0);
					ofEnableAlphaBlending();
					float wave = sin(ofGetElapsedTimef()*2.0);
					//	mLeft1.draw(500, (wave *  ofGetHeight()*.5));
					ofSetColor(155, 155, 200);
					verdana30.drawString("Activate Musician on the Left", 950, 400);

				}
				//ofSetColor(0, 255, 0);

				else if (left_hand_pos.z > shoulderleft_pos.z && right_hand_pos.z > shoulderright_pos.z &&left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos > 125 && right_shoulder_left_shoulder_hand_elbow_with_left_hand_pos > 125 && left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos < 170 && right_shoulder_left_shoulder_hand_elbow_with_left_hand_pos < 170) {
					ofSetColor(255);
					ofEnableAlphaBlending();
					float wave = sin(ofGetElapsedTimef()*2.0);
					//	mLeft2.draw(500, (wave * ofGetHeight()*.5));
					ofColor color = ofColor::darkSlateBlue;

					ofSetColor(color);

					verdana30.drawString("Activate Musician on the Left edge", 950, 400);


				}
				//else  ofSetColor(0, 255, 0);


				else if (right_hand_pos.y > head_pos.y && left_hand_pos.y < spinebase_pos.y) {
					ofEnableAlphaBlending();
					float wave = sin(ofGetElapsedTimef()*2.0);
					//	mRight1.draw(500, (wave *  ofGetHeight()*.5));

					ofColor color = ofColor::orangeRed;

					ofSetColor(color);

					verdana30.drawString("Activate Musician on the Right", 950, 400);

				}
				else  ofSetColor(255, 255, 255);
				ofDisableAlphaBlending();


				//bool righthand_is_inplace = false;
				//if (right_shoulder_hand_elbow_with_right_hand_pos < 120 && right_shoulder_hand_elbow_with_right_hand_pos>70){
				//	ofSetColor(30, 30, 255);
				//	if (left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos < 110 && left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos>70){
				//		righthand_is_inplace = true;
				//		ofSetColor(255, 130, 0);
				//	}
				//}


				if (showPos_Head) {
					ofSetColor(0, 0, 0);
					verdana30.drawString("Head Pos.X    " + ofToString(head_pos.x), 550, 50 + 90 * i);
					verdana30.drawString("Head Pos.Y    " + ofToString(head_pos.y), 550, 90 + 90 * i);
					verdana30.drawString("Head Pos.Z    " + ofToString(head_pos.z), 550, 130 + 90 * i);

					//verdana30.drawString("Head Pos.Z    " + ofToString(right_shoulder_left_shoulder_hand_elbow_with_left_hand_pos), 550, 130 + 90 * i);

					/*			verdana30.drawString("Head Pos.Z    " + ofToString((int)(left_shoulder_R_L.x*10)), 550, 50 + 90 * i);
					verdana30.drawString("Head Pos.Z    " + ofToString((int)(left_shoulder_R_L.y*10)), 550, 90 + 90 * i);
					verdana30.drawString("Head Pos.Z    " + ofToString((int)(left_shoulder_R_L.z*10)), 550, 130 + 90 * i);*/

				}

				/*
				if (showPos_LHand_Shoulder){
				ofSetColor(125, 120, 200);
				verdana30.drawString("lHand_shoulder.X    " + ofToString((int)(left_hand_shoulder.x * 10)), 550, 50 + 90 * i);
				verdana30.drawString("lHand_shoulder.Y    " + ofToString((int)(left_hand_shoulder.y * 10)), 550, 90 + 90 * i);
				verdana30.drawString("lHand_shoulder.Z    " + ofToString((int)(left_hand_shoulder.z * 10)), 550, 130 + 90 * i);
				}
				*/

				//if (readyforplay == 1) {
				//	synthPlay();
				//	readyforplay = 0;
				//}

/*
				if ((int)(left_hand_shoulder.x * 10) == -5 &&
					left_hand_shoulder.y > 0.00 && left_hand_shoulder.y < 0.01 &&
					(int)(left_hand_shoulder.z * 10) == 0 &&
					left_shoulder_hand_elbow_with_left_hand_pos > 176 && left_shoulder_hand_elbow_with_left_hand_pos < 180) {

					if (readyforplay == 0) {
						synth.setPaused(true);
						readyforplay = 1;
					}

					if (readyforplay == 1) {
						synthPlay();
						readyforplay = 0;
					}

					ofSetColor(0, 0, 0);
					verdana30.drawString("PLAY SOUND", ofGetWidth() / 2, ofGetHeight() / 2);
					readyforplay = 0;
				}

				/*
				//				if (showAngle_Arm_Forearm){
				ofSetColor(125, 0, 200);
				verdana30.drawString("Angle_Arm_lForearm    " + ofToString(left_shoulder_hand_elbow_with_left_hand_pos), 550, 50 + 90 * i);
				//	verdana30.drawString("Angle_Arm_rForearm    " + ofToString(right_shoulder_hand_elbow_with_right_hand_pos), 550, 90 + 90 * i);
				verdana30.drawString("left_hand_shoulder.y    " + ofToString(left_hand_shoulder.y), 550, 90 + 90 * i);
				verdana30.drawString("left_hand_shoulder.x    " + ofToString(left_hand_shoulder.x), 550, 150 + 90 * i);
				//				}
				*/
				/*
				if (showAngle_Arm_Spine) {
					ofSetColor(255, 0, 0);
					verdana30.drawString("lAngle_Arm_Spine    " + ofToString(left_angle_elbow_shoulder_with_neck_hip), 550, 50 + 90 * i);
					verdana30.drawString("rAngle_Arm_Spine    " + ofToString(right_angle_elbow_shoulder_with_neck_hip), 550, 90 + 90 * i);
				}


				////////////////////////////****/////////////////////////


				////////////////////////////****/////////////////////////

			/*	if (abs(neck_hip.x) > 0.04 && abs(neck_hip.x) < 0.09) {
					//fakeStage.draw(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
					ofEnableBlendMode(OF_BLENDMODE_SCREEN);
					ofFill();
					//	ofSetColor(255,0,0);
					ofSetColor(255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500);
					ofRect(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
					ofSetColor(0, 0, 0);

					verdana14.drawString("1", 50, 90 + 90 * i);
					//mOne.draw(ofGetWindowWidth()/2 - 1474 / 4, ofGetWindowHeight()/2 - 1423 / 4, 1474 , 1423);
				}

				if (abs(neck_hip.x) >= 0.09 && abs(neck_hip.x) < 0.15) {
					ofEnableBlendMode(OF_BLENDMODE_SCREEN);
					ofFill();
					ofSetColor(255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500);
					ofRect(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
					//mTwo.draw(ofGetWindowWidth() / 2 - 1474 / 4, ofGetWindowHeight() / 2 - 1423 / 4, 1474, 1423);
					ofSetColor(0, 0, 0);
					verdana14.drawString("2", 50, 90 + 90 * i);
				}

				if (abs(neck_hip.x) >= 0.15 && abs(neck_hip.x) < 0.22) {
					ofEnableBlendMode(OF_BLENDMODE_SCREEN);
					ofFill();
					ofSetColor(255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500);
					ofRect(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
					ofSetColor(0, 0, 0);
					verdana14.drawString("3", 50, 90 + 90 * i);
				}

				if (abs(neck_hip.x) >= 0.22 && abs(neck_hip.x) < 0.30) {
					ofEnableBlendMode(OF_BLENDMODE_SCREEN);
					ofFill();
					ofSetColor(255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500);
					ofRect(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
					//mTwo.draw(ofGetWindowWidth() / 2 - 1474 / 4, ofGetWindowHeight() / 2 - 1423 / 4, 1474, 1423);
					ofSetColor(0, 0, 0);
					verdana14.drawString("4", 50, 90 + 90 * i);
				}

				if (abs(neck_hip.x) >= 0.30 && abs(neck_hip.x) < 0.37) {
					ofEnableBlendMode(OF_BLENDMODE_SCREEN);
					ofFill();
					ofSetColor(255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500, 255 - abs(neck_hip.x) * 500);
					ofRect(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
					ofSetColor(0, 0, 0);
					verdana14.drawString("5", 50, 90 + 90 * i);
				}
				//	if (showPos_Neck_Hip){
				ofSetColor(0, 0, 0);
				verdana30.drawString("Neck_Hip pos.X    " + ofToString(neck_hip.x), 550, 50 + 90 * i);
				verdana30.drawString("Neck_Hip pos.Z    " + ofToString(neck_hip.z), 550, 90 + 90 * i);
				verdana30.drawString("Neck_Hip pos.Z    " + ofToString(abs(neck_hip.x) * 500), 550, 120 + 90 * i);
				//		}


				//verdana30.drawString("Right Sh to H " + ofToString(right_shoulder_hand_elbow_with_right_hand_pos), 800, 175 + 90 * i);
				//verdana30.drawString("Right Sh to H-Side " + ofToString(left_shoulder_right_shoulder_hand_elbow_with_right_hand_pos), 800, 200 + 90 * i);

				/////////////////////////////////////////////////////////////////////


				//			ofDrawBitmapString("Left hand pos.X    " + ofToString(left_hand_pos.x), 1600, 100 + 60 * i);
				//		ofDrawBitmapString("Left hand Pos.Y    " + ofToString(left_hand_pos.y), 1600, 120 + 60 * i);
				//	ofDrawBitmapString("Left hand Pos.Z    " + ofToString(left_hand_pos.z), 1600, 140 + 60 * i);
				
			}
		*/}


		cout << skeleton_count << "\n\n\n";
	}

}
void KinectManager::draw()
{//ofBackground(255);
	//ofSetColor(255);
	/*vidGrabber.draw(20, 20);
	videoTexture.draw(20 + camWidth, 20, camWidth, camHeight);

	vidGrabber.draw(20, 20);
	videoTexture.draw(20 + camWidth, 20, camWidth, camHeight);
	*/

	//kinect.draw(0, 0);
	kinect.drawDepth(0, 0);
	//kinect.drawBodyIndex(500, 0);
	//kinect.drawAllSkeletons(ofVec2f(640,480));
	ofPushMatrix();
	ofTranslate(900, 100);
	//kinect.drawAllSkeletons(ofVec2f(640, 480));
	kinect.drawAllSkeletons(ofVec2f(1200, 900));

	ofPopMatrix();

	float hue = fmodf(ofGetElapsedTimef() * 80, 255);

	int step = 5;
	// step through horizontally



	//ofSetColor(255);

	//---------------------------------- synth:
	if (synth.getIsPlaying()) ofSetHexColor(0xFF0000);
	else ofSetHexColor(0x000000);
	verdana30.drawString("!", 50, 50);

	ofSetHexColor(0x000000);
	string tempStr = "click to play\npct done: " + ofToString(synth.getPosition()) + "\nspeed: " + ofToString(synth.getSpeed()) + "\npan: " + ofToString(synth.getPan());
	ofDrawBitmapString(tempStr, 50, ofGetHeight() - 50);


}

void KinectManager::synthPlay()
{
	synth.play();
	synth.setSpeed(0.1f + ((float)(ofGetHeight()) / (float)ofGetHeight()) * 10);
	synth.setPan(ofMap(100, 0, ofGetWidth() / 3.0f, -1, 1, true));
}

void KinectManager::mousePressed(int x, int y, int button)
{
	synth.play();
	synth.setSpeed(0.1f + ((float)(ofGetHeight() - y) / (float)ofGetHeight()) * 10);
	synth.setPan(ofMap(x, 0, ofGetWidth() / 3.0f, -1, 1, true));

}

void KinectManager::keyPressed(int key)
{
	switch (key) {
	case 49:  // key 1
		showPos_Head = true;
		showPos_LHand_Shoulder = false;
		showAngle_Arm_Forearm = false;
		showAngle_Arm_Spine = false;
		showPos_Neck_Hip = false;
		synth.stop();
		break;

	case 50:  // key 2
		showPos_Head = false;
		showPos_LHand_Shoulder = true;
		showAngle_Arm_Forearm = false;
		showAngle_Arm_Spine = false;
		showPos_Neck_Hip = false;
		synth.play();
		break;

	case 51:  // key 3	
		showPos_Head = false;
		showPos_LHand_Shoulder = false;
		showAngle_Arm_Forearm = true;
		showAngle_Arm_Spine = false;
		showPos_Neck_Hip = false;
		break;

	case 52: // key 4
		showPos_Head = false;
		showPos_LHand_Shoulder = false;
		showAngle_Arm_Forearm = false;
		showAngle_Arm_Spine = true;
		showPos_Neck_Hip = false;
		break;
	case 53:   // key 5
		showPos_Head = false;
		showPos_LHand_Shoulder = false;
		showAngle_Arm_Forearm = false;
		showAngle_Arm_Spine = false;
		showPos_Neck_Hip = true;
		synth.stop();
		break;

	default:
		showPos_Head = false;
		showPos_LHand_Shoulder = true;
		showAngle_Arm_Forearm = false;
		showAngle_Arm_Spine = false;
		showPos_Neck_Hip = false;
		break;
	}
}