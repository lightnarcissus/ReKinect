#pragma once
#include "ofMain.h"
#include "ofEvents.h"
#include "Pic.h"
#include "ShuffleManager.h"

class PlayerManager {
public:
	PlayerManager();
	void init();
	void drawTitlePage();
	void drawInputText();
	void drawCursor() const;
	void drawAppSelectionPage();
	void musicConductorPage(float,float);
	void drawingChallengePage(float,float,int,int);
	void drawCircleTargets(float, float);
	void drawSquareTargets(float, float);
	void drawHexagonTargets(float, float);
	void drawInvisibleOctagonTargets(float, float);
	//shuffle functions
	void initCards();
	void checkCollisions();

	int isPlaying = 0;

	int currentDrawingLevel = 3;
	vector<bool> fillCircles;
	vector<ofVec2f> targetPoints;
	void matrixMatchingPage();

	float lastResetTime = 0;
	float currentTimer = 0;

	void keyPressed(int key);
	void mouseDragged(int,int, int);
	void mouseEvent(int,int,int);
	void mouseReleased(int, int, int); //TODO: combine released and dragged to mouseEvent
	void clear();
	bool rightSideActivated = false; // if Right Side selected
	int musicActivated = 0;
	ofSoundPlayer music;
	ofSoundPlayer music2;
	ostringstream stringParser;
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
	ofVec2f circleTarget1;
	ofVec2f circleTarget2;
	ofVec2f circleTarget3;
	ofVec2f circleTarget4;
	ofVec2f circleTarget5;
	ofVec2f circleTarget6;
	ofVec2f circleTarget7;
	ofVec2f circleTarget8;
	//conductor images
	ofImage violinist;
	ofImage contrabass;

	ofImage background;
	ofImage fakeStage;

	ofImage mCenter;
	ofImage mLeft1;
	ofImage mLeft2;
	ofImage mRight1;
	ofImage mRight2;

	ofImage orchestraBg;


	ofImage mOne;
	ofImage mTwo;
	ofImage mThree;

	//shuffle application variables
	int picnum = 4; //4, 6
					//int a=3;
	int b_shuffle = 250;

	vector<Pic*> pics;
	vector<Pic*> pics2;

	ShuffleManager shuffleIt;

	int startTime;
	int x;
	int f;
	int picsize = 100;
	int whichoneamimoving = -1;
	int match = 0;
	float x1, y1, x2, y2 = 0;
	int c1, c2, c3;
	int num = 0;
	bool init1 = false;


	ofImage orchestraActive1;
	ofImage orchestraActive2;
	ofImage orchestraActive3;


protected:
	void keyPressedEvent(ofKeyEventArgs &a); //checks events for key presses for text input
	int cursorX, cursorY, appWidth, appHeight;

};