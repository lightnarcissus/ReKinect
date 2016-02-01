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
	currentDrawingLevel = 2;
	for (int i = 0; i < 8; i++) //initialize all to false
	{

		fillCircles.push_back(false);
		targetPoints.push_back(ofVec2f(0, 0));
	}
	targetPoints[0]=ofVec2f(appWidth / 2, appHeight / 2 - 300);
	targetPoints[1]= ofVec2f(appWidth / 2 - 300, appHeight / 2);
	targetPoints[2] = ofVec2f(appWidth / 2 +300, appHeight / 2);
	targetPoints[3] = ofVec2f(appWidth / 2, appHeight / 2 + 300);

	targetPoints[4] = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
	targetPoints[5] = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
	targetPoints[6] = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
	targetPoints[7] = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
		/*ofVec2f circleTarget1(appWidth / 2, appHeight / 2 - 300);
	ofVec2f circleTarget2(appWidth / 2 + 300, appHeight / 2);
	ofVec2f circleTarget3(appWidth / 2 - 300, appHeight / 2);
	ofVec2f circleTarget4(appWidth / 2, appHeight / 2 + 300);*/

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
	ofDrawBitmapString(hitTarget, 100, 100);
	ofDrawBitmapString("Next Target:" + nextTarget, 400, 400);
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

	ofSetColor(ofColor::red);
	ofDrawCircle(appWidth / 2 - 500, appHeight / 2 - 50, 150);

	ofSetColor(ofColor::green);
	ofDrawCircle(appWidth / 2, appHeight / 2 - 50, 150);

	ofSetColor(ofColor::blue);
	ofDrawCircle(appWidth / 2 +500, appHeight / 2 - 50, 150);

	//app selection
	miscFont.drawString("Drawing\nChallenge", appWidth / 2 - 600, appHeight / 2 + 150);
	miscFont.drawString("Multi-Matrix \nMatching", appWidth / 2 - 200, appHeight / 2 + 150);
	miscFont.drawString("Orchestra \nConducting", appWidth / 2 + 300, appHeight / 2 + 150);

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

void PlayerManager::drawingChallengePage(int x,int y, int prevX, int prevY)
{
	switch (currentDrawingLevel)
	{
	case 1:
		drawCircleTargets(x,y);
		break;
	case 2:
		drawSquareTargets(x,y);
		break;
	}
	
	
}

void PlayerManager::drawCircleTargets(int x,int y)
{
	ofVec2f circleTarget1(appWidth / 2, appHeight / 2 - 300);
	ofVec2f circleTarget2(appWidth / 2 + 300, appHeight / 2);
	ofVec2f circleTarget3(appWidth / 2, appHeight / 2 + 300);
	ofVec2f circleTarget4(appWidth / 2 - 300, appHeight / 2);
	if (enableFill)
	{
		ofFill();
	}
	//ofDrawLine(x, y, prevX, prevY);

	for (int i = 0; i < 4; i++)
	{
		/*
		if (fillCircles[i])
		ofFill();
		else
		ofNoFill();
		*/
		ofDrawCircle(targetPoints[i], 50);
	}
	ofSetColor(ofColor::red);
	ofSetLineWidth(3);
	ofNoFill();
	ofBeginShape();
	drawPoints.push_back(ofVec2f(x, y));
	/*
	ostringstream temp;
	temp << hitTarget;
	cout << temp.str();
	*/
	if (hitTarget > 15)
	{
		hitTarget = 0;
		currentDrawingLevel++;
	}
	for (int i = 0; i < drawPoints.size(); i++)
	{
		ofVertex(drawPoints[i].x, drawPoints[i].y);
		if (drawPoints.size() > 50)
		{
			drawPoints.erase(drawPoints.begin());
		}

		if (circleTarget1.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//	cout << "on target 1" << "\n";
			if (nextTarget == 0 || nextTarget == 1)
			{
				nextTarget = 2;
				hitTarget++;
				enableFill = true;
				ofSetColor(0, 255, 0, 128);
				ofFill();
				ofDrawCircle(circleTarget1, 100);
			}
			else if (nextTarget != 2)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 2;
				enableFill = false;
			}
			else
			{

				nextTarget = 2;
			}
		}
		else if (circleTarget2.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 2" << "\n";
			if (nextTarget == 0 || nextTarget == 2)
			{
				nextTarget = 3;
				hitTarget++;
				enableFill = true;
				ofSetColor(0, 255, 0, 128);
				ofFill();
				ofDrawCircle(circleTarget2, 100);
			}
			else if (nextTarget != 3)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 3;
				enableFill = false;
			}
		}
		else if (circleTarget3.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 3" << "\n";
			if (nextTarget == 0 || nextTarget == 3)
			{
				nextTarget = 4;
				hitTarget++;
				enableFill = true;
				ofSetColor(0, 255, 0, 128);
				ofFill();
				ofDrawCircle(circleTarget3, 100);
			}
			else if (nextTarget != 4)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 4;
				enableFill = false;

			}
		}
		else if (circleTarget4.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 4" << "\n";
			if (nextTarget == 0 || nextTarget == 4)
			{
				nextTarget = 1;
				hitTarget++;
				enableFill = true;
				ofFill();
				ofSetColor(0, 255, 0, 128);
				ofDrawCircle(circleTarget4, 100);
			}
			else if (nextTarget != 1)
			{
				hitTarget--;
				nextTarget = 1;

				//cout << "Wrong";
				enableFill = false;

			}
		}

	}
	ofVertex(x, y);
	ofEndShape();
	/*	b.addVertex(ofPoint(x,y));
	b.curveTo(ofPoint(x, y));
	b.bezierTo(x,y,x,y,x,y);
	b.addVertex(ofPoint(x, y));
	b.close();

	b.getSmoothed(5, 0.5);
	angle += TWO_PI / 30;
	b.draw();*/
	//cout << b.getClosestPoint(ofPoint(appWidth / 2, appHeight / 2))<<"\n";
	//path.arc(x, y, 50, 50, 0, 360);
	//path.draw();
}

void PlayerManager::drawSquareTargets(int x, int y)
{
	cout << "In square";
	ofVec2f circleTarget1(appWidth / 2 - 200, appHeight / 2 - 300);
	ofVec2f circleTarget2(appWidth / 2 + 200, appHeight / 2-300);
	ofVec2f circleTarget3(appWidth / 2 + 200, appHeight / 2 + 300);
	ofVec2f circleTarget4(appWidth / 2 - 200, appHeight / 2 + 300);
	if (enableFill)
	{
		ofFill();
	}
	//ofDrawLine(x, y, prevX, prevY);

	for (int i = 4; i < 8; i++)
	{
		/*
		if (fillCircles[i])
		ofFill();
		else
		ofNoFill();
		*/
		ofDrawCircle(targetPoints[i], 50);
	}
	ofSetColor(ofColor::red);
	ofSetLineWidth(3);
	ofNoFill();
	ofBeginShape();
	drawPoints.push_back(ofVec2f(x, y));
	/*
	ostringstream temp;
	temp << hitTarget;
	cout << temp.str();
	*/
	if (hitTarget > 15)
	{
		hitTarget = 0;
		currentDrawingLevel++;
	}
	for (int i = 0; i < drawPoints.size(); i++)
	{
		ofVertex(drawPoints[i].x, drawPoints[i].y);
		if (drawPoints.size() > 50)
		{
			drawPoints.erase(drawPoints.begin());
		}

		if (circleTarget1.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//	cout << "on target 1" << "\n";
			if (enableFill) //correct hit
			{
				ofSetColor(0, 255, 0, 128);
				ofFill();
			}
			else //wrong hit
			{
				ofSetColor(255, 0, 0, 128);
				ofFill();
			}
			ofDrawCircle(circleTarget1, 100); //indicate whether it is a hit or miss
			if (nextTarget == 0 || nextTarget == 1)
			{
				nextTarget = 2;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 2)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 2;
				enableFill = false;
			}
			else
			{

				nextTarget = 2;
			}
		}
		else if (circleTarget2.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 2" << "\n";
			if (enableFill) //correct hit
			{
				ofSetColor(0, 255, 0, 128);
				ofFill();
			}
			else //wrong hit
			{
				ofSetColor(255, 0, 0, 128);
				ofFill();
			}
			ofDrawCircle(circleTarget2, 100);

			if (nextTarget == 0 || nextTarget == 2)
			{
				nextTarget = 3;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 3)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 3;
				enableFill = false;
			}
		}
		else if (circleTarget3.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 3" << "\n";
			if (enableFill) //correct hit
			{
				ofSetColor(0, 255, 0, 128);
				ofFill();
			}
			else //wrong hit
			{
				ofSetColor(255, 0, 0, 128);
				ofFill();
			}
			ofDrawCircle(circleTarget3, 100);

			if (nextTarget == 0 || nextTarget == 3)
			{
				nextTarget = 4;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 4)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 4;
				enableFill = false;

			}
		}
		else if (circleTarget4.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 4" << "\n";
			if (enableFill) //correct hit
			{ 
				ofSetColor(0, 255, 0, 128);
				ofFill();
			}
			else //wrong hit
			{
				ofSetColor(255,0, 0, 128);
				ofFill();
			}
			
			ofDrawCircle(circleTarget4, 100);
			if (nextTarget == 0 || nextTarget == 4)
			{
				nextTarget = 1;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 1)
			{
				hitTarget--;
				nextTarget = 1;

				//cout << "Wrong";
				enableFill = false;

			}
		}

	}
	ofVertex(x, y);
	ofEndShape();
	/*	b.addVertex(ofPoint(x,y));
	b.curveTo(ofPoint(x, y));
	b.bezierTo(x,y,x,y,x,y);
	b.addVertex(ofPoint(x, y));
	b.close();

	b.getSmoothed(5, 0.5);
	angle += TWO_PI / 30;
	b.draw();*/
	//cout << b.getClosestPoint(ofPoint(appWidth / 2, appHeight / 2))<<"\n";
	//path.arc(x, y, 50, 50, 0, 360);
	//path.draw();
}

void PlayerManager::matrixMatchingPage()
{

}

void PlayerManager::musicConductorPage()
{

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