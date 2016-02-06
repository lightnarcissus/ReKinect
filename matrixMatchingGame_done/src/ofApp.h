#pragma once

#include "ofMain.h"
#include "ofxKinect.h"

#include "Pic.h"
#include "ShuffleManager.h"

class ofApp : public ofBaseApp{

	public:
		void setup();
		void update();
		void draw();
        void initCards();
        void checkcollisions();

		void keyPressed(int key);
		void keyReleased(int key);
		void mouseMoved(int x, int y );
		void mouseDragged(int x, int y, int button);
		void mousePressed(int x, int y, int button);
		void mouseReleased(int x, int y, int button);
		void mouseEntered(int x, int y);
		void mouseExited(int x, int y);
		void windowResized(int w, int h);
		void dragEvent(ofDragInfo dragInfo);
		void gotMessage(ofMessage msg);
    
        //declaring variable for Kinect
        ofxKinect kinect;
    
    
    int picnum = 4; //4, 6
    //int a=3;
    int b=250;
    
    vector<Pic*> pics;
    vector<Pic*> pics2;
    
    ShuffleManager shuffleIt;
    
    int startTime;
    int x;
    int f;
    int picsize = 100;
    int whichoneamimoving = -1;
    int match=0;
    float x1, y1, x2, y2=0;
    int c1, c2, c3;
    int num = 0;
    bool init1=false;
};



