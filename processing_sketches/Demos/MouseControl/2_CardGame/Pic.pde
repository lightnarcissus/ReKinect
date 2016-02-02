class Pic {
  float locationX;
  float locationY;
  PImage image;
  int match=0;
  int whichcard;
  int col, col2, col3;
  boolean tint=false;
  
  Pic(float x, float y, int _h, int co_, int co2_,int co3_) 
  {
    locationX = x;
    locationY = y;
    match = 0;
    col=co_;
    col2=co2_;
    col3=co3_;

    whichcard = _h;

    image = new PImage();
    image = loadImage( whichcard + ".png" ); 
    image.resize(picsize, picsize);
  }
  
  void draw() 
  {
     
    if(match==1){
     
    } else noTint();
    
    image(image, locationX, locationY);   
    
  }

  void drawsneaky() 
  {
    if (tint==true){
    tint(col, col2, col3);
    } else {noTint();}
    
    
    if(match==0){
    
      image(image, locationX, locationY); 
          
    }
    
  }

//  void rand() {
//    locationX = random(0, width);
//    locationY = random(0, height);
//    println(locationX + " " + locationY);
//    /*
//    if (mousePressed==true) {
//      this.locationX = mouseX;
//      this.locationY = mouseY;
//      println("hi!!!");
//    }
//    */
//  }
  
}