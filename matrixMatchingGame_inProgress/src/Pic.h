//
//  Pic.h
//  matrixMatchingGame
//
//  Created by EOZIN CHE on 1/28/16.
//
//

#pragma once

#include <stdio.h>
#include "ofMain.h"


class Pic {
    
    ofImage image;
    
public:
    Pic(float x, float y, int _h, int co_, int co2_,int co3_);

    void update();
    void draw();
    void drawsneaky();
    
    float locationX;
    float locationY;
    
    int picsize = 100;
    
    
    int match=0;
    int whichcard;
    int col, col2, col3;
    bool tint = false;
    
};





