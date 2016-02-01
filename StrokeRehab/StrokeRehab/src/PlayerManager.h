#pragma once
#include "ofMain.h"
#include "ofEvents.h"
class PlayerManager {
public:
	PlayerManager();
	void init();
	void drawTitlePage();
	void drawInputText();
	void drawCursor() const;
	void drawAppSelectionPage();
	void musicConductorPage();
	void drawingChallengePage(int,int,int,int);
	void drawCircleTargets(int,int);
	void drawSquareTargets(int,int);
	int currentDrawingLevel = 2;
	vector<bool> fillCircles;
	vector<ofVec2f> targetPoints;
	void matrixMatchingPage();


	void keyPressed(int key);
	void mouseEvent(int,int, int);
	void clear();
	bool rightSideActivated = false; // if Right Side selected
	
	int hitTarget = 0;
	int missTarget = 0;
	string clientName;
	int position; //text field cursor position
	int activeApp;
	float debugFloat;
	string debugString;
	int vertexCount = 0;
	vector<ofVec2f> drawPoints;
	int nextTarget = 0;
	bool enableFill = false;
	ofTrueTypeFont miscFont; //font for normal UI text
	ofTrueTypeFont actionFont; //font for action buttons like Continue and Back
	ofTrueTypeFont textFont; //font for input text field
	ofTrueTypeFont titleFont; // font for title

	ofEvent<int>launchApp;
	ofEvent<void> eventEnter; // on press Enter, initiate Selection Screen

	//drawing challenge variables
	ofMesh mesh;
	ofPath path;
	ofPolyline b;
	vector<ofPoint> movementPoints;



protected:
	void keyPressedEvent(ofKeyEventArgs &a); //checks events for key presses for text input
	int cursorX, cursorY, appWidth, appHeight;

};