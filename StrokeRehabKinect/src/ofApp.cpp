#include "ofApp.h"

//--------------------------------------------------------------
void ofApp::setup(){

	
	ofSetFrameRate(30); //set Frame Rate to 30
	ofSetVerticalSync(true); //set V-Sync
	
	//GUI panel for positional debugging
	//gui.setup("panel");

	//load fonts
	player.init();

	ofAddListener(player.eventEnter, this, &ofApp::activateSelectionScreen);
}


//--------------------------------------------------------------
void ofApp::update(){

}

//--------------------------------------------------------------
void ofApp::draw(){

	ofBackground(ofColor::blueSteel);
	switch (appState)
	{
		//start page
	case 0:

		/* disabled cursor unless needed
		ofPushMatrix();
		ofScale(15, 15);
		player.draw();
		ofPopMatrix();
		*/
		player.drawTitlePage();
		player.drawInputText();
		break;

		//game selection screen
	case 1:
		cout << "App Selection Screen";
	}
}

void ofApp::activateSelectionScreen()
{
	if(appState==0)
		appState = 1; //switch to Game Selection Screen
}

//for future use
void ofApp::addText(string &s) {
	textEntries.push_back(s);
	player.clear();
}
//--------------------------------------------------------------
//for future use
void ofApp::drawEntries() {
	for (int i = 0; i<textEntries.size(); ++i) {
		ofDrawBitmapString(textEntries[i], 50, 100 + 10 * i);
	}
}
//--------------------------------------------------------------
void ofApp::keyPressed(int key){

}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

	if (button == 0)
	{
		player.mouseEvent(mouseX,mouseY);
	}

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){ 

}
