//
//  ShuffleManager.cpp
//  matrixMatchingGame
//
//  Created by EOZIN CHE on 1/28/16.
//
//

#include "ShuffleManager.h"

vector<int> ShuffleManager::shuffle(int num){

    vector<int> shuf;
    vector<int> used;
    
    for(int i = 0; i < num; i++)
    {
        used.push_back(0);
        shuf.push_back(0);
    }
    
    for(int i = 0; i < num; i++)
    {
        
        int keeppicking = 1;
        int pick = 0;
        
        while(keeppicking == 1)
        {
            keeppicking = 0;
            pick = int(ofRandom(0, num)); // pick a number
            
            if(used[pick] == 1) keeppicking = 1; // see if it's been used already... if yes, keep looping
        }
        
        shuf[i] = pick; // assign our random number to the shuffle list
        used[pick] = 1;
        
        ofLog() << pick << ".jpg";
    }
    
    return shuf;
}

