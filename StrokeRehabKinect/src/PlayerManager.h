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

	void keyPressed(int key);
	void mouseEvent(int,int, int);
	void clear();
	bool rightSideActivated = false; // if Right Side selected

	string clientName;
	int position; //text field cursor position
	int activeApp;
	float debugFloat;
	string debugString;


	ofTrueTypeFont miscFont; //font for normal UI text
	ofTrueTypeFont actionFont; //font for action buttons like Continue and Back
	ofTrueTypeFont textFont; //font for input text field
	ofTrueTypeFont titleFont; // font for title

	ofEvent<int>launchApp;
	ofEvent<void> eventEnter; // on press Enter, initiate Selection Screen

protected:
	
	void keyPressedEvent(ofKeyEventArgs &a); //checks events for key presses for text input
	int cursorX, cursorY, appWidth, appHeight;

};