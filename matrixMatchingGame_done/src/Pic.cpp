//
//  Pic.cpp
//  matrixMatchingGame
//
//  Created by EOZIN CHE on 1/28/16.
//
//

#include "Pic.h"

Pic::Pic(float x, float y, int _h, int co_, int co2_,int co3_){
    locationX = x;
    locationY = y;
    match = 0;
    col=co_;
    col2=co2_;
    col3=co3_;

    whichcard = _h;
    
    std::string s = to_string(whichcard);
    image.load( s + ".png" );
    image.resize(picsize, picsize);

    ofEnableAlphaBlending();
}


void Pic::update(){
    
}

void Pic::draw(){
    if(match==1){   }
    else {
        ofSetColor(255);
        //        noTint();
    }
    
    image.draw(locationX, locationY);
 
}

void Pic::drawsneaky(){
    
    if (tint){

        ofSetColor(col, col2, col3, 100);
        
    } else {
         ofSetColor(255);
         //noTint();
    }
    
    if(match==0){
       image.draw(locationX, locationY);
    }
    
}

