#include "ofApp.h"

//--------------------------------------------------------------
void ofApp::setup(){
    
    ofEnableSmoothing();
    ofSetWindowShape(3840, 1200);
    startTime = ofGetElapsedTimeMillis();
    
//    for(int i = 0; i < picnum; i++){
//        Pic* tempPic = new Pic(i*100,100,0,255,0,0);
//        pics.push_back(tempPic);
//    }
//    ofLog() << pics.size();
    
//    initCards();
    
}

//--------------------------------------------------------------
void ofApp::update(){

}

//--------------------------------------------------------------
void ofApp::draw(){

    ofSetColor(255);
    
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

//--------------------------------------------------------------
void ofApp::initCards(){
    
    vector<int> order;
    
    for(int i = 0; i < picnum; i++){
        order.push_back(0);
    }
    
    order = shuffleIt.shuffle(picnum);
    
    for (int i=0; i< picnum; i++) {
        
        int x = i%2 * (ofGetWidth()/6) + ofGetWidth()/4;
        int y = i/2 * (ofGetHeight()/6) + b;
     
        Pic* tempPic = new Pic(x, y, order[i], c1, c2, c3);
        pics.push_back( tempPic );
        
    }
    
    order = shuffleIt.shuffle(picnum);
    
    for (int i=0; i< picnum; i++) {
        
        int x = i%2 * (ofGetWidth()/6) + ofGetWidth()/4 + 140;
        int y = i/2 * (ofGetHeight()/6)+b;
        
        Pic* tempPic2 = new Pic(x, y, order[i], c1, c2, c3);
        pics2.push_back( tempPic2 );
        
    }
}


//--------------------------------------------------------------
void ofApp::keyPressed(int key){
    
        if( key == '1' ){
            picnum = 4;
            b = 250;
        
            initCards();
        }
    
        if( key == '2' ){
            picnum = 6;
            b = 250;
            
            initCards();
        }
        
        if( key == '3' ){
            picnum = 10;
            b = 70;
            
            initCards();
        }
    
}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){
    if (whichoneamimoving>-1&&whichoneamimoving<pics.size())
    {
        pics2[whichoneamimoving]->locationX+=(ofGetMouseX()-ofGetPreviousMouseX());
        pics2[whichoneamimoving]->locationY+=(ofGetMouseY()-ofGetPreviousMouseY());
    } else if (whichoneamimoving>-1&&whichoneamimoving>=pics.size())
    {
        pics[whichoneamimoving-pics.size()]->locationX+=(ofGetMouseX()-ofGetPreviousMouseX());

        pics[whichoneamimoving-pics.size()]->locationY+=(ofGetMouseY()-ofGetPreviousMouseY());
    }
}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){
    
    whichoneamimoving = -1;
    
    for (int j = pics2.size()-1; j>=0; j--)
    {
        if (ofGetMouseX() >= pics2[j]->locationX&&ofGetMouseX()<(pics2[j]->locationX+picsize)&&ofGetMouseY()>=pics2[j]->locationY&&ofGetMouseY()<(pics2[j]->locationY+picsize))
        {
            
            num=j;
            whichoneamimoving = j;
            
            pics2[j]->tint=true;
            pics2[j]->col=255;
            pics2[j]->col2=215;
            pics2[j]->col3=0;
            
            //ofLog() << pics2[j].col + ""+ "COL!!!";
            break;
        }
    }

    if (whichoneamimoving==-1) {
        
        for (int i = pics.size()-1; i>=0; i--)
        {
            if (ofGetMouseX() >= pics[i]->locationX && ofGetMouseX()<(pics[i]->locationX+picsize)&&ofGetMouseY()>=pics[i]->locationY&&ofGetMouseY()<(pics[i]->locationY+picsize))
            {
                whichoneamimoving = i + pics.size();
                num=i;
                pics[i]->tint=true;
                pics[i]->col=255;
                pics[i]->col2=215;
                pics[i]->col3=0;
                
                break;
            }
        }
    }

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){
    whichoneamimoving = -1;
    checkcollisions();
    pics[num]->tint=false;
    pics2[num]->tint=false;
}


void ofApp::checkcollisions()
{
    match = 0;
    //println(match +""+"match!!!");
    //println("checking collisions...");
    for (int i = 0; i< pics.size(); i++)
    {
        pics[i]->match=0;
        pics2[i]->match=0; // assume they're not touching yet
    }
    for (int i = 0; i<pics.size(); i++)
    {
        for (int j = 0; j<pics2.size(); j++)
        {
            if (pics[i]->whichcard==pics2[j]->whichcard) {
                if ((pics[i]->locationX+picsize)>=pics2[j]->locationX&&pics[i]->locationX<(pics2[j]->locationX+picsize)&&(pics[i]->locationY+picsize)>=pics2[j]->locationY&&pics[i]->locationY<(pics2[j]->locationY+picsize))
                {
                    //println(i + " is touching " + j);
                    pics[i]->match=1;
                    pics2[j]->match=1;
                    match++;
                }
            }
        }
    }
    if (match==pics.size()) {
        
        startTime = ofGetElapsedTimeMillis();
    }
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



