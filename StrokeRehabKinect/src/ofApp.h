#pragma once
#include "ofMain.h"

#include "ofxGui.h"
#include "PlayerManager.h"

class ofApp : public ofBaseApp{

	public:
		void setup();
		void update();
		void draw();

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

		PlayerManager player;

		vector<string> textEntries; //for future use;keeps track of text input entries
		void addText(string &s); // adds input text
		void drawEntries();
		void launchSelectedApp(int &i);
		void activateSelectionScreen(); // moves player to Selection Screen		
		int appState = 0; //tracks state of app; by default 0 --> Start Menu		

		//for debugging purposes
		ofxPanel gui;
		ofParameter<float> distance1;
		ofParameter<float>distance2;
		ofParameter<float>distance3;
		ofParameter<float>distance4;
};
