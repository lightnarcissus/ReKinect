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
	currentDrawingLevel = 1;
	for (int i = 0; i < 29; i++) //initialize all to false
	{

		fillCircles.push_back(false);
		targetPoints.push_back(ofVec2f(0, 0));
	}
	targetPoints[0]=ofVec2f(appWidth / 2, appHeight / 2 - 300);
	targetPoints[1]= ofVec2f(appWidth / 2 - 300, appHeight / 2);
	targetPoints[2] = ofVec2f(appWidth / 2 +300, appHeight / 2);
	targetPoints[3] = ofVec2f(appWidth / 2, appHeight / 2 + 300);

	//for square
	targetPoints[4] = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
	targetPoints[5] = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
	targetPoints[6] = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
	targetPoints[7] = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);

	//for hexagon
	targetPoints[8] = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
	targetPoints[9] = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
	targetPoints[10] = ofVec2f(appWidth / 2 - 300, appHeight / 2);
	targetPoints[11] = ofVec2f(appWidth / 2 + 300, appHeight / 2);
	targetPoints[12] = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
	targetPoints[13] = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);

	//for invisible octagon
	targetPoints[14] = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
	targetPoints[15] = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
	targetPoints[16] = ofVec2f(appWidth / 2 + 300, appHeight / 2 -100);
	targetPoints[17] = ofVec2f(appWidth / 2 + 300, appHeight / 2 + 100);
	targetPoints[18] = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
	targetPoints[19] = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
	targetPoints[20] = ofVec2f(appWidth / 2 - 300, appHeight / 2 + 100);
	targetPoints[21] = ofVec2f(appWidth / 2 - 300, appHeight / 2  -100);

	targetPoints[25] = ofVec2f(0,0);
	targetPoints[24] = ofVec2f(0, appHeight / 2);
	targetPoints[22] = ofVec2f(appWidth / 2, 0);
	targetPoints[23] = ofVec2f(appWidth / 2, appHeight/2);

	targetPoints[26] = ofVec2f(150, appHeight / 2 + 200);
	targetPoints[27] = ofVec2f(appWidth / 2, appHeight / 2 -250);
	targetPoints[28] = ofVec2f(appWidth / 2 + 500, appHeight / 2 + 300);
	//music.load("sounds/synth.wav", false);
	
		/*ofVec2f circleTarget1(appWidth / 2, appHeight / 2 - 300);
	ofVec2f circleTarget2(appWidth / 2 + 300, appHeight / 2);
	ofVec2f circleTarget3(appWidth / 2 - 300, appHeight / 2);
	ofVec2f circleTarget4(appWidth / 2, appHeight / 2 + 300);*/
	//music.loadSound("synth.wav",false);
	//music.setVolume(0.3);
	//music2.loadSound("synth2.wav",false);
	//music2.setVolume(0.3);

}

void PlayerManager::init() {

	//load fonts
	titleFont.load("segoeui.ttf", 80);
	miscFont.load("segoeui.ttf", 40);
	actionFont.load("segoeui.ttf", 30);
	textFont.load("micross.ttf", 30);


	//loading conductor images
	orchestraBg.loadImage("orchestra4Eozin.png");

	violinist.loadImage("violinist.png");
	contrabass.loadImage("contrabass.jpg");

	background.loadImage("newbackground.png");
	fakeStage.loadImage("fakeStage1.png");

	//background.loadImage("background2.jpg");
	//fakeStage.loadImage("fakeStage.jpg");

	mCenter.loadImage("1_center.png");
	mLeft1.loadImage("1_left.png");
	mLeft2.loadImage("2_left.png");
	mRight1.loadImage("1_right.png");
	mRight2.loadImage("2_right.png");

	mOne.loadImage("1.png");
	mTwo.loadImage("2.png");
	mThree.loadImage("3.png");


	orchestraActive1.loadImage("orcActive1.png");
	orchestraActive2.loadImage("orcActive2.png");
	orchestraActive3.loadImage("orcActive3.png");
	//listen for Selection Screen events
	ofAddListener(ofEvents().keyPressed, this, &PlayerManager::keyPressedEvent);



	//music conductor load events

	//music.load("sounds/synth.wav", false);
	//music.setVolume(0.3);
//	music2.loadSound("synth2.wav",false);
	//music.setMultiPlay(true);
	//music2.setMultiPlay(true);
	//music2.setVolume(0.3);

}

void PlayerManager::drawTitlePage()
{
	//draw text
	ofSetColor(ofColor::white);
	titleFont.drawString("Stroke + Kinect", 300, 300);
	miscFont.drawString("Client Name", (float)ofGetWidth() / 2 - 150, (float)ofGetHeight() / 2 + 60);
//	ofDrawBitmapString(hitTarget, 100, 100);
	//ofDrawBitmapString("Next Target:" + nextTarget, 400, 400);
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
	miscFont.drawString("Orchestra \nConducting", appWidth / 2 + 350, appHeight / 2 + 150);

	actionFont.drawString("<-- Back", appWidth / 2 - 650, appHeight / 2 + 300);
	//currentTimer -= lastResetTime;
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


	//for shuffle functions
	if (key == '1') {
		picnum = 4;
		b_shuffle = 250;

		initCards();
	}

	if (key == '2') {
		picnum = 6;
		b_shuffle = 250;

		initCards();
	}

	if (key == '3') {
		picnum = 10;
		b_shuffle = 70;

		initCards();
	}

}

void PlayerManager::drawingChallengePage(float x,float y, int prevX, int prevY)
{
	switch (currentDrawingLevel)
	{
	case 1:
		drawCircleTargets(x,y);
		break;
	case 2:
		drawSquareTargets(x,y);
		break;
	case 3:
		drawHexagonTargets(x, y);
		break;
	case 4:
		drawInvisibleOctagonTargets(x, y);
		break;
	default:
		drawCircleTargets(x, y);
		break;
	}

	//other text stuff
	currentTimer = ofGetElapsedTimef()- lastResetTime;
	stringParser.str("");
	stringParser.clear();
	stringParser << "Score: \n" << hitTarget;
	ofSetColor(ofColor::white);
	textFont.drawString(stringParser.str(), appWidth / 2 + 400, appHeight / 2 - 300);
	textFont.drawString("Instructions: \nMove clockwise \n through the \n targets",0,100);
	stringParser.str("");
	stringParser.clear();
	stringParser << "Timer: \n" << currentTimer;
	textFont.drawString(stringParser.str(), appWidth / 2 + 400, appHeight / 2 + 300);
	ofSetColor(ofColor::red);


}

void PlayerManager::drawCircleTargets(float x,float y)
{
	if (rightSideActivated)
	{
		circleTarget4=ofVec2f(appWidth / 2, appHeight / 2 - 300);
		circleTarget3 = ofVec2f(appWidth / 2 + 300, appHeight / 2);
		circleTarget2 = ofVec2f(appWidth / 2, appHeight / 2 + 300);
		circleTarget1 = ofVec2f(appWidth / 2 - 300, appHeight / 2);
	}
	else
	{
		circleTarget1 = ofVec2f(appWidth / 2, appHeight / 2 - 300);
		circleTarget2 = ofVec2f(appWidth / 2 + 300, appHeight / 2);
		circleTarget3 = ofVec2f(appWidth / 2, appHeight / 2 + 300);
		circleTarget4 = ofVec2f(appWidth / 2 - 300, appHeight / 2);
	}
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
		ofDrawCircle(targetPoints[i], 50 + ((15-hitTarget) * 2));
	}
	ofSetColor(ofColor::red);
	ofSetLineWidth(3);
	ofNoFill();
//	cout << "doing this";
	//ofSetPolyMode(OF_POLY_WINDING_ODD);
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
	else if (hitTarget < 0)
	{
		hitTarget = 0;
	}
	for (int i = 0; i < drawPoints.size(); i++)
	{
		ofVertex(drawPoints[i].x, drawPoints[i].y);
		if (drawPoints.size() > 50)
		{
			drawPoints.erase(drawPoints.begin());
		}

		if (circleTarget1.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15-hitTarget) * 2))
		{
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

			ofDrawCircle(circleTarget1, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			//	cout << "on target 1" << "\n";
			if (nextTarget == 0 || nextTarget == 1)
			{
				nextTarget = 2;
				hitTarget++;
				enableFill = true;
				ofSetColor(0, 255, 0, 128);
				ofFill();
				ofDrawCircle(circleTarget1, 100 + ((15 - hitTarget) * 2));
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
		else if (circleTarget2.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
		{
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

			ofDrawCircle(circleTarget2, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			//cout << "on target 2" << "\n";
			if (nextTarget == 0 || nextTarget == 2)
			{
				nextTarget = 3;
				hitTarget++;
				enableFill = true;
				ofSetColor(0, 255, 0, 128);
				ofFill();
				ofDrawCircle(circleTarget2, 100 + ((15 - hitTarget) * 2));
			}
			else if (nextTarget != 3)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 3;
				enableFill = false;
			}
		}
		else if (circleTarget3.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
		{
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

			ofDrawCircle(circleTarget3, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			//cout << "on target 3" << "\n";
			if (nextTarget == 0 || nextTarget == 3)
			{
				nextTarget = 4;
				hitTarget++;
				enableFill = true;
				ofSetColor(0, 255, 0, 128);
				ofFill();
				ofDrawCircle(circleTarget3, 100 + ((15 - hitTarget) * 2));
			}
			else if (nextTarget != 4)
			{
				//	cout << "Wrong";
				hitTarget--;
				nextTarget = 4;
				enableFill = false;

			}
		}
		else if (circleTarget4.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
		{
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

			ofDrawCircle(circleTarget4, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			//cout << "on target 4" << "\n";
			if (nextTarget == 0 || nextTarget == 4)
			{
				nextTarget = 1;
				hitTarget++;
				enableFill = true;
				ofFill();
				ofSetColor(0, 255, 0, 128);
				ofDrawCircle(circleTarget4, 100 + ((15 - hitTarget) * 2));
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
	ofEndShape();
	/*b.addVertex(ofPoint(x, y));
	b.curveTo(ofPoint(x, y));
	b.bezierTo(x, y, x, y, x, y);
	b.addVertex(ofPoint(x, y));
	b.close();

	b.getSmoothed(5, 0.5);
	//angle += TWO_PI / 30;
	b.draw(); */

}

void PlayerManager::drawHexagonTargets(float x, float y)
{
	if (rightSideActivated)
	{
		circleTarget6 = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
		circleTarget5 = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
		circleTarget4 = ofVec2f(appWidth / 2 + 300, appHeight / 2);
		circleTarget3 = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
		circleTarget2 = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
		circleTarget1 = ofVec2f(appWidth / 2 - 300, appHeight / 2);
	}
	else
	{
		circleTarget1 = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
		circleTarget2 = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
		circleTarget3 = ofVec2f(appWidth / 2 + 300, appHeight / 2);
		circleTarget4 = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
		circleTarget5 = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
		circleTarget6 = ofVec2f(appWidth / 2 - 300, appHeight / 2);
		
	}
	
	if (enableFill)
	{
		ofFill();
	}
	//ofDrawLine(x, y, prevX, prevY);

	for (int i = 8; i < 14; i++)
	{
		/*
		if (fillCircles[i])
		ofFill();
		else
		ofNoFill();
		*/
		ofDrawCircle(targetPoints[i], 50 + ((15 - hitTarget) * 2));
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
	else if (hitTarget < 0)
	{
		hitTarget = 0;
	}
	for (int i = 0; i < drawPoints.size(); i++)
	{
		ofVertex(drawPoints[i].x, drawPoints[i].y);
		if (drawPoints.size() > 50)
		{
			drawPoints.erase(drawPoints.begin());
		}

		if (circleTarget1.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			ofDrawCircle(circleTarget1, 100 + ((15 - hitTarget) * 2)); //indicate whether it is a hit or miss
			ofSetColor(255, 0, 0);
			ofNoFill();
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
		else if (circleTarget2.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			ofDrawCircle(circleTarget2, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();

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
		else if (circleTarget3.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			ofDrawCircle(circleTarget3, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();

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
		else if (circleTarget4.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
		{
			//cout << "on target 4" << "\n";
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

			ofDrawCircle(circleTarget4, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 4)
			{
				nextTarget = 5;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 5)
			{
				hitTarget--;
				nextTarget = 5;

				//cout << "Wrong";
				enableFill = false;

			}
		}
		else if (circleTarget5.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
		{
			//cout << "on target 5" << "\n";
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

			ofDrawCircle(circleTarget5, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 5)
			{
				nextTarget = 6;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 6)
			{
				hitTarget--;
				nextTarget = 6;

				//cout << "Wrong";
				enableFill = false;

			}
		}

		else if (circleTarget6.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
		{
			//cout << "on target 4" << "\n";
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

			ofDrawCircle(circleTarget6, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 6)
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
	//ofVertex(x, y);
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

void PlayerManager::drawInvisibleOctagonTargets(float x, float y)
{
	if (rightSideActivated)
	{
		circleTarget8 = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
		circleTarget7 = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
		circleTarget6 = ofVec2f(appWidth / 2 + 300, appHeight / 2 - 100);
		circleTarget5 = ofVec2f(appWidth / 2 + 300, appHeight / 2 + 100);
		circleTarget4 = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
		circleTarget3 = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
		circleTarget2 = ofVec2f(appWidth / 2 - 300, appHeight / 2 + 100);
		circleTarget1 = ofVec2f(appWidth / 2 - 300, appHeight / 2 - 100);
	}
	else
	{
		circleTarget1 = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
		circleTarget2 = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
		circleTarget3 = ofVec2f(appWidth / 2 + 300, appHeight / 2 - 100);
		circleTarget4 = ofVec2f(appWidth / 2 + 300, appHeight / 2 + 100);
		circleTarget5 = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
		circleTarget6 = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
		circleTarget7 = ofVec2f(appWidth / 2 - 300, appHeight / 2 + 100);
		circleTarget8 = ofVec2f(appWidth / 2 - 300, appHeight / 2 - 100);
		
	}
	
	if (enableFill)
	{
		ofFill();
	}
	//ofDrawLine(x, y, prevX, prevY);

	for (int i = 14; i < 22; i++)
	{
		/*
		if (fillCircles[i])
		ofFill();
		else
		ofNoFill();
		*/
		//ofDrawCircle(targetPoints[i], 50);
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

		if (circleTarget1.distance(drawPoints[drawPoints.size() - 1]) < 50 + (hitTarget * 2))
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
			ofDrawCircle(circleTarget1, 100 + (hitTarget * 2)); //indicate whether it is a hit or miss
			ofSetColor(255, 0, 0);
			ofNoFill();
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
		else if (circleTarget2.distance(drawPoints[drawPoints.size() - 1]) < 50 + (hitTarget * 2))
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
			ofDrawCircle(circleTarget2, 100 + (hitTarget * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();

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
		else if (circleTarget3.distance(drawPoints[drawPoints.size() - 1]) < 50 + (hitTarget * 2))
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
			ofSetColor(255, 0, 0);
			ofNoFill();

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
		else if (circleTarget4.distance(drawPoints[drawPoints.size() - 1]) < 50 + (hitTarget * 2))
		{
			//cout << "on target 4" << "\n";
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

			ofDrawCircle(circleTarget4, 100);
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 4)
			{
				nextTarget = 5;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 5)
			{
				hitTarget--;
				nextTarget = 5;

				//cout << "Wrong";
				enableFill = false;

			}
		}
		else if (circleTarget5.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 5" << "\n";
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

			ofDrawCircle(circleTarget5, 100);
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 5)
			{
				nextTarget = 6;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 6)
			{
				hitTarget--;
				nextTarget = 6;

				//cout << "Wrong";
				enableFill = false;

			}
		}

		else if (circleTarget6.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 4" << "\n";
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

			ofDrawCircle(circleTarget6, 100);
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 6)
			{
				nextTarget = 7;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget !=7)
			{
				hitTarget--;
				nextTarget = 7;

				//cout << "Wrong";
				enableFill = false;

			}
		}
		else if (circleTarget7.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 4" << "\n";
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

			ofDrawCircle(circleTarget7, 100);
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 7)
			{
				nextTarget = 8;
				hitTarget++;
				enableFill = true;
			}
			else if (nextTarget != 8)
			{
				hitTarget--;
				nextTarget = 8;

				//cout << "Wrong";
				enableFill = false;

			}
		}
		else if (circleTarget8.distance(drawPoints[drawPoints.size() - 1]) < 50)
		{
			//cout << "on target 4" << "\n";
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

			ofDrawCircle(circleTarget8, 100);
			ofSetColor(255, 0, 0);
			ofNoFill();
			if (nextTarget == 0 || nextTarget == 8)
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
	//ofVertex(x, y);
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
void PlayerManager::drawSquareTargets(float x, float y)
{
	//cout << "In square";
	if (rightSideActivated)
	{
		circleTarget4 = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
		circleTarget3 = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
		circleTarget2 = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
		circleTarget1 = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
	}
	else
	{
		circleTarget1 = ofVec2f(appWidth / 2 - 200, appHeight / 2 - 300);
		circleTarget2 = ofVec2f(appWidth / 2 + 200, appHeight / 2 - 300);
		circleTarget3 = ofVec2f(appWidth / 2 + 200, appHeight / 2 + 300);
		circleTarget4 = ofVec2f(appWidth / 2 - 200, appHeight / 2 + 300);
		
	}
	
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
		ofDrawCircle(targetPoints[i], 50 + ((15 - hitTarget) * 2));
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
	else if (hitTarget < 0)
	{
		hitTarget = 0;
	}
	for (int i = 0; i < drawPoints.size(); i++)
	{
		ofVertex(drawPoints[i].x, drawPoints[i].y);
		if (drawPoints.size() > 50)
		{
			drawPoints.erase(drawPoints.begin());
		}

		if (circleTarget1.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			ofDrawCircle(circleTarget1, 100 + ((15 - hitTarget) * 2)); //indicate whether it is a hit or miss
			ofSetColor(255, 0, 0);
			ofNoFill();
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
		else if (circleTarget2.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			ofDrawCircle(circleTarget2, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();

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
		else if (circleTarget3.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			ofDrawCircle(circleTarget3, 100 + ((15 - hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();

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
		else if (circleTarget4.distance(drawPoints[drawPoints.size() - 1]) < 50 + ((15 - hitTarget) * 2))
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
			
			ofDrawCircle(circleTarget4, 100+ ((15-hitTarget) * 2));
			ofSetColor(255, 0, 0);
			ofNoFill();
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
//	ofVertex(x, y);
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

//shuffle functions

void PlayerManager::matrixMatchingPage()
{
	//cout << "At shuffle";
	ofEnableSmoothing();
	ofSetWindowShape(3840, 1200);

	for (int m = 0; m < pics.size(); m++)
	{
		pics[m]->drawsneaky();
	}

	for (int i = 0; i < pics2.size(); i++)
	{
		pics2[i]->drawsneaky();
	}

	int elapsed = ofGetElapsedTimeMillis() - startTime;

}

void PlayerManager::initCards()
{

		vector<int> order;

		pics.clear();
		pics2.clear();

		for (int i = 0; i < picnum; i++) {
			order.push_back(0);
		}

		order = shuffleIt.shuffle(picnum);

		for (int i = 0; i< picnum; i++) {

			int x = i % 2 * (ofGetWidth() / 6) + ofGetWidth() / 4;
			int y = i / 2 * (ofGetHeight() / 6) + b_shuffle;

			Pic* tempPic = new Pic(x, y, order[i], c1, c2, c3);
			pics.push_back(tempPic);

		}

		order = shuffleIt.shuffle(picnum);

		for (int i = 0; i< picnum; i++) {

			int x = i % 2 * (ofGetWidth() / 6) + ofGetWidth() / 4 + 140;
			int y = i / 2 * (ofGetHeight() / 6) + b_shuffle;

			Pic* tempPic2 = new Pic(x, y, order[i], c1, c2, c3);
			pics2.push_back(tempPic2);

		}

		ofLog() << "order in initCard : " << order.size();
	
}


void PlayerManager::checkCollisions()
{
	match = 0;
	//println(match +""+"match!!!");
	//println("checking collisions...");
	for (int i = 0; i< pics.size(); i++)
	{
		pics[i]->match = 0;
		pics2[i]->match = 0; // assume they're not touching yet
	}
	for (int i = 0; i<pics.size(); i++)
	{
		for (int j = 0; j<pics2.size(); j++)
		{
			if (pics[i]->whichcard == pics2[j]->whichcard) {
				if ((pics[i]->locationX + picsize) >= pics2[j]->locationX&&pics[i]->locationX<(pics2[j]->locationX + picsize) && (pics[i]->locationY + picsize) >= pics2[j]->locationY&&pics[i]->locationY<(pics2[j]->locationY + picsize))
				{
					//println(i + " is touching " + j);
					pics[i]->match = 1;
					pics2[j]->match = 1;
					match++;
				}
			}
		}
	}
	if (match == pics.size()) {
			
		startTime = ofGetElapsedTimeMillis();
	}
}

void PlayerManager::musicConductorPage(float x,float y)
{

	//cout << "In here";
	//ofSetColor(ofColor::white);


	ofSetColor(ofColor::white);
	background.resize(appWidth, appHeight);
	if(musicActivated==0)
	background.draw(0, 0, 0);

	/*	ofSetColor(0, 0, 0, 255);
	ofDrawCircle(100, appHeight/2 + 200, 50);
	ofDrawCircle(appWidth / 2, appHeight / 2  - 150, 50);
	ofDrawCircle(appWidth / 2 + 500, appHeight / 2 + 300, 50);*/
	ofSetColor(ofColor::white);
	ofVec3f instrument1(appWidth / 2 - 100, appHeight, 0);
	ofVec3f mousePos(x, y, 0);
	//cout << "X: " << x << " and Y: " << y << "\n";
	if (mousePos.distance(targetPoints[26]) < 150)
	{
		//cout << "TARGET LEFT";
		orchestraActive1.resize(appWidth, appHeight);
		orchestraActive1.draw(0, 0,0);
		//musicActivated = 1;
	}
	else if (mousePos.distance(targetPoints[27]) < 300)
	{
		//cout << "TARGET MIDDLE";
		orchestraActive2.resize(appWidth, appHeight);
		orchestraActive2.draw(0, 0, 0);
		//musicActivated = 2;
	}
	else if (mousePos.distance(targetPoints[28]) < 150)
	{
		//cout << "TARGET RIGHT";
		orchestraActive3.resize(appWidth, appHeight);
		orchestraActive3.draw(0, 0, 0);
	//	musicActivated = 3;
	}
/*	if (x > appWidth / 2 && y < appHeight / 2) //quadrant 1
	{
		ofDrawBitmapString("HIIII", appWidth / 2, appWidth / 2 + 100);
		//music.play();
		//music.setVolume((appHeight / 2 - y) / appHeight);
		musicActivated = 1;
	}
	else if (x > appWidth / 2 && y > appHeight / 2) //quadrant 2
	{
		ofDrawBitmapString("HIIII", appWidth / 2, appWidth / 2 + 100);
		//music.play();
		//music.setVolume((y-appHeight / 2) / appHeight);
		musicActivated = 2;
	}
	else if (x < appWidth / 2 && y > appHeight / 2) //quadrant 3
	{
		ofDrawBitmapString("HIIII", appWidth / 2, appWidth / 2 + 100);
		//music2.play();
		//music2.setVolume((y-appHeight / 2) / appHeight);
		musicActivated = 3;
	}
	*/
	/*
	for (int i = 22; i < 26; i++)
	{
		if (musicActivated == (i-22))
		{
			ofSetColor(0,255,0,128);
		}
		else
		{
			ofSetColor(255,0,0,128);
		}
		ofRect(targetPoints[i], appWidth / 2, appHeight / 2);
	}
	*/
	//ofDrawCircle(300, 300, 100);
	//fakeStage.draw(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
	//ofDrawCone(appWidth - 100, appHeight-300, 0, 100, 100);
	//ofDrawCone(appWidth, appHeight, 0, 100, 100);
	//background.draw(0, 0, ofGetWindowWidth(), ofGetWindowHeight());
	//orchestraBg.draw(0, 0, ofGetWindowWidth(), ofGetWindowHeight()-100);
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

	if (activeApp == 2) //for shuffle cards
	{
		whichoneamimoving = -1;

		for (int j = pics2.size() - 1; j >= 0; j--)
		{
			if (ofGetMouseX() >= pics2[j]->locationX&&ofGetMouseX()<(pics2[j]->locationX + picsize) && ofGetMouseY() >= pics2[j]->locationY&&ofGetMouseY()<(pics2[j]->locationY + picsize))
			{

				num = j;
				whichoneamimoving = j;

				pics2[j]->tint = true;
				pics2[j]->col = 255;
				pics2[j]->col2 = 215;
				pics2[j]->col3 = 0;

				//ofLog() << pics2[j].col + ""+ "COL!!!";
				break;
			}
		}

		if (whichoneamimoving == -1) {

			for (int i = pics.size() - 1; i >= 0; i--)
			{
				if (ofGetMouseX() >= pics[i]->locationX && ofGetMouseX()<(pics[i]->locationX + picsize) && ofGetMouseY() >= pics[i]->locationY&&ofGetMouseY()<(pics[i]->locationY + picsize))
				{
					whichoneamimoving = i + pics.size();
					num = i;
					pics[i]->tint = true;
					pics[i]->col = 255;
					pics[i]->col2 = 215;
					pics[i]->col3 = 0;

					break;
				}
			}
		}

	}
}

void PlayerManager::mouseDragged(int x, int y, int button)
{
	if (whichoneamimoving>-1 && whichoneamimoving<pics.size())
	{
		pics2[whichoneamimoving]->locationX += (ofGetMouseX() - ofGetPreviousMouseX());
		pics2[whichoneamimoving]->locationY += (ofGetMouseY() - ofGetPreviousMouseY());
	}
	else if (whichoneamimoving>-1 && whichoneamimoving >= pics.size())
	{
		pics[whichoneamimoving - pics.size()]->locationX += (ofGetMouseX() - ofGetPreviousMouseX());

		pics[whichoneamimoving - pics.size()]->locationY += (ofGetMouseY() - ofGetPreviousMouseY());
	}
}

void PlayerManager::mouseReleased(int x, int y, int button)
{
	whichoneamimoving = -1;
	checkCollisions();
//	pics[num]->tint = false;
//	pics2[num]->tint = false;
}
void PlayerManager::keyPressedEvent(ofKeyEventArgs &a) {
	keyPressed(a.key);
}