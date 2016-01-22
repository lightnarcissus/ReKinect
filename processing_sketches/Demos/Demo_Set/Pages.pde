void drawFrontPage() {
  //------------------------FRONT PAGE---S--------------------------------------
  pushMatrix();

  translate(width/2, height/2, 1);
  fill(39, 0, 59);
  rect(-width/2, -height/2, width, height);

  //image(title3, -width/2+930, -height/2+300, title3.width*0.4, title3.height*0.4);
  //image(title1, -width/2+150, -height/2+260, title1.width*0.4, title1.height*0.4);
  
  image(title1, -width/2+150, -height/2+260, title1.width*0.4, title1.height*0.4);
  
  //image(title2, -width/2+1050, -height/2+260, title2.width*0.4, title2.height*0.4);
  image(name, -width/2+1050, -height/2+660, name.width*0.3, name.height*0.3);
  image(arrow, -width/2+1112, -height/2+783, arrow.width, arrow.height);
 
  fill(255);
  rect(-width/2+1290, -height/2+637, 420, 90);
  
  //CHOOSE SIDE
  image(probSide, -width/2+150, -height/2+660, probSide.width*0.3, probSide.height*0.3);
  image(left, -width/2+150, -height/2+660+100, left.width*0.5, left.height*0.5);
  image(right, -width/2+500, -height/2+660+100+10, right.width*0.5, right.height*0.5);
  
 
  textFont(font2, 65);
  fill(0);
  textAlign(LEFT);
  text(text1, -width/2 + 1300, 65, width, height);

  popMatrix();
  //------------------------FRONT PAGE---E--------------------------------------
}


void drawMenuPage() {
  //------------------------MENU PAGE---S--------------------------------------
  pushMatrix();

  translate(width/2, height/2, 2);
  fill(25, 0, 38);
  rect(-width/2, -height/2, width, height);

  fill(255);

  if (mouseX>width/2-600-270 && mouseX<width/2-600+270 && mouseY>height/2-100-270 && mouseY<height/2-100+270) {
    stroke(149, 91, 179);
    strokeWeight(12);
    ellipse(-600, -100, 540, 540);
    noStroke();
    ellipse(0, -100, 540, 540);
    ellipse(600, -100, 540, 540);
    println("GAME 1");
  } else if (mouseX>width/2-270 && mouseX<width/2+270 && mouseY>height/2-100-270 && mouseY<height/2-100+270) {
    stroke(149, 91, 179);
    strokeWeight(12);
    ellipse(0, -100, 540, 540);

    noStroke();
    ellipse(-600, -100, 540, 540);
    ellipse(600, -100, 540, 540);
    println("GAME 2");
    
  } else if (mouseX>width/2+600-270 && mouseX<width/2+600+270 && mouseY>height/2-100-270 && mouseY<height/2-100+270) {
    stroke(149, 91, 179);
    strokeWeight(12);
    ellipse(600, -100, 540, 540);

    noStroke();
    ellipse(0, -100, 540, 540);
    ellipse(-600, -100, 540, 540);
    println("GAME 3");
  } else {
    noStroke();
    ellipse(-600, -100, 540, 540);
    ellipse(0, -100, 540, 540);
    ellipse(600, -100, 540, 540);
  }

  image(menu1, -600-((menu1.width*0.7)/2), -menu1.height/2-27, menu1.width*0.7, menu1.height*0.7);
  image(menu2, -((menu2.width*0.7)/2), -menu2.height/2-31, menu2.width*0.7, menu2.height*0.7);
  image(menu3, 600-((menu3.width)/2), -menu3.height/2-101, menu3.width, menu3.height);

  textFont(font2, 75);
  textAlign(CENTER);
  textSize(50);
  fill(255);
  text("Drawing", -600, 340-50);
  text("Challenge", -600, 320 + 85 -50);

  text("Multi-Matrix", 0, 340 -50);
  text("Matching", 0, 320 + 85 -50);

  text("Orchestra", 600, 340 -50);
  text("Conductor", 600, 320 + 85 -50);


  popMatrix();
  //------------------------MENU PAGE---E--------------------------------------
}

void drawInst1Page() {
  smooth();

  //------------------------INST1 PAGE---S--------------------------------------
  pushMatrix();  

  translate(width/2, height/2, 3);
  fill(25, 0, 38);
  rect(-width/2, -height/2, width, height);
  image(game1_inst, -width/2+80, -height/2 +100, game1_inst.width/3, game1_inst.height/3);
  ////CENTER
  fill(255);
  rectMode(CENTER);
  rect(-200, 0, width/3, height-500);

  textFont(font2, 55);
  textAlign(LEFT);
  fill(60);
  text("Drawing Challenge", -470, -200);
  fill(0);
  textSize(30);
  //text("Instruction", -470, -200+65);
  textSize(25);
  String s = "In drawing challenges, you’ll be asked to draw specified shapes while maintaining your balance.  Select the level of difficult that you would like to try.  Level 1 is the easiest.";
  fill(90);
  text(s, -470+276, -200+65+35+300, width/3.5, height-500);
  
String s0 = "level 1 requires less precision and smaller movements/close to the body (for people with Fair sitting or standing balance).";

String s2 = "level 2  requires a moderate amount of precision and movements/close to the body (for people with Good dynamic sitting or standing balance).";

String s3 = "level 3 requires a great amount of precision and a wide range of movements (for people with Good+/Normal dynamic sitting or standing balance).";

textSize(20);
  text(s0, -470+276, -200+65+35+300+170, width/3.5, height-500);
  text(s2, -470+276, -200+65+35+300+250, width/3.5, height-500);
  text(s3, -470+276, -200+65+35+300+350, width/3.5, height-500);
  ////LEFT SIDE
  fill(160, 98, 192);
  ellipse(-740, 430-40, 250, 250);

  fill(255);
  textFont(font2, 45);
  textAlign(CENTER);
  text("HIGH", -740, 450-70-40);
  text("SCORE", -740, 450-30-40);
  textSize(97);
  text("20", -740, 450+52-40);

  ////RIGHT SIDE
  //MOUSE OVER
  if (mouseX > width/2+270-55 && mouseX < width/2+270+55 && mouseY > -55+height/2-(height-640)/2 && mouseY < 55+height/2-(height-640)/2) { 
    fill(255);
    ellipse(270, -(height-620)/2, 110, 110);

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2+150, 110, 110);
    ellipse(270, -(height-620)/2+300, 110, 110);
  } else if (mouseX > width/2+270-55 && mouseX < width/2+270+55 && mouseY > -55+height/2-(height-640)/2+150 && mouseY < 55+height/2-(height-640)/2+150) { 
    fill(255);
    ellipse(270, -(height-620)/2+150, 110, 110); 

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2, 110, 110);
    ellipse(270, -(height-620)/2+300, 110, 110);
  } else if (mouseX > width/2+270-55 && mouseX < width/2+270+55 && mouseY > -55+height/2-(height-640)/2 + 300&& mouseY < 55+height/2-(height-640)/2+300) { 
    fill(255);
    ellipse(270, -(height-620)/2+300, 110, 110);

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2, 110, 110);
    ellipse(270, -(height-620)/2+150, 110, 110);
  } else {

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2, 110, 110);
    ellipse(270, -(height-620)/2+150, 110, 110);
    ellipse(270, -(height-620)/2+300, 110, 110);
  }

  textAlign(LEFT);
  textSize(45);
  text("Level 1", 270+95, -(height-640)/2);
  text("Level 2", 270+95, -(height-640)/2+150);
  text("Level 3", 270+95, -(height-640)/2+300);


  ////RIGHT BOTTOM SIDE
  if ( mouseX > width/2+370+150-300 && mouseX < width/2+370+150 + 300 && mouseY > height/2+ 450 - 100 && mouseY < height/2+450 +100 ) {
    fill(0);
    stroke(255);
  } else {
    fill(160, 98, 192);
  }
  noStroke();
  rect(370+150, 400, 600, 200);
  fill(255);
  textAlign(CENTER);
  textSize(145);
  text("enter", 370+150, 450);

  //fill(20);
  //rect(-width/2, -height/2, width, height);

  popMatrix();

  //------------------------INST1 PAGE---E--------------------------------------
}

void drawInst2Page() { //FOR MULTI-MATRIX GAME
  smooth();

  //------------------------INST2 PAGE---S--------------------------------------
  pushMatrix();  

  translate(width/2, height/2, 3);
  fill(25, 0, 38);
  rect(-width/2, -height/2, width, height);
  image(game1_inst, -width/2+80, -height/2 +100, game1_inst.width/3, game1_inst.height/3);
  ////CENTER
  fill(255);
  rectMode(CENTER);
  rect(-200, 0, width/3, height-500);

  textFont(font2, 55);
  textAlign(LEFT);
  fill(60);
  text("Multi-Matrix Matching", -470, -200);
  fill(0);
  textSize(30);
  //text("Instruction", -470, -200+65);
  textSize(25);
  String s1 = "In matching challenges, you’ll be asked to drag cards to their matches.  How many cards would you like to try first?";
  
  fill(90);
  text(s1, -470+276, -200+65+35+300, width/3.5, height-500);

  ////LEFT SIDE
  fill(160, 98, 192);
  ellipse(-740, 430-40, 250, 250);

  fill(255);
  textFont(font2, 45);
  textAlign(CENTER);
  text("HIGH", -740, 450-70-40);
  text("SCORE", -740, 450-30-40);
  textSize(97);
  text("30", -740, 450+52-40);

  ////RIGHT SIDE
  //MOUSE OVER
  if (mouseX > width/2+270-55 && mouseX < width/2+270+55 && mouseY > -55+height/2-(height-640)/2 && mouseY < 55+height/2-(height-640)/2) { 
    fill(255);
    ellipse(270, -(height-620)/2, 110, 110);

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2+150, 110, 110);
    ellipse(270, -(height-620)/2+300, 110, 110);
  } else if (mouseX > width/2+270-55 && mouseX < width/2+270+55 && mouseY > -55+height/2-(height-640)/2+150 && mouseY < 55+height/2-(height-640)/2+150) { 
    fill(255);
    ellipse(270, -(height-620)/2+150, 110, 110); 

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2, 110, 110);
    ellipse(270, -(height-620)/2+300, 110, 110);
  } else if (mouseX > width/2+270-55 && mouseX < width/2+270+55 && mouseY > -55+height/2-(height-640)/2 + 300&& mouseY < 55+height/2-(height-640)/2+300) { 
    fill(255);
    ellipse(270, -(height-620)/2+300, 110, 110);

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2, 110, 110);
    ellipse(270, -(height-620)/2+150, 110, 110);
  } else {

    noFill();
    stroke(255);
    strokeWeight(4);
    ellipse(270, -(height-620)/2, 110, 110);
    ellipse(270, -(height-620)/2+150, 110, 110);
    ellipse(270, -(height-620)/2+300, 110, 110);
  }

  textAlign(LEFT);
  textSize(45);
  text("Level 1", 270+95, -(height-640)/2);
  text("Level 2", 270+95, -(height-640)/2+150);
  text("Level 3", 270+95, -(height-640)/2+300);


  ////RIGHT BOTTOM SIDE
  if ( mouseX > width/2+370+150-300 && mouseX < width/2+370+150 + 300 && mouseY > height/2+ 450 - 100 && mouseY < height/2+450 +100 ) {
    fill(0);
    stroke(255);
  } else {
    fill(160, 98, 192);
  }
  noStroke();
  rect(370+150, 400, 600, 200);
  fill(255);
  textAlign(CENTER);
  textSize(145);
  text("ENTER", 370+150, 450);

  //fill(20);
  //rect(-width/2, -height/2, width, height);

  popMatrix();

  //------------------------INST2 PAGE---E--------------------------------------
}





void pageClear() {
  if (pageClearOn) {
    pushMatrix();
    translate(width/2, height/2, 3);
    fill(20);
    rect(0, 0, width, height);
    popMatrix();
  }
  pageClearOn = false;
}