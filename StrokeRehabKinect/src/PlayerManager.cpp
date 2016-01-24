#include "PlayerManager.h"

PlayerManager::PlayerManager() {
	clientName = "";
	position = 0;
	appWidth = ofGetWidth();
	appHeight = ofGetHeight();
	cursorX = appWidth/2;
	cursorY = appHeight/2 + 100;
	debugFloat = 0;
}

void PlayerManager::init() {

	//load fonts
	titleFont.load("segoeui.ttf", 80);
	miscFont.load("segoeui.ttf", 40);
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
	
	miscFont.drawString("Continue -->", (float)ofGetWidth() / 2 + 250, (float)ofGetHeight() / 2 + 160);
}

void PlayerManager::drawInputText()
{
	ofSetColor(ofColor::red);
	textFont.drawString(clientName, (float)(ofGetWidth() / 2)+150, (float)(ofGetHeight() / 2) + 50);

}

void PlayerManager::drawCursor() const {

	////disabled cursor blink unless needed
	/*ofPushStyle();
	float timeFrac = 0.5 * sin(3.0f * ofGetElapsedTimef()) + 0.5;

	ofColor col = ofGetStyle().color;

	ofSetColor(col.r * timeFrac, col.g * timeFrac, col.b * timeFrac);
	ofSetLineWidth(1.0f);
	
	
	//ofLine(cursorX *2 + 10, 13.7*cursorY + 30, cursorX * 2 + 10, 10 + 13.7*cursorY +30);

	ofPopStyle();
	*/
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

void PlayerManager::mouseEvent(int x,int y)
{
	ofVec2f mousePos(x, y);
	ofVec2f leftFieldPos((float)ofGetWidth() / 2 - 500, (float)ofGetHeight() / 2 + 160); //for Left
	ofVec2f rightFieldPos((float)ofGetWidth() / 2 - 400, (float)ofGetHeight() / 2 + 160); // for Right
	ofVec2f continuePos((float)ofGetWidth() / 2 + 250, (float)ofGetHeight() / 2 + 160); // for Continue Button
	debugFloat = mousePos.distance(leftFieldPos);  //for debug
	if (mousePos.distance(leftFieldPos) < 50)
	{
		rightSideActivated = false;
	}

	if(mousePos.distance(rightFieldPos) < 50)
	{
		rightSideActivated = true;
	}

	if (mousePos.distance(continuePos) < 70) //need a better way to activate "Buttons" without using GUI
	{
		ofNotifyEvent(eventEnter, this); //activate Selection Screen
	}

}

void PlayerManager::keyPressedEvent(ofKeyEventArgs &a) {
	keyPressed(a.key);
}