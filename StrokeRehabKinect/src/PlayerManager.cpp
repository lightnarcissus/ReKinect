#include "PlayerManager.h"

PlayerManager::PlayerManager() {
	clientName = "";
	position = 0;
	appWidth = ofGetWidth();
	appHeight = ofGetHeight();
	cursorX = appWidth/2;
	cursorY = appHeight/2 + 100;
	activeApp = 0;
	debugFloat = 0;
}

void PlayerManager::init() {

	//load fonts
	titleFont.load("segoeui.ttf", 80);
	miscFont.load("segoeui.ttf", 40);
	actionFont.load("segoeui.ttf", 30);
	textFont.load("micross.ttf", 30);

	//listen for Selection Screen events
	ofAddListener(ofEvents().keyPressed, this, &PlayerManager::keyPressedEvent);
}

void PlayerManager::drawTitlePage()
{
	//draw text
	ofSetColor(ofColor::white);
	titleFont.drawString("Stroke + Kinect", 300, 300);
	miscFont.drawString("Client Name", (float)ofGetWidth() / 2 - 150, (float)ofGetHeight() / 2 + 60);
	ofDrawBitmapString(debugFloat, 100, 100);
	miscFont.drawString("Problem Side", (float)ofGetWidth() / 2 - 550, (float)ofGetHeight() / 2 + 60);
	//draw if Left Side is selected
	if (!rightSideActivated)
	{
		ofSetColor(ofColor::red);
		miscFont.drawString("L", (float)ofGetWidth() / 2 - 500, (float)ofGetHeight() / 2 + 160);
		ofSetColor(ofColor::white);
		miscFont.drawString("/", (float)ofGetWidth() / 2 - 450, (float)ofGetHeight() / 2 + 160);
		miscFont.drawString("R", (float)ofGetWidth() / 2 - 400, (float)ofGetHeight() / 2 + 160);
	}
	//draw if Right Side is selected
	else
	{
		miscFont.drawString("L", (float)ofGetWidth() / 2 - 500, (float)ofGetHeight() / 2 + 160);
		miscFont.drawString("/", (float)ofGetWidth() / 2 - 450, (float)ofGetHeight() / 2 + 160);
		ofSetColor(ofColor::red);
		miscFont.drawString("R", (float)ofGetWidth() / 2 - 400, (float)ofGetHeight() / 2 + 160);
		ofSetColor(ofColor::white);
	}
	ofDrawRectangle((float)ofGetWidth() / 2 +150, (float)ofGetHeight() / 2, 0, 300, 80);
	
	actionFont.drawString("Continue -->", (float)ofGetWidth() / 2 + 250, (float)ofGetHeight() / 2 + 160);
}

void PlayerManager::drawInputText()
{
	ofSetColor(ofColor::red);
	textFont.drawString(clientName, (float)(ofGetWidth() / 2)+150, (float)(ofGetHeight() / 2) + 50);

}

void PlayerManager::drawCursor() const {

	////disabled cursor blink unless needed
/*	ofPushStyle();
	float timeFrac = 0.5 * sin(3.0f * ofGetElapsedTimef()) + 0.5;

	ofColor col = ofGetStyle().color;

	ofSetColor(col.r * timeFrac, col.g * timeFrac, col.b * timeFrac);
	ofSetLineWidth(1.0f);
	
	
	ofLine(cursorX * 1.4 + 10 + 45.5 , 13.7*cursorY + 30 - 5, cursorX * 1.4 + 10 + 45.5, 10 + 13.7*cursorY +30 - 10.5);

	ofPopStyle();
	*/
}

void PlayerManager::drawAppSelectionPage()
{
	ofSetColor(ofColor::white);
	miscFont.drawString("Select a Game", 100, 100);

	//app selection
	miscFont.drawString("Drawing\nChallenge", appWidth / 2 - 600, appHeight / 2 + 150);
	miscFont.drawString("Multi-Matrix \nMatching", appWidth / 2 - 200, appHeight / 2 + 150);
	miscFont.drawString("Orchestra \nConducting", appWidth / 2 + 200, appHeight / 2 + 150);

	actionFont.drawString("<-- Back", appWidth / 2 - 650, appHeight / 2 + 300);
}

void PlayerManager::keyPressed(int key) {
	//add charachter (non unicode sorry!)
	if (key >= 32 && key <= 126) {
		if (position < 15)
		{
			clientName.insert(clientName.begin() + position, key);
			position++;
		}
	}

	if (key == OF_KEY_RETURN) {
		ofNotifyEvent(eventEnter, this);
		//ofNotifyEvent(eventEnter, clientName, this);
	/*	if (eventEnter.empty()) {
			clientName.insert(text.begin() + position, '\n');
			position++;
		//} */
	}

	if (key == OF_KEY_BACKSPACE) {
		if (position>0) {
			clientName.erase(clientName.begin() + position - 1);
			--position;
		}
	}

	if (key == OF_KEY_DEL) {
		if (clientName.size() > position) {
			clientName.erase(clientName.begin() + position);
		}
	}

	if (key == OF_KEY_LEFT)
		if (position>0)
			--position;

	if (key == OF_KEY_RIGHT)
		if (position<clientName.size() + 1)
			++position;

	//for multiline:
	cursorX = cursorY = 0;
	for (int i = 0; i<position; ++i) {
		if (*(clientName.begin() + i) == '\n') {
			++cursorY;
			cursorX = 0;
		}
		else {
			cursorX++;
		}
	}
}

void PlayerManager::clear() {
	clientName.clear();
	position = 0;
}

void PlayerManager::mouseEvent(int x,int y, int appState)
{
	ofVec2f mousePos(x, y);
	ofVec2f leftFieldPos((float)ofGetWidth() / 2 - 500, (float)ofGetHeight() / 2 + 160); //for Left
	ofVec2f rightFieldPos((float)ofGetWidth() / 2 - 400, (float)ofGetHeight() / 2 + 160); // for Right
	ofVec2f continuePos((float)ofGetWidth() / 2 + 250, (float)ofGetHeight() / 2 + 160); // for Continue Button
	ofVec2f drawingPos(appWidth / 2 - 600, appHeight / 2 + 150);
	ofVec2f cardMatchingPos(appWidth / 2 - 200, appHeight / 2 + 150);
	ofVec2f musicConductorPos(appWidth / 2 + 200, appHeight / 2 + 150);
	switch (appState)
	{
	case 0:
		
		debugFloat = mousePos.distance(leftFieldPos);  //for debug
		if (mousePos.distance(leftFieldPos) < 50)
		{
			rightSideActivated = false;
		}

		else if (mousePos.distance(rightFieldPos) < 50)
		{
			rightSideActivated = true;
		}

		else if (mousePos.distance(continuePos) < 70) //need a better way to activate "Buttons" without using GUI
		{
			ofNotifyEvent(eventEnter, this); //activate Selection Screen
		}

		break;
	case 1:
		if (y < 600 && y > 130 && x < 310 && x > 60)
		{
			activeApp = 1;
			ofNotifyEvent(launchApp, activeApp, this);
		}
		else if (y < 600 && y >130 && x < 770 && x > 450)
		{
			activeApp = 2;
			ofNotifyEvent(launchApp,activeApp, this);
		}
		else if (y < 600 && y >130 && x > 870 && x < 1145)
		{
			activeApp = 3;
			ofNotifyEvent(launchApp,activeApp, this);
		}
		break;
	}
}

void PlayerManager::keyPressedEvent(ofKeyEventArgs &a) {
	keyPressed(a.key);
}