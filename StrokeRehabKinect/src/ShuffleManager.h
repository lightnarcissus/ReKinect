//
//  ShuffleManager.h
//  matrixMatchingGame
//
//  Created by EOZIN CHE on 1/28/16.
//
//

#ifndef ShuffleManager_h
#define ShuffleManager_h

#pragma once

#include "ofMain.h"

class ShuffleManager {
    
    
public:
    
    void setup();
    void update();
    void draw();
    
    vector<int> shuffle(int num);
    
};

#endif /* ShuffleManager_h */


